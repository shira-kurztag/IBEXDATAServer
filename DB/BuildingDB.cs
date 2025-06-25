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

        public async Task<string?> GetPurchaseDateByApartmentId(int apartmentId)
        {
            _logger.Information($"Fetching purchase date for apartment ID: {apartmentId}");

            if (apartmentId <= 0)
            {
                throw new ArgumentException("Invalid apartment ID.");
            }

            var apartment = await _context.Apartments.FirstOrDefaultAsync(x => x.ApartmentId == apartmentId);
            if (apartment == null)
            {
                throw new ApplicationException($"No apartment found with ID: {apartmentId}");
            }

            var building = await _context.Buildings.FirstOrDefaultAsync(x => x.BuildingId == apartment.BuildingId);
            if (building == null)
            {
                throw new ApplicationException($"No building found for apartment ID: {apartmentId}");
            }

            return building.AddressAndNumberOfMunicipal;
        }

        public  async Task<Building> GetBuildingByApartmentId(int ApartmentId)
        {

            var building = await (from a in _context.Apartments
                                  join b in _context.Buildings on a.BuildingId equals b.BuildingId
                                  where a.ApartmentId == ApartmentId
                                  select b).FirstOrDefaultAsync();

            if (building == null)
            {
                throw new InvalidOperationException($"No building found for the given ApartmentId: {ApartmentId}.");
            }
            return building;

        }
}