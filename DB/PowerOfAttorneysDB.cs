using IBEXDATA.Models;
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

        public async Task AddPower(PowerOfAttorney power)
        {
            await _context.PowerOfAttorneys.AddAsync(power);
        }
    }
}
