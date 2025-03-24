using DB;
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OwnerTenantService: IOwnerTenantService
    {

        private readonly IOwnerTenantDB _ownerTenantRepository;
        private readonly ITenantService _tenantService;


        public OwnerTenantService(IOwnerTenantDB ownerTenantRepository, ITenantService tenantService)
        {
            _ownerTenantRepository = ownerTenantRepository;
            _tenantService = tenantService;
        }
        public async Task<List<Tenant>> GetAllownerTenantByOwners(List<Owner> owners)
        {
           var allOwnersTenants = await _ownerTenantRepository.GetOwnersTeants();
            var tenantIds = allOwnersTenants
           .Where(ot => owners.Any(o => o.OwnerId == ot.OwnerId))
           .Select(ot => ot.TenantId)
           .Distinct()
           .ToList();

            // קבלת כל ה-Tenants לפי TenantId
            var tenants = await _tenantService.GetTenantsByIds(tenantIds);

            return tenants;
        }
    }
}
