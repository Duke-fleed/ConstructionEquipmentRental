using ConstructionEquipmentRental.Services.MakeEquipmentRental;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionEquipmentRental.Api.Features.Rental
{
    /// <summary>
    /// Controller for submitting new rental requests
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator mediatR;

        public RentalController(ILogger<RentalController> logger, IMediator mediatR)
        {
            this.mediatR = mediatR;
        }

        /// <summary>
        /// Creates a new rental record
        /// </summary>
        /// <param name="items">List of item ids and number of days for rental</param>
        /// <returns>Id of the newly created Rental record</returns>
        [HttpPost]
        public async Task<ActionResult<int>> Post(ICollection<RentEquipmentRequestItem> items)
        {
            if (!items.Any())
                return BadRequest("Rental request cannot be empty!");

            return await mediatR.Send(new MakeEquipmentRentalRequest(items.Select(x=> (x.ItemId.Value, x.NumberOfDays.Value))));
        }
    }
}