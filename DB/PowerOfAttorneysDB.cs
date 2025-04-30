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
    public class PowerOfAttorneysDB: IPowerOfAttorneysDB
    {
        private readonly ILogger<ApartmentDB> _logger;
        private readonly dbContext _context;
        public PowerOfAttorneysDB(dbContext context, ILogger<ApartmentDB> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task <int>AddPower(PowerOfAttorney power)
        {
            await _context.PowerOfAttorneys.AddAsync(power);
            await _context.SaveChangesAsync();
            return power.Id;

        }

        
         public async Task<PowerOfAttorney> GetPowerById(int powerId)
         {
             return await _context.PowerOfAttorneys.FirstOrDefaultAsync(p => p.Id == powerId);
         }

        

        public async Task UpdatePower(PowerOfAttorney Power)
        {

            if (Power == null)
            {
                throw new ArgumentNullException(nameof(Power), "Tenant cannot be null");
            }
            if(Power.Id!=0|| Power.Id != null)
            {

            

            // מצא את הדייר הקיים במסד הנתונים
            var existingPower = await _context.PowerOfAttorneys
                                                 .FirstOrDefaultAsync(a => a.Id == Power.Id);

            if (existingPower == null)
            {
                throw new InvalidOperationException($"Power with ID {Power.Id} not found");
            }
            // עדכן את המאפיינים של הדייר הקיים
            existingPower.PowerOfAttorneyId = Power.PowerOfAttorneyId;
            existingPower.LastName = Power.LastName;
            existingPower.FirstName = Power.FirstName;
            existingPower.IdFileName = Power.IdFileName;
            existingPower.PowerOfAttorneyType = Power.PowerOfAttorneyType;
            existingPower.FromDate = Power.FromDate;
            existingPower.FileName = Power.FileName;
            existingPower.Address = Power.Address;
            existingPower.NumberPhone = Power.NumberPhone;
            existingPower.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
            existingPower.NumberPhone2 = Power.NumberPhone2;
            

            _logger.LogInformation("Power of attorney updated successfully.");
            
            }

        }
    }
}
