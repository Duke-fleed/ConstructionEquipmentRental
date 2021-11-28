using ConstructionEquipmentRental.Services.Models;
using ConstructionEquipmentRental.Services.Options;
using Microsoft.Extensions.Options;

namespace ConstructionEquipmentRental.Services
{
    public class RentalEquipmentInventory : IRentalEquipmentInventory
    {
        private readonly List<EquipmentListOptions> equipmentListOptions;
        private IEnumerable<RentalEquipmentItem>? rentalEquipmentItems;

        public RentalEquipmentInventory(IOptions<List<EquipmentListOptions>> equipmentListOptions)
        {
            this.equipmentListOptions = equipmentListOptions.Value;
        }

        public Task<IEnumerable<RentalEquipmentItem>> GetAll()
        {
            rentalEquipmentItems ??= equipmentListOptions.Select(x =>
            {
                if (Enum.TryParse<EquipmentType>(x.Type, out var equipmentType))
                {
                    return new RentalEquipmentItem(x.Id, x.Name, equipmentType);
                }

                throw new InvalidOptionsException("Incorrect equipment type");
            });
            return Task.FromResult(rentalEquipmentItems);
        }
    }
}
