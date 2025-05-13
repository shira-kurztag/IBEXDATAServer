using Common.DTO;
using IBEXDATA.Models;

namespace DB
{
    public interface IApartmentDB
    {
        IEnumerable<Apartment> GetApartmentsByBuildingId(int buildingId);
        IEnumerable<LinkageCode> GetLinkagCode();
        IEnumerable<Parking> GetParkingByBuilding(int buildingId);
        IEnumerable<Warehouse> GetWarehouseByBuilding(int buildingId);
        Task<List<OwnerTenant>> GetPartAsset(int ApartmentID);
        Task<int> GetTenantsApartment(int ApartmentID);
        Task<Apartment> GetApartmentById(int apartmentId);
       
    }
}