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
        { 
            var MortagegeLevels= await _mortagegeService.GetAllMortagegeLevels();
            return Ok(MortagegeLevels);
               

        }
        [Route("SaveFullMortagege/{mortagegeId}")]
        [HttpPut]
        public async Task<IActionResult> SaveFullMortagege([FromBody] MortagegeDTO mortagegeDTO, int mortagegeId)
        {
            try
            {
                if (mortagegeDTO == null)
                {
                    return BadRequest(new { error = "Invalid object.", details = "The MortagegeDTO object is null." });
                }

                await _mortagegeService.SaveFullMortagege(mortagegeDTO, mortagegeId);
                // החזרת תגובה בפורמט JSON
                return Ok(new { message = "Mortgage saved successfully.", mortagegeId = mortagegeId });
            }
            catch (Exception ex)
            {
                // Log the exception (logging implementation is skipped here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the mortagege.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMortagege([FromBody] MortagegeDTO mortagegeDTO)
        {
            try
            {
                if (mortagegeDTO == null)
                {
                    return BadRequest(new { message = "Invalid object." });
                }

                // צור את המשכנתא וקבל את ה-ID שלה
                var newMortagegeId = await _mortagegeService.CreateMortagege(mortagegeDTO);

                // החזר את ה-ID של המשכנתא שנוספה בתגובה
                return Ok(new { id = newMortagegeId });
            }
            catch (Exception ex)
            {
                // Log the exception (logging implementation is skipped here)
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the mortagege." });
            }
        }
        [Route("GetAllTypeMessages")]
        [HttpGet]
        public async Task<IActionResult> GetAllTypeMessages()
        {

            var TypeMessages = await _mortagegeService.GetAllTypeMessages();
            return Ok(TypeMessages);
        }
    }
    }
