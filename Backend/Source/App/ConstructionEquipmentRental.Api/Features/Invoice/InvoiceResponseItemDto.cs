namespace ConstructionEquipmentRental.Api.Features.Invoice
{
    public record struct InvoiceResponseItemDto(string ItemName, decimal ItemPrice)
    {
        public string ItemName { get; init; } = ItemName;
        public decimal ItemPrice { get; init; } = ItemPrice;
    }
}
