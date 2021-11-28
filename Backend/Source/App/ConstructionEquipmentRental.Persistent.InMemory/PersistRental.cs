using ConstructionEquipmentRental.Services;
using ConstructionEquipmentRental.Services.Models;

namespace ConstructionEquipmentRental.Persistent.InMemory
{
    public class PersistRental : IPersistRental
    {
        private readonly IList<Rental> rentalStore = new List<Rental>();
        private readonly object _lock = new();

        public Task<IList<Rental>> GetRentals()
        {
            return Task.FromResult(rentalStore);
        }

        public Task<Rental> SaveRental(Rental rental)
        {
            lock (_lock)
            {
                rental.Id = !rentalStore.Any() ? 1 : rentalStore.Max(x => x.Id) + 1;
                rentalStore.Add(rental);
            }
            return Task.FromResult(rental);
        }

        public Task<Rental?> GetById(int id)
        {
            return Task.FromResult(rentalStore.FirstOrDefault(x => x.Id == id));
        }
    }
}