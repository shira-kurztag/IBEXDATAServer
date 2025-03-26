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
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<TenantController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TenantController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
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


    
