using iForce.App;
using iForce.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iForce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAccess _customerAccess;

        public CustomerController(ICustomerAccess dataAccess)
        {
            _customerAccess = dataAccess;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Customer>), StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            var customers = _customerAccess.Get(null);
            foreach (var customer in customers)
            {
                customer.SetDatesLocalIfUnspecified();
            }
            return Ok(customers);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IReadOnlyList<Customer>), StatusCodes.Status200OK)]
        public IActionResult Index([FromBody] CustomerFilter filter)
        {
            var customers = _customerAccess.Get(filter);
            foreach (var customer in customers)
            {
                customer.SetDatesLocalIfUnspecified();
                foreach (var vehicle in customer.Vehicles)
                {
                    vehicle.SetDatesLocalIfUnspecified();
                }
            }
            return Ok(customers);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IdResult), StatusCodes.Status200OK)]
        public IActionResult Insert([FromBody] CustomerBase customer)
        {
            return Ok(new IdResult { NewId = _customerAccess.Create(customer) });
        }

        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] CustomerBase customer)
        {
            return _customerAccess.Change(id, customer) ? Ok() : NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            return _customerAccess.Delete(id) ? Ok() : NotFound();
        }

    }
}
