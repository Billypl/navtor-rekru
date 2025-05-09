using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NavtorShiper.Entities;
using NavtorShiper.Services;
using NavtorShiper.Web.DTOs;

namespace NavtorShiper.Web.Controllers
{
    [ApiController]
    [Route("api/ship")]

    public class ShipController : ControllerBase
    {
        private readonly IShipService _shipService;

        public ShipController(IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ShipDto>> GetAll()
        {
            var ships = _shipService.GetAll();
            return Ok(ShipDto.MapToDtos(ships));
        }

        [HttpGet("{imo}")]
        public ActionResult<ShipDto> GetById([FromRoute] string imo) 
        {
            var ship = _shipService.GetById(imo);
            return Ok(ShipDto.MapToDto(ship));
        }

        [HttpPost]
        public ActionResult Add([FromBody] ShipDto ship)
        {
            _shipService.Add(ShipDto.MapToEntity(ship));
            return Created();
        }

        [HttpDelete("{imo}")]
        public ActionResult Delete([FromRoute] string imo)
        {
            _shipService.Delete(imo);
            return NoContent();
        }
    }
}
