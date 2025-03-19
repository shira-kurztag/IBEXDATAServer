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
    public class FareDB : IFareDB
    {
        private readonly ILogger<FareDB> _logger;

        private readonly dbContext _context;
        public FareDB(dbContext context, ILogger<FareDB> logger)
        {
            _logger = logger;
            _context = context;
        }


        public IEnumerable<Fare> GetFares()
        {
            return _context.Fares.ToList();
        }

        public IEnumerable<Fare> DeleteFare(int id)
        {
            Fare? fare = _context.Fares.Find(id);
            _context.Fares.Remove(fare);
            _context.SaveChanges();
            return GetFares();
        }

        public IEnumerable<Fare> UpdateFare(string fareText, int fareId)

        {

            var existingFare = _context.Fares.Find(fareId);

            if (existingFare == null)

            {

                throw new ArgumentException("Fare not found");

            }

            existingFare.FareName = fareText;

            _context.SaveChanges();

            return _context.Fares.ToList();

        }
        public void UpdateFareAmount(int fareId, int fareAmount)
        {
            var existingFare = _context.Fares.Find(fareId);
            if (existingFare == null)
            {
                throw new ArgumentException("Fare not found");
            }
            existingFare.FareAmount = fareAmount;
            _context.SaveChanges();
        }
        public async Task<IEnumerable<Fare>> filterFare(string FareName)
        {

            var p = _context.Fares.Include(e => e.FareId);

            var q = _context.Fares.Where(Fares =>
            (FareName == null ? (true) : (Fares.FareName.Contains(FareName)))

    );

            List<Fare> fare = await q.ToListAsync();

            return fare;
        }


        public IEnumerable<Fare> AddFare(Fare newFare)
        {
            try
            {
                var fare = new Fare
                {
                    FareId = newFare.FareId,
                    FareName = newFare.FareName,
                    FareAmount = newFare.FareAmount,
                };
                _context.Fares.Add(fare);
                _context.SaveChanges();
                return GetFares();
            }
            catch (DbUpdateException ex)
            {
                // Log the error or throw a more specific exception
                throw new InvalidOperationException($"An error occurred while saving the entity changes: {ex.InnerException?.Message}", ex);
            }
        }
    }
}
