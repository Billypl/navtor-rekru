using Microsoft.AspNetCore.Mvc;
using NavtorShiper.Entities;
using NavtorShiper.Services;

namespace NavtorShiper.Web.Controllers
{
    [ApiController]
    [Route("api/ship/{imo}/tank")]
    public class TankerShipController : ControllerBase
    {
        private readonly ITankerShipService _tankerShipService;
        private readonly IShipService _shipService;

        public TankerShipController(ITankerShipService tankerShipService, IShipService shipService)
        {
            _tankerShipService = tankerShipService;
            _shipService = shipService;
        }

        [HttpPut("{tankId}/refuel")]
        public ActionResult Refuel(
            [FromRoute] string imo, 
            [FromRoute] int tankId, 
            [FromQuery] FuelType fuelType, 
            [FromQuery] double amount)
        {
            _tankerShipService.RefuelTank(imo, tankId, fuelType, amount);
            return Ok();
        }

        [HttpPut("{tankId}/empty")]
        public ActionResult Empty(
            [FromRoute] string imo,
            [FromRoute] int tankId)
        {
            _tankerShipService.EmptyTank(imo, tankId);
            return Ok();
        }
    }
}
