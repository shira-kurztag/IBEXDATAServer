using Common.DTO;
using IBEXDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MortagegeController : ControllerBase
    {

        private readonly IMortagegeService _mortagegeService;

        public MortagegeController(IMortagegeService mortagegeService)
        {
            _mortagegeService = mortagegeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMortagegesTypes()
        {
            try
            {
                var MortagegesTypes = await _mortagegeService.GetAllMortagegesTypes();

                return Ok(MortagegesTypes);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("GetAllCurrencyTypes")]
        [HttpGet]
        public async Task<IActionResult> GetAllCurrencyTypes()
        {
           
              var CurrencyTypes= await _mortagegeService.GetAllCurrencyTypes();
                return Ok(CurrencyTypes);
        }
        [Route("GetAllMortagegeLevels")]
        [HttpGet]
        public async Task<IActionResult> GetAllMortagegeLevels()
        { var MortagegeLevels= await _mortagegeService.GetAllMortagegeLevels();
            return Ok(MortagegeLevels);
               

        }

        [HttpPost]
        public async Task<IActionResult> CreateMortagege([FromBody] MortagegeDTO mortagegeDTO)
        {
            try
            {
                if (mortagegeDTO == null)
                {
                    return BadRequest("Invalid object.");
                }

                await _mortagegeService.CreateMortagege(mortagegeDTO);
                return Ok("Mortagege created successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception (logging implementation is skipped here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the mortagege.");
            }
        }

    }
}
