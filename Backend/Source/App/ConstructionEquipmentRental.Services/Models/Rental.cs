namespace ConstructionEquipmentRental.Services.Models
{
    public class Rental
    {
        public Rental(DateTime rentalDateUtc, IEnumerable<RentalItem> rentalItems)
        {
            RentalDateUtc = rentalDateUtc;
            RentalItems = rentalItems;
        }

        public int Id { get; set; }
        public DateTime RentalDateUtc { get; set; }
        public IEnumerable<RentalItem> RentalItems { get; set; }
    }
}