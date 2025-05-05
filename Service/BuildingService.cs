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


        public async Task<List<Building>> GetAllBuilding()
        {
            return await _buildingDB.GetAllBuilding();
        }
        public async Task<List<Building>> DeleteBuildingByIdAsync(int id)
        {
            return await _buildingDB.DeleteBuildingByIdAsync(id);
        }
        public async Task<Building> GetAllBuildingByProject(int Id)
        {
            var Buildings = await _buildingDB.GetAllBuildingByProject();
            return Buildings.Find(x => x.BuildingId == Id);
        }


    }
}