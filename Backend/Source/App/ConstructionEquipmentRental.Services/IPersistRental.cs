using ConstructionEquipmentRental.Services.Models;

namespace ConstructionEquipmentRental.Services
{
    public interface IPersistRental
    {
        Task<IList<Rental>> GetRentals();
        Task<Rental> SaveRental(Rental rental);
        Task<Rental?> GetById(int id);
    }
}
