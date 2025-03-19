using IBEXDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FareController : ControllerBase
    {
        private readonly IFareService _FareService;
        public FareController(IFareService FareService)
        {
            _FareService = FareService;
        }

        [Route("GetFare")]
        [HttpGet]
        public IEnumerable<Fare> GetFares()
        {
            return _FareService.GetFares();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteFare(int id)
        {
            _FareService.DeleteFare(id);
            return Ok();
        }
        [HttpPut("{fareId}")]
        public IActionResult UpdateFare([FromRoute] int fareId, [FromBody] string farename)
        {
            try
            {
                var updatedfares = _FareService.UpdateFare(farename, fareId);
                return Ok(updatedfares);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Route("GetFilter")]
        [HttpGet]
        public async Task<IEnumerable<Fare>> filterFare(string FareName)
        {

            return await _FareService.filterFare(FareName);
        }
        [HttpPost]
        public IEnumerable<Fare> AddFare([FromBody] Fare fare)
        {
            var fare2 = _FareService.AddFare(fare);

            return fare2;
        }


        [HttpPut("{fareId}/amount")]
        public IActionResult UpdateFareAmount([FromRoute] int fareId, [FromBody] int fareAmount)
        {
            try
            {
                _FareService.UpdateFareAmount(fareId, fareAmount);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
