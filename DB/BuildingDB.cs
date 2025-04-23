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
            return await _context.Buildings.Where(x=> x.ProjectId == projectId).ToListAsync();
        }

        public async Task<Building> Add(Building Building)
        {
            try
            {
                await _context.Buildings.AddAsync(Building);
                await _context.SaveChangesAsync();

                if (Building != null)
                {
                    _logger.Information("Successfully added a new project.");
                    return Building;
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
        //.Where(b => b.ProjectId == projectId)
        //    .Select(b => b.BuildingNumber)
    }
}