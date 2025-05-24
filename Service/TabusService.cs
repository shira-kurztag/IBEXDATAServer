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
        private readonly IOwnerDB _IOwnerDB;
        private readonly ITabusDB _ITabusDB;
        private readonly IMapper _mapper;
        private readonly ILogger<TabusService> _logger;

        public TabusService( IMapper mapper, IOwnerDB IOwnerDB,  ILogger<TabusService> logger, ITabusDB ITabusDB)
        {
            _mapper = mapper;
            _IOwnerDB = IOwnerDB;
            _logger = logger;
            _ITabusDB = ITabusDB;
        }

        public async Task<TabuDTO> GetTabusByOwnerId(int OwnerId)
        {
            var owner = await _IOwnerDB.GetOwnerById(OwnerId);
            
            var tabus = await _ITabusDB.GetTabusByOwnerId(OwnerId);

            return _mapper.Map<TabuDTO>(tabus);





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
