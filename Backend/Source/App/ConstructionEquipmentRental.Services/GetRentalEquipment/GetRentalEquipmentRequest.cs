using ConstructionEquipmentRental.Services.Models;
using MediatR;

namespace ConstructionEquipmentRental.Services.GetRentalEquipment
{
    public class GetRentalEquipmentRequest : IRequest<IEnumerable<RentalEquipmentItem>>
    {
    }
}
