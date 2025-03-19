using IBEXDATA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _ApartmentService;
        public ApartmentController(IApartmentService ApartmentService)
        {
            _ApartmentService = ApartmentService;
        }


        [Route("GetApartmentsByBuilding/{buildingId}")]
        [HttpGet]
        public IEnumerable<Apartment> GetApartmentsByBuildingId(int buildingId)
        {
            return _ApartmentService.GetApartmentsByBuildingId(buildingId);
        }
        [Route("GetWarehousesByBuilding/{buildingId}")]
        [HttpGet]
        public IEnumerable<Warehouse> GetWarehouseByBuilding(int buildingId)
        {
            return _ApartmentService.GetWarehouseByBuilding(buildingId);
        }
        [Route("GetParkingByBuilding/{buildingId}")]
        [HttpGet]
        public IEnumerable<Parking> GetParkingByBuilding(int buildingId)
        {
            return _ApartmentService.GetParkingByBuilding(buildingId);
        }
        [Route("GetLinkagCode")]
        [HttpGet]
        public IEnumerable<LinkageCode> GetLinkagCode()
        {
            return _ApartmentService.GetLinkagCode();
        }
    }
}
