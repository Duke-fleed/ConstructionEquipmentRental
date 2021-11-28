using ConstructionEquipmentRental.Services.Models;
using MediatR;

namespace ConstructionEquipmentRental.Services.GetRentalEquipment
{
    public class GetRentalEquipmentRequestHandler : IRequestHandler<GetRentalEquipmentRequest, IEnumerable<RentalEquipmentItem>>
    {
        private readonly IRentalEquipmentInventory rentalEquipmentInventory;

        public GetRentalEquipmentRequestHandler(IRentalEquipmentInventory rentalEquipmentInventory)
        {
            this.rentalEquipmentInventory = rentalEquipmentInventory;
        }

        public Task<IEnumerable<RentalEquipmentItem>> Handle(GetRentalEquipmentRequest request, CancellationToken cancellationToken)
        {
            return rentalEquipmentInventory.GetAll();
        }
    }
}