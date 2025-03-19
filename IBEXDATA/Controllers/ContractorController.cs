using Common.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractorController : ControllerBase
    {
          private readonly IContractorService _contractorService;

        public ContractorController(IContractorService contractorService)
        {
            _contractorService = contractorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContractors()
        {
            var contractors = await _contractorService.GetAllContractors();
            return Ok(contractors);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContractor(ContractorDTO2 contractor)
        {

            try
            {
                await _contractorService.UpdateContractor(contractor);
                return Ok(new { message = "success" });
            }
            catch (KeyNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// not work


        [HttpPost]
        public async Task<IActionResult> CreateContractor(ContractorDTO2 contractor)
        {
            await _contractorService.CreateContractor(contractor);
            return Ok(new { message = "success" });
        }

        [Route("GetContractorById/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetContractorById(int userId)
        {
            var contractor = await _contractorService.GetAllContractorById(userId);
            return Ok(contractor);
        }

    }
}
