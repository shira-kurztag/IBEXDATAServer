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
    public class BuildingController : ControllerBase
    {


        private readonly IBuildingService _BuildingService;
    
        private readonly ILogger<BuildingController> _logger;
        private readonly IMapper _mapper;

        public BuildingController(IBuildingService BuildingService, ILogger<BuildingController> logger, IMapper mapper)
        {
           _BuildingService = BuildingService;
           _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetBuildingNumbersByProjectId(int projectId)
        {
            _logger.LogInformation($"Getting building numbers for project ID: {projectId}");
            var buildingNumbers = await _BuildingService.GetBuildingNumbersByProjectId(projectId);

            if (buildingNumbers == null || !buildingNumbers.Any())
            {
              return NotFound();
            }

            var buildingNumbersDTOs = _mapper.Map<List<Building>, List<BuildingDTO>>(buildingNumbers.ToList());
            _logger.LogInformation("Successfully retrieved all buildingNumbers.");


            return Ok(buildingNumbersDTOs);
        }
    }
}
