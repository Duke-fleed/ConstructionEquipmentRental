using System.Net;
using ConstructionEquipmentRental.Services.GetRentalEquipment;
using ConstructionEquipmentRental.Services.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionEquipmentRental.Api.Features.RentalEquipment
{
    [ApiController]
    [Route("[controller]")]
    public class RentalEquipmentController : ControllerBase
    {
        private readonly IMediator mediatR;

        public RentalEquipmentController(IMediator mediatR)
        {
            this.mediatR = mediatR;
        }

        /// <summary>
        /// Returns existing equipment items and their types
        /// </summary>
        /// <returns>List of items with name and type</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalEquipmentItem>>> Get()
        {
            try
            {
                return (await mediatR.Send(new GetRentalEquipmentRequest())).ToList();
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}