using Common.DTO;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class OwnerDB : IOwnerDB
    {
        private readonly dbContext _context;
        private static readonly Serilog.ILogger _logger = Log.ForContext<ProjectDB>(); // Create a logger instance

        public OwnerDB(dbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Owner>> Get()
        {
            try
            {
                var owners = await _context.Owners.ToListAsync();

                if (owners != null)
                {
                    _logger.Information("Successfully get all the owners.");
                    return owners;
                }
                else
                {
                    _logger.Warning("Owner list not loaded successfully.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Get method of Get in OwnerDB.");
                return null;
            }
        }
        public async Task<int> AddOwner(Owner owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner), "Owner cannot be null");
            }

            if (_context == null)
            {
                throw new InvalidOperationException("DbContext is not initialized.");
            }

            await _context.Owners.AddAsync(owner);
            return owner.OwnerId;
        }


        public async Task<Owner> getOwnerByApartmen(int apartmenID)
        {

            Owner owner = await _context.Owners
                                       .FirstOrDefaultAsync(a => a.ApartmentId == apartmenID); await _context.SaveChangesAsync();
            if(owner == null)
            {
                 owner=new Owner();
            }
            return owner;
           
        }
    }
}
