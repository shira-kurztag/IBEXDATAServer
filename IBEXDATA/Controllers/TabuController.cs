using Common.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabusController : ControllerBase
    {
        // GET: api/<TabusController>
        [HttpGet("{OwnerId}")]
        public async Task<Tabus> GetTabusByOwnerId (int OwnerId) // מספר

        {
            try
            {
                // קבל את הרשימה של הדיירים מהשירות
                var Tabus = await _tenantService.GetTenantByApartment(OwnerId);
                // החזר את הרשימה בתגובה
                return Ok(Tabus);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        // GET api/<TabusController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TabusController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TabusController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TabusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
