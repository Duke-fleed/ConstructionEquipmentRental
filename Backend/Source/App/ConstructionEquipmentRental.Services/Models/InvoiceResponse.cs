namespace ConstructionEquipmentRental.Services.Models
{
    public record struct InvoiceResponse(IEnumerable<(string name, decimal price)> Items, int BonusPointsEarned);
}
