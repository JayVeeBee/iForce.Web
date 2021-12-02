using iForce.App;
using iForce.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iForce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleAccess _vehicleAccess;

        public VehicleController(IVehicleAccess dataAccess)
        {
            _vehicleAccess = dataAccess;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Vehicle>), StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            var vehicles = _vehicleAccess.Get(null);
            foreach (var vehicle in vehicles)
            {
                vehicle.SetDatesLocalIfUnspecified();
            }
            return Ok(vehicles);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IReadOnlyList<Vehicle>), StatusCodes.Status200OK)]
        public IActionResult Index([FromBody] VehicleFilter filter)
        {
            var vehicles = _vehicleAccess.Get(filter);
            foreach (var vehicle in vehicles)
            {
                vehicle.SetDatesLocalIfUnspecified();
            }
            return Ok(vehicles);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IdResult), StatusCodes.Status200OK)]
        public IActionResult Insert([FromBody] VehicleBase vehicle)
        {
            return Ok(new IdResult { NewId = _vehicleAccess.Create(vehicle) });
        }

        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] VehicleBase vehicle)
        {
            return _vehicleAccess.Change(id, vehicle) ? Ok() : NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            return _vehicleAccess.Delete(id) ? Ok() : NotFound();
        }

    }
}
