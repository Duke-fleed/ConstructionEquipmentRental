namespace ConstructionEquipmentRental.Services.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime RentalDateUtc { get; set; }
        public IEnumerable<RentalItem> RentalItems { get; set; }
    }
}