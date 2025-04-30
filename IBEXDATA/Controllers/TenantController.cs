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
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly IApartmentService _ApartmentService;


        public TenantController(ITenantService tenantService,IApartmentService ApartmentService)
        {
            _tenantService = tenantService;
            _ApartmentService = ApartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            try
            {
                var tenants = await _tenantService.GetAllTenants();

                return Ok(tenants);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
        [Route("GetTenantById/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetTenantById(int userId)
        {
            var tenant = await _tenantService.GetTenantById(userId);
            return Ok(tenant);
        }
        [Route("GetTenantByApartment/{apartment}")]
        [HttpGet]
        public async Task<IActionResult> GetTenantByApartment(int apartment)
        {
            try
            {
                // קבל את הרשימה של הדיירים מהשירות
                var tenants = await _tenantService.GetTenantByApartment(apartment);
                // החזר את הרשימה בתגובה
                return Ok(tenants);
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
        //
        // POST api/<TenantController>
        [HttpPost]
     
        public async Task<IActionResult> Post([FromBody] List<TenantDTO> tenants)
        {
            if (tenants == null || tenants.Count == 0)
            {
                return BadRequest("No tenants provided.");
            }

            try
            {
                await _tenantService.AddTenants(tenants);
                return Ok(new { message = "Tenants added successfully." });
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

        // PUT api/<TenantController>/5
        [HttpPut]
        public async Task<IActionResult> Put( [FromBody] List<TenantDTO2> tenants)
        {
            if (tenants == null || tenants.Count == 0)
            {
                return BadRequest("No tenants provided.");
            }

            try
            {
                await _tenantService.UpdateTenant(tenants);
                return Ok(new { message = "Tenants Update successfully." });
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

        //// DELETE api/<TenantController>/5
        [HttpDelete("{tenantId}")]
        public async Task<IActionResult> Delete(int tenantId)
        {
            if (tenantId == null || tenantId == 0)
            {
                return BadRequest("No tenants provided.");
            }

            try
            {
                await _tenantService.DeleteTenant(tenantId);
                return Ok(new { message = "Tenants delete  successfully." });
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


        [Route("GetPartAssetByOwnerTenants/{Id}")]
        [HttpGet]
        public async Task<ActionResult<double>> GetPartAssetByOwnerTenants(int Id)
        {
            var PartAsset = await _tenantService.GetPartAssetByOwnerTenants(Id);
            if (PartAsset == null)
            {
                return NotFound("Tenant not found");
            }
            return Ok(PartAsset);

        }




    }

}


    
