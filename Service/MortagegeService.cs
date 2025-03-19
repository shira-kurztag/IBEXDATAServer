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



        public MortagegeService(IMortagegeDB MortagegeDB)
        {
            _mortagegeDB = MortagegeDB;

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

    }
}
