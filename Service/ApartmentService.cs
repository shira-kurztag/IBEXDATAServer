using DB;
using IBEXDATA.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ApartmentService : IApartmentService
    {
        public readonly IApartmentDB _ApartmentDB;


        public ApartmentService(IApartmentDB ApartmentDB)
        {
            _ApartmentDB = ApartmentDB;

        }

        public IEnumerable<Apartment> GetApartmentsByBuildingId(int buildingId)
        {
            return _ApartmentDB.GetApartmentsByBuildingId(buildingId);
        }
        public IEnumerable<Warehouse> GetWarehouseByBuilding(int buildingId)
        {
            return _ApartmentDB.GetWarehouseByBuilding(buildingId);
        }
        public IEnumerable<Parking> GetParkingByBuilding(int buildingId)
        {
            return _ApartmentDB.GetParkingByBuilding(buildingId);
        }
        public IEnumerable<LinkageCode> GetLinkagCode()
        {
            return _ApartmentDB.GetLinkagCode();
        }
        // פונקציה שבודקת האם יש לדיירה מסוימת חלק בנכס
        public async Task<double> GetPartAssetApartmenID(int ApartmenID)
        {
            
            List<OwnerTenant> ownerTenants = await _ApartmentDB.GetPartAsset(ApartmenID);
            double curPartAsset = 0;
            if (ownerTenants == null||ownerTenants.Count==0)
            {
                curPartAsset = 0;
            }
            else
            {
                foreach (var item in ownerTenants)
                {
                    curPartAsset += item.PartAsset ?? 0;

                }
            }

            Console.WriteLine(curPartAsset);
            return curPartAsset;
        }

    }
}
