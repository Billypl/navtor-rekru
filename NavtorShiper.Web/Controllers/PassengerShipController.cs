using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using NavtorShiper.Entities;
using NavtorShiper.Services;
using NavtorShiper.Web.DTOs;

namespace NavtorShiper.Web.Controllers
{
    [ApiController]
    [Route("api/ship/{imo}/passenger")]

    public class PassengerShipController : ControllerBase
    {
        private readonly IPassengerShipService _passengerShipService;
        private readonly IShipService _shipService;

        public PassengerShipController(IPassengerShipService passengerShipService, IShipService shipService)
        {
            _passengerShipService = passengerShipService;
            _shipService = shipService;
        }

        [HttpPost]
        public ActionResult Add([FromRoute] string imo, [FromBody] Passenger passenger)
        {
            _passengerShipService.AddPassenger(imo, passenger);
            return Created();
        }

        [HttpDelete("{passengerId}")]
        public ActionResult Delete([FromRoute] string imo, [FromRoute] int passengerId)
        {
            _passengerShipService.RemovePassenger(imo, passengerId);
            return NoContent();
        }
    }
}
