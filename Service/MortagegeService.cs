using AutoMapper;
using Common.DTO;
using DB;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MortagegeService : IMortagegeService
    {
        private readonly IMortagegeDB _mortagegeDB;

        private readonly IMapper _mapper;


        public MortagegeService(IMortagegeDB MortagegeDB, IMapper mapper)
        {
            _mortagegeDB = MortagegeDB;
            _mapper = mapper;

        }
        public async Task<List<MortagegesType>> GetAllMortagegesTypes()
        {
            return await _mortagegeDB.GetAllMortagegesTypes();
        }
        public async Task<List<CurrencyType>> GetAllCurrencyTypes()
        {
            return await _mortagegeDB.GetAllCurrencyTypes();

        }
        public async Task<List<MortagegeLevel>> GetAllMortagegeLevels()
        {
            return await _mortagegeDB.GetAllMortagegeLevels();

        }

        public async Task CreateMortagege(MortagegeDTO mortagegeDTO)
        {
            var mortagege = _mapper.Map<Mortagege>(mortagegeDTO);
            var mortgageToTeanant = new MortgageToTeanant
            {
                TeanantId = mortagegeDTO.TeanantId  
            };

            await _mortagegeDB.CreateMortagege(mortagege, mortgageToTeanant);
        }

    }
}
