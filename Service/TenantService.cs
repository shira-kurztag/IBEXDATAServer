using DB;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TenantService : ITenantService
    {



        private readonly ITenantDB _tenantDB;



        public TenantService(ITenantDB TenantRepository)
        {
            _tenantDB = TenantRepository;

        }
        public async Task<List<Tenant>> GetAllTenants()
        {
            return await _tenantDB.GetAllTenants();
        }
        public async Task<Tenant> GetTenantById(int Id)
        {
            var contractors = await _tenantDB.GetAllTenants();
            return contractors.Find(x => x.TenantId == Id);
        }



        public async Task<double> GetPartAssetByOwnerTenants(int Id)
        {
            var OwnerTenants = await _tenantDB.GetPartAssetByOwnerTenants();
            var curOwnerTenants = OwnerTenants.Find(x => x.TenantId == Id);

            if (curOwnerTenants == null || curOwnerTenants.PartAsset == null)
            {
                throw new InvalidOperationException("Tenant not found or PartAsset is null.");
            }

            return curOwnerTenants.PartAsset.Value;
        }

    }
}
