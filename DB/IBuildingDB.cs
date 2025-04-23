using IBEXDATA.Models;

namespace DB
{
    public interface IBuildingDB
    {
        Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId);
        Task<Building> Add(Building Building);

    }
}