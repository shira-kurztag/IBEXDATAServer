using IBEXDATA.Models;
using Microsoft.AspNetCore.Mvc;
using Common.DTO;
using Service;
using AutoMapper;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _BankService;
        private readonly ILogger<BankController> _logger;
        private readonly IMapper _mapper;

        public BankController(IBankService BankService, ILogger<BankController> logger, IMapper mapper)
        {
            _BankService = BankService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var banks = await _BankService.Get();
            if (banks != null)
            {
                //var bankDTOs = _mapper.Map<List<Bank>, List<BankDTO>>(banks.ToList());
                _logger.LogInformation("Successfully retrieved all banks.");
                //return Ok(bankDTOs);
                return Ok(banks);
            }

            _logger.LogWarning("Failed to retrieve banks.");
            return BadRequest();
        }

        [Route("GetNames")]
        [HttpGet]
        public async Task<IActionResult> GetNames()
        {
            var banks = await _BankService.GetNames();

            if (banks != null)
            {
                var bankDTOs = _mapper.Map<List<Bank>, List<BankNamesDTO>>(banks.ToList());
                _logger.LogInformation("Successfully retrieved all banks.");
                return Ok(bankDTOs);
            }

            _logger.LogWarning("Failed to retrieve banks.");
            return BadRequest();
        }

/*        [Route("GetFilter")]
        [HttpGet]
        public async Task<IEnumerable<Bank>> filterBank(string LastNameBank)
        {

            return await _BankService.filterBank(LastNameBank);
        }*/

        [Route("GetBanks")]
        [HttpGet]
        public IEnumerable<Bank> GetBanks()
        {
            return _BankService.GetBanks();
        }

        [HttpPost]
        public ActionResult<IEnumerable<Bank>> AddBank([FromBody] Bank bank)
        {
            if (bank == null)
            {
                return BadRequest("Bank is null.");
            }

            var bank2 = _BankService.AddBank(bank);
            return Ok(bank2);
        }

        [HttpDelete("{Id}")]
        public IActionResult deleteBank(int Id)
        {
            var gift = _BankService.DeleteBankById(Id);
            return Ok(gift);
        }

        //public IActionResult UpdateBank([FromRoute] int bankId, [FromBody] BankDTO bank)
        //{
        //    try
        //    {
        //        var updatedBanks = _BankService.UpdateBank(bank, bankId);
        //        return Ok(updatedBanks);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BankDTO bank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proj = _mapper.Map<BankDTO, Bank>(bank);
            var updatedBank = await _BankService.Update(id, proj);

            if (updatedBank != null)
            {
                _logger.LogInformation("Successfully updated Bank with ID: {BankId}", id);
                var updatedBanktDTO = _mapper.Map<Bank,BankDTO>(updatedBank);
                return Ok(updatedBanktDTO);
            }

            _logger.LogWarning("Failed to update Bank with ID: {BankId}", id);
            return NotFound($"Bank with ID {id} not found");
        }



        //[Route("GetNameBankById")]
        //[HttpGet]
        //public async Task<IActionResult> GetNameBankById([FromQuery] int id)
        //{
        //    var bank = await _BankService.GetNameBankById(id);
        //    if (bank == null)
        //    {
        //        return NotFound();
        //    }

        //   // var bankDTO = _mapper.Map<Contractor, ContractorDTO>(bank);
        //    return Ok(bank);
        //}
    }
}