using Common.DTO;
using IBEXDATA.Models;

namespace DB
{
    public interface ITenantDB
    {
        Task<List<Tenant>> GetAllTenants();
        Task<List<OwnerTenant>> GetPartAssetByOwnerTenants();
        Task  AddTenants (Tenant tenants);
        Task AddPower(PowerOfAttorney Power);
        Task<Apartment> GetTenantsApartment(int ApartmentID);
    }
}