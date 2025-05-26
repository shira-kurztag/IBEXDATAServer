using Common.DTO;
using DB;
using IBEXDATA.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabuController : ControllerBase
    {
        private readonly ITabusService _tabusService;

        public TabuController(ITabusService TabusService)
        {
            _tabusService = TabusService;
        }

        // GET: api/<TabusController>
        [HttpGet("{ApartmentId}")]
        public async Task<IActionResult> GetTabusByApartmentId(int ApartmentId)
        {
            try
            {
                // Fetch the list of tenants from the service
                var tabu = await _tabusService.GetTabusByApartmentId(ApartmentId);

                // Return the list in the response
                return Ok(tabu);
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

        //// GET api/<TabusController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<TabusController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}


        // PUT api/<TabusController>/5


        //// PUT api/<TabusController>/5
        [HttpPut()]
        public async Task<IActionResult> Put( [FromBody] TabuDTO tabu)
        {
            if (tabu == null )
            {
                return BadRequest("No tabu provided.");
            }

            try
            {

                await _tabusService.UpdateTabusByOwnerId(tabu);
                return Ok(new { message = "Tabu Update successfully." });
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

        //// DELETE api/<TabusController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
