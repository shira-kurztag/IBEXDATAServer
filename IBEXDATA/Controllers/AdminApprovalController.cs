using Common.DTO;
using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApprovalController : ControllerBase
    {

        private readonly IAdminApprovalService _AdminApprovalService;
        public AdminApprovalController(IAdminApprovalService AdminApprovalService)
        {
            _AdminApprovalService = AdminApprovalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminApproval([FromBody] AdminApprovalDTO adminApprovalDB)
        {
            try
            {
                if (adminApprovalDB == null)
                {
                    return BadRequest(new { message = "Invalid object." });
                }

                // שולח את האובייקט ישירות לסרביס
                await _AdminApprovalService.CreateAdminApproval(adminApprovalDB);

                return Ok(new { message = "Admin approval created successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (logging implementation is skipped here)
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the adminApproval." });
            }
        }
    }
}
