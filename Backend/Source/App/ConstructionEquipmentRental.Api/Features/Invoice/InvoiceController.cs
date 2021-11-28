using ConstructionEquipmentRental.Api.Features.RentalEquipment;
using ConstructionEquipmentRental.Services.GenerateInvoice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionEquipmentRental.Api.Features.Invoice
{
    /// <summary>
    /// Controller for generating invoices
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<RentalEquipmentController> _logger;
        private readonly IMediator mediatR;

        public InvoiceController(ILogger<RentalEquipmentController> logger, IMediator mediatR)
        {
            _logger = logger;
            this.mediatR = mediatR;
        }

        /// <summary>
        /// Generates invoice object based on rental Id
        /// </summary>
        /// <param name="rentalId">Id of the rental record, for which invoice needs to be generated</param>
        /// <returns>Invoice object</returns>
        [HttpGet("{rentalId}")]
        public async Task<ActionResult<InvoiceResponseDto>> Get([FromRoute] int rentalId)
        {
            var invoiceResponse = await mediatR.Send(new GenerateInvoiceRequest(rentalId));
            if (invoiceResponse is null)
                return NotFound();
            return InvoiceResponseDto.FromDomain(invoiceResponse.Value);
        }
    }
}