
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IApartmentService
    {
        IEnumerable<Apartment> GetApartmentsByBuildingId(int buildingId);
        IEnumerable<Warehouse> GetWarehouseByBuilding(int buildingId);
        IEnumerable<Parking> GetParkingByBuilding(int buildingId);
        IEnumerable<LinkageCode> GetLinkagCode();
    }
}
