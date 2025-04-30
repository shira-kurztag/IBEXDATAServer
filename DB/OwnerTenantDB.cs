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

        public async Task<List<OwnerTenant>> GetOwnerTenantBytenantId(int tenantId)
        {
            if (tenantId == 0)
            {
                throw new InvalidOperationException("Tenant not found.");
            }

            // שליפת רשימת OwnerTenants לפי TenantId
            var ownerTenantsByTenantId = await _context.OwnerTenants
                .Where(a => a.TenantId == tenantId)
                .ToListAsync();

            if (ownerTenantsByTenantId == null || !ownerTenantsByTenantId.Any())
            {
                throw new InvalidOperationException("No OwnerTenants found for the given TenantId.");
            }

            // שליפת כל ה-OwnerId מתוך התוצאה הקודמת
            var ownerIds = ownerTenantsByTenantId.Select(o => o.OwnerId).Distinct().ToList();

            // שליפת כל OwnerTenants נוספים לפי ה-OwnerId שמצאנו קודם
            var allOwnerTenants = await _context.OwnerTenants
                .Where(o => ownerIds.Contains(o.OwnerId))
                .ToListAsync();

            return allOwnerTenants;
        }

        public async Task Delete(int TenantId)
        {
            // מצא את הרשומות שברצונך למחוק
            var tenantsToDelete = await _context.OwnerTenants
                .Where(a => a.TenantId == TenantId)
                .ToListAsync();

            if (tenantsToDelete == null || !tenantsToDelete.Any())
            {
                throw new InvalidOperationException("No tenants found with the specified TenantId.");
            }
            _context.OwnerTenants.RemoveRange(tenantsToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
