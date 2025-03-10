using DB;
using IBEXDATA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingDB _buildingDB;

        public BuildingService(IBuildingDB buildingDB)
        {
            _buildingDB = buildingDB;
        }

        public async Task<IEnumerable<Building>> GetBuildingNumbersByProjectId(int projectId)
        {
            return await _buildingDB.GetBuildingNumbersByProjectId(projectId);
        }
    }
}