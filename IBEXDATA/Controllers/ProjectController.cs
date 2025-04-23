using IBEXDATA.Models;
using Microsoft.AspNetCore.Mvc;
using Common.DTO;
using Service;
using AutoMapper;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _ProjectService;
        private readonly ILogger<ProjectController> _logger;
        private readonly IMapper _mapper;


        public ProjectController(IProjectService ProjectService, ILogger<ProjectController> logger, IMapper mapper)
        {
            _ProjectService = ProjectService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContractors()
        {
            var contractors = await _ProjectService.GetAllContractors(); // השגת כל הקבלנים
            if (contractors == null || !contractors.Any())
            {
                return NotFound("No contractors found.");
            }

            return Ok(contractors);
        }


        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProjectCreateDTO Project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proj = _mapper.Map<ProjectCreateDTO, Project>(Project);

            var projto = await _ProjectService.Add(proj);

            if (projto != null)
            {
                _logger.LogInformation("Successfully added Project: {ProjectName}", Project.ProjectName);

                ProjectDTO newProj = _mapper.Map<Project, ProjectDTO>(projto);
                return CreatedAtAction(nameof(GetById), new { id = newProj.ProjectId }, newProj);
            }

            _logger.LogWarning("Failed to add the Project: {ProjectName}", Project.ProjectName);
            return BadRequest($"The Project {Project.ProjectName} not successfully added");
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var proj = await _ProjectService.GetById(id);
            if (proj == null)
            {
                return NotFound();
            }

            var projDTO = _mapper.Map<Project, ProjectDTO>(proj);
            return Ok(projDTO);
        }

        [Route("GetCompanyById")]
        [HttpGet]
        public async Task<IActionResult> GetCompanyById([FromQuery] int id)
        {
            var cont = await _ProjectService.GetCompanyById(id);
            if (cont == null)
            {
                return NotFound();
            }

            var contDTO = _mapper.Map<Contractor, ContractorDTO>(cont);
            return Ok(contDTO);
        }

        [HttpGet("byname/{projectName}")]
        public async Task<ActionResult<ProjectDTO>> GetByName(string projectName)
        {
            var proj = await _ProjectService.GetByName(projectName);
            if (proj == null)
            {
                return NotFound();
            }

            var projDTO = _mapper.Map<Project, ProjectDTO>(proj);
            return Ok(projDTO);
        }

        [Route("GetProjecctByContractor")]
        [HttpGet]
        public async Task<IActionResult> GetProjecctByContractor([FromQuery] int id)
        {
            var proj = await _ProjectService.GetProjecctByContractor(id);
            if (proj == null)
            {
                return NotFound();
            }

            var projDTO = _mapper.Map<List<Project>, List<ProjectCreateDTO>>(proj);
            return Ok(projDTO);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] ProjectCreateDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var proj = _mapper.Map<ProjectCreateDTO, Project>(project);
            var updatedProject = await _ProjectService.Update(id, proj);

            if (updatedProject != null)
            {
                _logger.LogInformation("Successfully updated Project with ID: {ProjectId}", id);
                var updatedProjectDTO = _mapper.Map<Project, ProjectCreateDTO>(updatedProject);
                return Ok(updatedProjectDTO);
            }

            _logger.LogWarning("Failed to update Project with ID: {ProjectId}", id);
            return NotFound($"Project with ID {id} not found");
        }
        [Route("GetBuilding")]
        [HttpGet]
        public async Task<IActionResult> GetBuildingAsync()
        {
            try
            {
                var building = await _ProjectService.GetBuildingAsync(); // קריאה נכונה לפונקציה
                return Ok(building); // החזרה תקינה של הנתונים
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // טיפול בשגיאות
            }
        }
    }
}
