using ConstructionEquipmentRental.Services.Models;
using MediatR;

namespace ConstructionEquipmentRental.Services.GenerateInvoice
{
    public class GenerateInvoiceRequest : IRequest<InvoiceResponse?>
    {
        public GenerateInvoiceRequest(int rentalId)
        {
            RentalId = rentalId;
        }

        public int RentalId { get; set; }
    }
}
