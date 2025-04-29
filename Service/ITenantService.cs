using Common.DTO;
using IBEXDATA.Models;

namespace Service
{
    public interface ITenantService
    {
        Task<List<Tenant>> GetAllTenants();
        Task<double> GetPartAssetByOwnerTenants(int Id);
        Task<Tenant> GetTenantById(int Id);
        Task AddTenants(List<TenantDTO> tenants);
        Task UpdateTenant(List<TenantDTO2> tenants);
        Task<List<TenantDTO2>> GetTenantByApartment(int apartment);
    }
}