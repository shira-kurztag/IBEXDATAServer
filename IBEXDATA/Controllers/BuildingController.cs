using AutoMapper;

using Common.DTO;

using DB;

using IBEXDATA.Models;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

using Service;

using System.Web;

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


        [HttpGet("build/{buildingId}")]

        public async Task<IActionResult> GetBuildingNumbers(int buildingId)

        {

            // פענוח המחרוזת המקודדת

            var decodedBuildingNumber = Uri.UnescapeDataString(buildingId.ToString());

            // לוגים לצורך בדיקות

            _logger.LogInformation($"Encoded building number received: {buildingId}");

            _logger.LogInformation($"Decoded building number: {decodedBuildingNumber}");

            // קריאה לשירות עם המחרוזת המפוענחת

            var buildingNumbers = await _BuildingService.GetBuildingNumbers(buildingId);

            if (buildingNumbers == null)

            {

                return NotFound("No buildings match the given number.");

            }

            // החזרת התוצאה

            return Ok(buildingNumbers);

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

        public async Task<IActionResult> Add([FromBody] BuildingDTO newBuilding)

        {

            if (!ModelState.IsValid)

            {

                return BadRequest(ModelState);

            }

            var newBuilding1 = _mapper.Map<BuildingDTO, Building>(newBuilding);

            var projto = await _BuildingService.Add(newBuilding1);

            if (projto != null)

            {

                _logger.LogInformation("Successfully added Project: {ProjectName}", newBuilding.BuildingId);

                BuildingDTO newProj = _mapper.Map<Building, BuildingDTO>(projto);

                return CreatedAtAction(nameof(GetBuildingNumbers), new { BuildingId = newProj.BuildingId }, newProj);

            }

            _logger.LogWarning("Failed to add the Project: {ProjectName}", newBuilding.BuildingId);

            return BadRequest($"The Project {newBuilding.BuildingId} not successfully added");

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

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] BuildingDTO build)

        {

            if (!ModelState.IsValid)

            {

                return BadRequest(ModelState);

            }

            var bui = _mapper.Map<BuildingDTO, Building>(build);

            var updatedBuilding = await _BuildingService.Update(id, bui);

            if (updatedBuilding != null)

            {

                _logger.LogInformation("Successfully updated Bank with ID: {BankId}", id);

                var updatedBuildDTO = _mapper.Map<Building, BuildingDTO>(updatedBuilding);

                return Ok(updatedBuildDTO);

            }

            _logger.LogWarning("Failed to update Bank with ID: {BankId}", id);

            return NotFound($"Bank with ID {id} not found");

        }

    }

}

