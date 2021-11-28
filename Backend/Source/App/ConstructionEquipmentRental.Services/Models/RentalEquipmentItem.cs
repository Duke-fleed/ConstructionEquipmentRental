namespace ConstructionEquipmentRental.Services.Models
{
    public class RentalEquipmentItem
    {
        public RentalEquipmentItem(int id,string name, EquipmentType equipmentType)
        {
            Name = name;
            EquipmentType = equipmentType;
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }
}
