using IBEXDATA.Models;

namespace DB
{
    public interface ITenantDB
    {
        Task<List<Tenant>> GetAllTenants();
        Task<List<OwnerTenant>> GetPartAssetByOwnerTenants();
    }
}