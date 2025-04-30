using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public interface IOwnerTenantDB
    {
        Task AddOwnerTenant(OwnerTenant OwnerTenant);
        Task UpdateOwnerTenant(OwnerTenant OwnerTenant);
        Task <List<int>> GetOwnerTenantByOwnerId( int OwnerId);
        Task<List<OwnerTenant>> GetOwnerTenantBytenantId(int tenantId);
        Task Delete(int TenantId);
    }
}
