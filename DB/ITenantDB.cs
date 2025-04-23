using Common.DTO;
using IBEXDATA.Models;

namespace DB
{
    public interface ITenantDB
    {
        Task<List<Tenant>> GetAllTenants();
        Task<List<OwnerTenant>> GetPartAssetByOwnerTenants();
        Task <int> AddTenants (Tenant tenants);
        Task<Apartment> GetTenantsApartment(int ApartmentID);
        Task UpdateTenant(Tenant tenant);



    }
}