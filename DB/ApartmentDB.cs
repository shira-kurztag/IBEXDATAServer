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
            try
            {
                // הדפסת הנתונים שנשלחים
                Console.WriteLine($"Adding apartment with data: {newLinkagesApartment}");

                // הוספת הדירה החדשה
                Apartment apartment = _mapper.Map<Apartment>(newLinkagesApartment);
                _context.Set<Apartment>().Add(apartment);
                _context.SaveChanges();

                // קבלת המזהה של הדירה החדשה
                int newApartmentId = apartment.ApartmentId;

                // החזרת הדירה החדשה יחד עם ההצמדות
                var res = _context.Set<Apartment>()
                               .Include(a => a.LinkagesApartments)
                               .Where(a => a.ApartmentId == newApartmentId)
                               .FirstOrDefault();

                return _mapper.Map<ApartmentDTO>(res);
            }
            catch (DbUpdateException ex)
            {
                // הדפסת ההודעה הפנימית של השגיאה
                Console.WriteLine($"DbUpdateException error: {ex.InnerException?.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // הדפסת שגיאות כלליות
                Console.WriteLine($"Exception error: {ex.Message}");
                throw;
            }
        }
    }
}