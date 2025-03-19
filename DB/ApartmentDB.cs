using IBEXDATA.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class ApartmentDB : IApartmentDB
    {
        private readonly ILogger<ApartmentDB> _logger;

        private readonly dbContext _context;
        public ApartmentDB(dbContext context, ILogger<ApartmentDB> logger)
        {
            _logger = logger;
            _context = context;
        }


        public IEnumerable<Apartment> GetApartmentsByBuildingId(int buildingId)
        {
            return _context.Apartments.Where(a => a.BuildingId == buildingId).ToList();
        }
        public IEnumerable<Warehouse> GetWarehouseByBuilding(int buildingId)
        {
            return _context.Warehouses.Where(w => w.FatherId == buildingId).ToList();
        }
        public IEnumerable<Parking> GetParkingByBuilding(int buildingId)
        {
            return _context.Parkings.Where(w => w.FatherId == buildingId).ToList();
        }
        public IEnumerable<LinkageCode> GetLinkagCode()
        {
            return _context.LinkageCodes.ToList();
        }
    }
}
