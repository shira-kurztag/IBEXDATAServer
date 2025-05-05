using Common.DTO;
using IBEXDATA.Models;

namespace DB
{
    public interface IBuildingDB
    {
        BuildingDTO AddBuilding(BuildingDTO newBuilding);
        Task<List<Building>> DeleteBuildingByIdAsync(int id);
        Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId);
        Task<List<Building>> GetAllBuilding();
        Task<List<Building>> GetAllBuildingByProject();
    }
}