using Common.DTO;
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
        public BuildingDTO AddBuilding(BuildingDTO newBuilding)
        {
            if (newBuilding == null)
            {
                throw new ArgumentException("Building data cannot be null.", nameof(newBuilding));
            }

            // קריאה לפונקציה ב-Repository
            return _buildingDB.AddBuilding(newBuilding);
        }
    }
}