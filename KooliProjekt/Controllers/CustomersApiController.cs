using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KooliProjekt.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomersApiController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersApiController(ICustomerService service)
        {
            _customerService = service;
        }



        // GET: api/<CarsApiController>
        [HttpGet]
        public async Task <IEnumerable<Customer>> Get()
        {
            var result = _customerService.List(1, 50000);
            return result.Result;
        }

        // GET api/<CarsApiController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var list = await _customerService.Get(id);
            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // POST api/<CarsApiController>
        [HttpPost]
        public async Task<object> Post([FromBody] Customer list)
        {
            await _customerService.Save(list);

            return Ok(list);

        }

        // PUT api/<CarsApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer list)
        {
            if(id != list.Id)
            {
                return BadRequest("Id mismatch");
            }
            await _customerService.Save(list);

            return Ok();

        }

        // DELETE api/<CarsApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var list = await _customerService.Get(id);

            if(list == null)
            {
                return NotFound();
            }

            await _customerService.Delete(id);

            return Ok();
        }

        
    }
}
