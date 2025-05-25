using Common.DTO;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DB
{
    public class BuildingDB : IBuildingDB
    {
        private readonly dbContext _context;
        private static readonly Serilog.ILogger _logger = Log.ForContext<ProjectDB>(); // Create a logger instance

        public BuildingDB(dbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId)
        {
            _logger.Information($"Fetching building numbers for project ID: {projectId}");
            return await _context.Buildings.Where(x => x.ProjectId == projectId).ToListAsync();
        }
        public async Task<Building> GetBuildingNumbers(int buildingId)
        {
            _logger.Information($"Fetching building numbers for project ID: {buildingId}");
            return _context.Buildings.Where(x => x.BuildingId == buildingId).FirstOrDefault();
        }
        public async Task<Building> AddBuilding(Building newBuilding)
        {
            try
            {
                await _context.Buildings.AddAsync(newBuilding);
                await _context.SaveChangesAsync();

                if (newBuilding != null)
                {
                    _logger.Information("Successfully added a new project.");
                    return newBuilding;
                }
                else
                {
                    _logger.Warning("project was not added.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Add method of Add in ProjectDB.");
                return null;
            }
        }
        public async Task<List<Building>> GetAllBuilding()
        {
            return await _context.Buildings.ToListAsync();
        }
        public async Task<List<Building>> DeleteBuildingByIdAsync(int id)
        {
            var building = await _context.Set<Building>().FindAsync(id);
            if (building == null)
            {
                return null; // Building not found
            }

            _context.Set<Building>().Remove(building);
            await _context.SaveChangesAsync();

            // Return the updated list of buildings
            return await _context.Set<Building>().ToListAsync();
        }
        public async Task<List<Building>> GetAllBuildingByProject()
        {
            return await _context.Buildings.ToListAsync();
        }
        public async Task<Building> Update(int id, Building building)
        {
            try
            {
                //var existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
                //if (existingProject == null)
                //{
                //    _logger.Warning("Project with ID {ProjectId} not found.", id);
                //    return null;
                //}

                building.BuildingId = id;
                _context.Buildings.Update(building);
                await _context.SaveChangesAsync();

                _logger.Information("Successfully updated Project with ID {BuildingId}.", id);
                return building;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Update method of BuildingDB.");
                return null;
            }
        }
    }
}