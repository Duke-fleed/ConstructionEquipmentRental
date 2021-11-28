using ConstructionEquipmentRental.Services.Models;

namespace ConstructionEquipmentRental.Api.Features.Invoice
{
    /// <summary>
    /// Invoice response returned after requesting invoice
    /// </summary>
    public record struct InvoiceResponseDto
    {
        /// <summary>
        /// Title of the invoice
        /// </summary>
        public string Title { get; init; }
        /// <summary>
        /// Rental items list with prices
        /// </summary>
        public IEnumerable<InvoiceResponseItemDto> Items { get; init; }
        /// <summary>
        /// Total price of the invoice
        /// </summary>
        public decimal TotalPrice { get; init; }
        /// <summary>
        /// Total number of bonus points earned after order
        /// </summary>
        public int BonusPointsEarned { get; init; }

        public static InvoiceResponseDto FromDomain(InvoiceResponse invoiceResponse) => new()
        {
            Title = "Invoice for construction equipment rental",
            Items = invoiceResponse.Items.Select(x => new InvoiceResponseItemDto(x.name, x.price)),
            BonusPointsEarned = invoiceResponse.BonusPointsEarned,
            TotalPrice = invoiceResponse.Items.Sum(x => x.price)
        };
    }
}
