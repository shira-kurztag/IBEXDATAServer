using Common.DTO;
using IBEXDATA.Models;

namespace Service
{
    public interface IBuildingService
    {
        Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId);
        BuildingDTO AddBuilding(BuildingDTO newBuilding);
        Task<List<Building>> GetAllBuilding();
        Task<List<Building>> DeleteBuildingByIdAsync(int id);
        Task<Building> GetAllBuildingByProject(int Id);


    }
}