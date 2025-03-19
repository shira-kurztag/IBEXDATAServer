using IBEXDATA.Models;

namespace Service
{
    public interface ITenantService
    {
        Task<List<Tenant>> GetAllTenants();
        Task<double> GetPartAssetByOwnerTenants(int Id);
        Task<Tenant> GetTenantById(int Id);
    }
}