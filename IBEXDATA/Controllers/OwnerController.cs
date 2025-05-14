using AutoMapper;
using Common.DTO;
using DB;
using IBEXDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _OwnerService;
        private readonly ILogger<BankController> _logger;
        private readonly IMapper _mapper;
        private readonly IOwnerTenantService _OwnerTenantService;
        public OwnerController(IOwnerService OwnerService, ILogger<BankController> logger, IMapper mapper, IOwnerTenantService OwnerTenantService)
        {

            _OwnerService = OwnerService;
            _logger = logger;
            _mapper = mapper;
            _OwnerTenantService = OwnerTenantService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var owners = await _OwnerService.Get();
            if (owners != null)
            {
                           return Ok(owners);
            }

            _logger.LogWarning("Failed to retrieve owners.");
            return BadRequest();
        }

        [HttpGet("GetOwnersByApartmentId/{apartmentId}")]

        public async Task<IActionResult> GetOwnersByApartmentId(int apartmentId)
        {
            try
            {
                var owners = await _OwnerService.GetOwnerByApartment(apartmentId);
                if (owners == null || !owners.Any())
                {
                    return NotFound();
                }

        
                var simpleTeants = _mapper.Map<List<TenantWithIdDTO>>(owners);
                return Ok(simpleTeants) ;
            }
            catch (ApplicationException ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

      
        [HttpPost("GetAllOwnersByTenants")]
        public async Task<ActionResult<List<SimpleOwnerDTO>>> GetAllOwnersByTenants([FromBody] List<int> tenants)
        {
            var result = await _OwnerService.GetAllOwnersByTenants(tenants);
            return Ok(result);
        }

    }
}
