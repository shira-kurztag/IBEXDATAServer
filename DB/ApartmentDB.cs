using Common.DTO;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<OwnerTenant>> GetPartAsset(int ApartmentID)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(a => a.ApartmentId == ApartmentID);
            if (apartment==null)
            {
                throw new InvalidOperationException("Apartment not found for the given ApartmentID.");

            }
            var owner = await _context.Owners.FirstOrDefaultAsync(a => a.ApartmentId == ApartmentID);
            int OwnerId = owner?.OwnerId ?? -1;
            if (OwnerId == -1)
            {
                return new List<OwnerTenant>(); // החזר רשימה ריקה אם לא נמצא בעלים

            }
            List<OwnerTenant> ownerTenants = await _context.OwnerTenants.Where(a => a.OwnerId == OwnerId).ToListAsync();

            return ownerTenants;


        }

        public async Task<int> GetTenantsApartment(int ApartmentID)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(a => a.ApartmentId == ApartmentID);
            if (apartment == null||apartment.ApartmentId==0)
            {
                throw new InvalidOperationException("Apartment not found for the given ApartmentID.");

            }
            return apartment.ApartmentId;
        }

        public async Task<Apartment> GetApartmentById(int apartmentId)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(a => a.ApartmentId == apartmentId);
            if (apartment == null)
            {
                throw new InvalidOperationException("Apartment not found for the given ApartmentID.");
            }
            return apartment;
        }

        
    }
}
