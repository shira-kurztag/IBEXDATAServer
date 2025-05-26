using AutoMapper;
using Common.DTO;
using DB;
using IBEXDATA.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TabusService : ITabusService
    {
        private readonly IApartmentDB _IApartmentDB;
        private readonly IFareDB _IFareDB;
        private readonly IOwnerDB _IOwnerDB;
        private readonly ITabusDB _ITabusDB;
        private readonly IMapper _mapper;
        private readonly BuildingDB _IBuildingDB;
        private readonly ProjectDB _IProjectDB;
        private readonly ILogger<TabusService> _logger;

        public TabusService(BuildingDB IBuildingDB,IMapper mapper, IOwnerDB IOwnerDB,  ILogger<TabusService> logger, ITabusDB ITabusDB, IApartmentDB IApartmentDB, IFareDB IFareDB, ProjectDB IProjectDB)
        {
            _mapper = mapper;
            _IFareDB = IFareDB;
            _IOwnerDB = IOwnerDB;
            _logger = logger;
            _ITabusDB = ITabusDB;
            _IApartmentDB = IApartmentDB;
            _IProjectDB = IProjectDB;
            _IBuildingDB = IBuildingDB;
        }

        public async Task<TabuDTO> GetTabusByApartmentId(int ApartmentId)
        {
            var Apartment = await _IApartmentDB.GetTenantsApartment(ApartmentId);
            if (Apartment == null)
            {
                throw new InvalidOperationException($"No apartment found for the given ApartmentId: {ApartmentId}.");
            }
            var fareList = await _IFareDB.filterFare("אגרת משכנתא");
            var fare = fareList.FirstOrDefault(); 

            var tabus = await _ITabusDB.GetTabusByApartmentId(ApartmentId);
            var tabuDTO = _mapper.Map<TabuDTO>(tabus);

                tabuDTO.FareName = fare.FareName ?? "";
                tabuDTO.FareAmount = fare.FareAmount ?? 0;

           
            var project = await _IProjectDB.GetBlocAndSmoothByProjectId(ApartmentId);
            tabuDTO.Bloc = project.Bloc ?? 0;
            tabuDTO.Smooth = project.Smooth ?? 0;
            var building = await _IBuildingDB.GetBuildingByApartmentId(ApartmentId);
            tabuDTO.SmothArea = building.SmothArea ?? 0;

            return tabuDTO;

        }

       

        public async Task UpdateTabusByOwnerId(TabuDTO tabu)
        {
            if (tabu == null)
            {
                throw new ArgumentNullException(nameof(tabu), "Tabu cannot be null.");
            }
            if( tabu.TabuId==0)
            {
                throw new ArgumentOutOfRangeException(nameof(tabu.TabuId), "tabuId must be a positive integer.");
            }
            if (tabu.OwnerId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tabu.OwnerId), "OwnerId must be a positive integer.");
            }
            await _ITabusDB.UpdateTabusByOwnerId(tabu);
            
        }
    }
}
