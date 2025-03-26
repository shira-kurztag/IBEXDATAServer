using IBEXDATA.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class OwnerTenantDB : IOwnerTenantDB
    {

        private readonly ILogger<ApartmentDB> _logger;

        private readonly dbContext _context;
        public OwnerTenantDB(dbContext context, ILogger<ApartmentDB> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task AddOwnerTenant(OwnerTenant ownerTenant)
        {
            if (ownerTenant == null)
            {
                throw new InvalidOperationException("OwnerTenant not found for the given ApartmentID.");
            }
            await _context.OwnerTenants.AddAsync(ownerTenant);
        }
    }
}
