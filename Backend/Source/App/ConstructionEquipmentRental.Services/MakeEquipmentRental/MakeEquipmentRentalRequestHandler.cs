using ConstructionEquipmentRental.Services.Models;
using MediatR;

namespace ConstructionEquipmentRental.Services.MakeEquipmentRental
{
    internal class MakeEquipmentRentalRequestHandler : IRequestHandler<MakeEquipmentRentalRequest, int>
    {
        private readonly IPersistRental persistRental;
        private readonly IRentalEquipmentInventory rentalEquipmentInventory;

        public MakeEquipmentRentalRequestHandler(IRentalEquipmentInventory rentalEquipmentInventory, IPersistRental persistRental)
        {
            this.rentalEquipmentInventory = rentalEquipmentInventory;
            this.persistRental = persistRental;
        }

        public async Task<int> Handle(MakeEquipmentRentalRequest request, CancellationToken cancellationToken)
        {
            var equipment = await rentalEquipmentInventory.GetAll();

            if (request.RentalItemsWithDays.Any(requestItem => equipment.All(x => x.Id != requestItem.id)))
                throw new ArgumentException("Invalid equipment Id!");

            var rentalItems = request.RentalItemsWithDays.Select(x => new RentalItem
            {
                NumberOfDays = x.numberOfDays,
                EquipmentItemId = x.id
            });
            var rental = new Rental(DateTime.UtcNow, rentalItems);

            return (await persistRental.SaveRental(rental)).Id;
        }
    }
}
