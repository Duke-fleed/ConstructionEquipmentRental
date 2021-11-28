using MediatR;

namespace ConstructionEquipmentRental.Services.MakeEquipmentRental
{
    public class MakeEquipmentRentalRequest : IRequest<int>
    {
        public MakeEquipmentRentalRequest(IEnumerable<(int id, int numberOfDays)> rentalItemsWithDays)
        {
            RentalItemsWithDays = rentalItemsWithDays;
        }

        public IEnumerable<(int id, int numberOfDays)> RentalItemsWithDays { get; set; }
    }
}
