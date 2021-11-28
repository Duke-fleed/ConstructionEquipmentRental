using ConstructionEquipmentRental.Services.Models;
using ConstructionEquipmentRental.Services.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace ConstructionEquipmentRental.Services.GenerateInvoice
{
    public class GenerateInvoiceRequestHandler : IRequestHandler<GenerateInvoiceRequest, InvoiceResponse?>
    {
        private readonly IPersistRental persistRental;
        private readonly FeeOptionsEur feeOptionsEur;
        private readonly IRentalEquipmentInventory rentalEquipmentInventory;

        public GenerateInvoiceRequestHandler(IPersistRental persistRental, IRentalEquipmentInventory rentalEquipmentInventory, IOptions<FeeOptionsEur> feeOptions)
        {
            this.persistRental = persistRental;
            this.rentalEquipmentInventory = rentalEquipmentInventory;
            this.feeOptionsEur = feeOptions.Value;
        }

        public async Task<InvoiceResponse?> Handle(GenerateInvoiceRequest request, CancellationToken cancellationToken)
        {
            var rental = await persistRental.GetById(request.RentalId);

            if (rental is null)
                return null;

            var inventory = await rentalEquipmentInventory.GetAll();

            var rentalItems = rental.RentalItems.Select(rentalItem =>
            {
                var item = inventory.FirstOrDefault(x => x.Id == rentalItem.EquipmentItemId);
                if (item is null)
                    throw new ArgumentException($"Invalid equipment Id {rentalItem.EquipmentItemId}");

                return (item, rentalItem.NumberOfDays);
            });

            IEnumerable<(string name, decimal price)> itemsWithPrice = rentalItems.Select(x => (x.item.Name, CalculateRentalItemPrice(x.item, x.NumberOfDays)));


            return new InvoiceResponse
            {
                Items = itemsWithPrice,
                BonusPointsEarned = CalculatePoints(rentalItems)
            };
        }

        private static int CalculatePoints(IEnumerable<(RentalEquipmentItem item, int numberOfDays)> items)
        {
            var heavyItemsCount = items.Count(x => x.item.EquipmentType == EquipmentType.Heavy);
            return items.Count() + heavyItemsCount;
        }

        private decimal CalculateRentalItemPrice(RentalEquipmentItem item, int numberOfDays)
        {
            return item.EquipmentType switch
            {
                EquipmentType.Heavy => feeOptionsEur.OneTime + numberOfDays * feeOptionsEur.PremiumPerDay,
                EquipmentType.Regular => feeOptionsEur.OneTime +
                                         Math.Min(numberOfDays, 2) * feeOptionsEur.PremiumPerDay +
                                         Math.Max(numberOfDays - 2, 0) * feeOptionsEur.RegularPerDay,
                EquipmentType.Specialized => Math.Min(numberOfDays, 3) * feeOptionsEur.PremiumPerDay +
                                             Math.Max(numberOfDays - 3, 0) * feeOptionsEur.RegularPerDay,
                _ => throw new NotImplementedException()
            };
        }
    }
}
