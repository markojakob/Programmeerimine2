using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KooliProjekt.Controllers
{
    [Route("api/rentings")]
    [ApiController]
    public class RentingsApiController : ControllerBase
    {
        private readonly IRentingService _rentingService;

        public RentingsApiController(IRentingService service)
        {
            _rentingService = service;
        }



        // GET: api/<RentingsApiController>
        [HttpGet]
        public async Task<IEnumerable<Renting>> Get()
        {
            var result = _rentingService.List(1, 50000);
            return result.Result;
        }

        // GET api/<RentingsApiController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var list = await _rentingService.Get(id);
            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // POST api/<RentingsApiController>
        [HttpPost]
        public async Task<object> Post([FromBody] Renting list)
        {
            await _rentingService.Save(list);

            return Ok(list);

        }

        // PUT api/<RentingsApiController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Renting list)
        {
            if (id != list.Id)
            {
                return BadRequest("Id mismatch");
            }
            await _rentingService.Save(list);

            return Ok();

        }

        // DELETE api/<RentingsApiController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var list = await _rentingService.Get(id);

            if (list == null)
            {
                return NotFound();
            }

            await _rentingService.Delete(id);

            return Ok();
        }


    }
}