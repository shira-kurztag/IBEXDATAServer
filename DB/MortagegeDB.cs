using AutoMapper;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class MortagegeDB : IMortagegeDB
    {


        private readonly dbContext _dbContext;
        private readonly IMapper mapper;

        private readonly ILogger<MortagegeDB> _logger;
        public MortagegeDB(dbContext dbContext, IMapper _Mapper, ILogger<MortagegeDB> logger)
        {
            _dbContext = dbContext;
            mapper = _Mapper;
            _logger = logger;

        }

        public async Task<List<MortagegesType>> GetAllMortagegesTypes()
        {
            return await _dbContext.MortagegesTypes.ToListAsync();

        }
        /// <summary>
        /// /קבלת סוגי מטבעות
        /// </summary>
        /// <returns></returns>
        public async Task<List<CurrencyType>> GetAllCurrencyTypes()
        {
            return await _dbContext.CurrencyTypes.ToListAsync();

        }
        /// <summary>
        /// /קבלת דרגות משכנתא
        /// </summary>
        /// <returns></returns>
        public async Task<List<MortagegeLevel>> GetAllMortagegeLevels()
        {
            return await _dbContext.MortagegeLevels.ToListAsync();

        }
    }
}
