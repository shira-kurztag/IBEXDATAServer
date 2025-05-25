using IBEXDATA.Models;

namespace DB
{
    public interface IBuildingDB
    {
        Task<Building> AddBuilding(Building newBuilding);
        Task<List<Building>> DeleteBuildingByIdAsync(int id);
        Task<List<Building>> GetAllBuilding();
        Task<List<Building>> GetAllBuildingByProject();
        Task<Building> GetBuildingNumbers(int buildingId);
        Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId);
        Task<Building> Update(int id, Building building);
    }
}