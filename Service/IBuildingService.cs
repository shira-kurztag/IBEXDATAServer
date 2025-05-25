using IBEXDATA.Models;

namespace Service
{
    public interface IBuildingService
    {
        Task<Building> Add(Building newBuilding);
        Task<List<Building>> DeleteBuildingByIdAsync(int id);
        Task<List<Building>> GetAllBuilding();
        Task<Building> GetAllBuildingByProject(int Id);
        Task<Building> GetBuildingNumbers(int buildingId);
        Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId);
        Task<Building> Update(int id, Building building);
    }
}