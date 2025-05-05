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
        public BuildingDTO AddBuilding(BuildingDTO newBuilding)
        {
            if (newBuilding == null)
            {
                throw new ArgumentNullException(nameof(newBuilding), "Building data cannot be null.");
            }

            // יצירת אובייקט ישות (Entity) מתוך ה-DTO
            var building = new Building
            {
                BuildingId = newBuilding.BuildingId,
                ProjectId = newBuilding.ProjectId,
                BuildingStatus = newBuilding.BuildingStatus,
                BuildingNumber = newBuilding.BuildingNumber
            };

            // הוספת הישות למסד הנתונים
            _context.Buildings.Add(building);
            _context.SaveChanges();

            // החזרת הישות בתור DTO
            return new BuildingDTO(
                building.BuildingId,
                building.ProjectId,
                building.BuildingStatus,
                building.BuildingNumber
            );
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
    }

}