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
    public class OwnerTenantDB : IOwnerTenantDB
    {

        private readonly ILogger<ApartmentDB> _logger;

        private readonly dbContext _context;
        public OwnerTenantDB(dbContext context, ILogger<ApartmentDB> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<int>> GetOwnerTenantByOwnerId(int OwnerId)
        {
            var TenantsID = await _context.OwnerTenants
                .Where(ot => ot.OwnerId == OwnerId)
                .Select(ot => ot.TenantId)
                .ToListAsync();

            if (TenantsID == null || !TenantsID.Any())
                throw new InvalidOperationException($"No tenants found for the given OwnerId: {OwnerId}.");

            return TenantsID;
        }
        public async Task AddOwnerTenant(OwnerTenant ownerTenant)
        {
            if (ownerTenant == null)
            {
                throw new InvalidOperationException("OwnerTenant not found for the given ApartmentID.");
            }
            await _context.OwnerTenants.AddAsync(ownerTenant);
        }

        public async Task UpdateOwnerTenant(OwnerTenant OwnerTenant)
        {
            if (OwnerTenant == null)
            {
                throw new InvalidOperationException("OwnerTenant not found for the given ApartmentID.");
            }
            // Find the existing OwnerTenant in the database
       
            var existingOwnerTenant = await _context.OwnerTenants
                                        .FirstOrDefaultAsync(a => a.TenantId == OwnerTenant.TenantId);
            if (existingOwnerTenant == null)
            {
                throw new InvalidOperationException("OwnerTenant not found for the given ApartmentID.");

            }
            // Update the properties of the existing OwnerTenant
            existingOwnerTenant.PartAsset = OwnerTenant.PartAsset;
            existingOwnerTenant.UpdateDate = DateOnly.FromDateTime(DateTime.Now);


        }

    }
}
