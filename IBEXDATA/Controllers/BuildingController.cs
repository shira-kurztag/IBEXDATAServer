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

        [HttpPost]

        [Route("AddBuilding")]

        public ActionResult<BuildingDTO> AddBuilding([FromBody] BuildingDTO newBuilding)

        {

            try

            {

                if (newBuilding == null)

                {

                    return BadRequest("Building data cannot be null.");

                }

                var addedBuilding = _BuildingService.AddBuilding(newBuilding);

                return Ok(addedBuilding);

            }

            catch (Exception ex)

            {

                // הדפסת השגיאה ללוג

                Console.WriteLine($"Error occurred in Controller: {ex.Message}");

                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                return StatusCode(500, "An error occurred while adding the building.");

            }

        }

        [HttpGet]

        public async Task<IActionResult> GetAllBuilding()

        {

            var contractors = await _BuildingService.GetAllBuilding();

            if (contractors == null || !contractors.Any())

            {

                return NotFound("No contractors found.");

            }

            return Ok(contractors);

        }

        //[Route("GetBuildingById")]

        //[HttpGet]

        //public async Task<IActionResult> GetAllBuildingByProject([FromQuery] int buildingId)

        //{

        //    var Buildings = await _BuildingService.GetAllBuildingByProject(buildingId);

        //    return Ok(Buildings);

        //}

        [HttpGet("getBuildingsByProject")]

        public async Task<IActionResult> GetBuildingsByProject([FromQuery] int id)

        {

            _logger.LogInformation($"Getting buildings for project ID: {id}");

            var buildings = await _BuildingService.GetBuildingNumbersByProjectId(id);

            if (buildings == null || !buildings.Any())

            {

                return NotFound("No buildings found for the specified project ID.");

            }

            var buildingDTOs = _mapper.Map<List<Building>, List<BuildingDTO>>(buildings.ToList());

            _logger.LogInformation("Successfully retrieved buildings.");

            return Ok(buildingDTOs);

        }

    }

}
