using Common.DTO;
using IBEXDATA.Models;

namespace Service
{
    public interface IBuildingService
    {
        Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId);
        BuildingDTO AddBuilding(BuildingDTO newBuilding);
    }
}