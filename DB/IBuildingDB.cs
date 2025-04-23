using Common.DTO;
using IBEXDATA.Models;

namespace DB
{
    public interface IBuildingDB
    {
        Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId);
        BuildingDTO AddBuilding(BuildingDTO building);

    }
}