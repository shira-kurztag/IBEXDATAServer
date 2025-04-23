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
        ApartmentDTO AddApartmentWithLinkages(int buildingId, ApartmentDTO newLinkagesApartment);

        bool BuildingExists(int buildingId);
    }
}