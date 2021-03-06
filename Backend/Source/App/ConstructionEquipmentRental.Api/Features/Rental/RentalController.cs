using System.Net;
using ConstructionEquipmentRental.Services.MakeEquipmentRental;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionEquipmentRental.Api.Features.Rental
{
    /// <summary>
    /// Controller for submitting new rental requests
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator mediatR;

        public RentalController(IMediator mediatR)
        {
            this.mediatR = mediatR;
        }

        /// <summary>
        /// Creates a new rental record
        /// </summary>
        /// <param name="items">List of item ids and number of days for rental</param>
        /// <returns>Id of the newly created Rental record</returns>
        [HttpPost]
        [MapToApiVersion("1")]
        public async Task<ActionResult<int>> Post(ICollection<RentEquipmentRequestItem> items)
        {
            if (!items.Any())
                return BadRequest("Rental request cannot be empty!");

            try
            {
                return await mediatR.Send(
                    new MakeEquipmentRentalRequest(items.Select(x => (x.ItemId.Value, x.NumberOfDays.Value))));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}