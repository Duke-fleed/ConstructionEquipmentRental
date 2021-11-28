using ConstructionEquipmentRental.Services.Models;

namespace ConstructionEquipmentRental.Services
{
    public interface IRentalEquipmentInventory
    {
        Task<IEnumerable<RentalEquipmentItem>> GetAll();
    }
}
