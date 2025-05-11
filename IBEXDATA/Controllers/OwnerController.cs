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

        public OwnerController(IOwnerService OwnerService, ILogger<BankController> logger, IMapper mapper)
        {
            _OwnerService = OwnerService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var owners = await _OwnerService.Get();
            if (owners != null)
            {
               // var ownerDTOs = _mapper.Map<List<Owner>, List<OwnerDTO>>(owners.ToList());
               // _logger.LogInformation("Successfully retrieved all owners.");
                return Ok(owners);
            }

            _logger.LogWarning("Failed to retrieve owners.");
            return BadRequest();
        }
        [Route("GetOwnerByApartmentId/{ApartmentId}")]
        [HttpGet]
        public async Task<IActionResult> GetOwnerByApartmentId(int ApartmentId)
        {
            // אם ה-ApartmentId לא תקין, מחזירים BadRequest
            if (ApartmentId <= 0)
            {
                return BadRequest("Invalid apartment ID.");
            }

            // שליפת בעל הדירה
            var owner = await _OwnerService.GetOwnerByApartmentId(ApartmentId);

            // אם לא נמצא בעל דירה
            if (owner == null)
            {
                return NotFound(); // במקרה שאין בעל דירה
            }

            // במקרה של הצלחה, מחזירים את המידע על בעל הדירה
            try
            {
                return Ok(owner);
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
       
        [HttpPut]
        public async Task<IActionResult> UpdateOwnerByApartment(OwnerDTO2 Owner)
        {
            // אם ה-ApartmentId לא תקין, מחזירים BadRequest
            if (Owner.OwnerId==null)
            {
                return BadRequest("Invalid Owner ID.");
            }
            if (Owner.ApartmentId == null)
            {
                return BadRequest("Invalid ApartmentId ID.");
            }
            // שליפת בעל הדירה
            //await _OwnerService.GetOwnerByApartmentId(ApartmentId);


            // במקרה של הצלחה, מחזירים את המידע על בעל הדירה
            try
            {
                return Ok((new { message = "Owner Update successfully." }));
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

    }
}
