using AutoMapper;
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
        private readonly IMapper _mapper;


        private readonly dbContext _context;
        public ApartmentDB(dbContext context, ILogger<ApartmentDB> logger, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
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

        public ApartmentDTO AddApartmentWithLinkages(int buildingId, ApartmentDTO newLinkagesApartment)
        {
            // הוספת הדירה החדשה
            Apartment apartment = _mapper.Map<Apartment>(newLinkagesApartment);
            _context.Set<Apartment>().Add(apartment);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }

            // קבלת המזהה של הדירה החדשה
            int newApartmentId = apartment.ApartmentId;

            // החזרת הדירה החדשה יחד עם ההצמדות
            var res = _context.Set<Apartment>()
                           .Include(a => a.LinkagesApartments)
                           .Where(a => a.ApartmentId == newApartmentId)
                           .FirstOrDefault();

            return _mapper.Map<ApartmentDTO>(res);
        }
        public bool BuildingExists(int buildingId)
        {
            return _context.Buildings.Any(b => b.BuildingId == buildingId);
        }
    }

}
