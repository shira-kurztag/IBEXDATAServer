using IBEXDATA.Models;

namespace DB
{
    public interface IBuildingDB
    {
        Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId);
        Task<string?> GetPurchaseDateByApartmentId(int ApartmentId);
        Task <Building> GetBuildingByApartmentId(int ApartmentId);

    }
}