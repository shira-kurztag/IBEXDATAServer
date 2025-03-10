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


        //.Where(b => b.ProjectId == projectId)
        //    .Select(b => b.BuildingNumber)
    }
}