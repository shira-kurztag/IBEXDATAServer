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

        public async Task<List<TypeMessage>> GetAllTypeMessages()
        {
            return await _dbContext.TypeMessages.ToListAsync();

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
  
        public async Task SaveFullMortgage(Mortagege mortgage, List<MortgageToTeanant > mortgageToTenants)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Retrieve the existing mortgage entity from the database
                    var existingMortgage = await _dbContext.Mortageges.FindAsync(mortgage.MortagegeId);
                    if (existingMortgage == null)
                    {
                        throw new Exception("Mortgage not found.");
                    }

                    // Update only the provided fields
                    var properties = typeof(Mortagege).GetProperties();
                    foreach (var property in properties)
                    {
                        var newValue = property.GetValue(mortgage);
                        if (newValue != null && !newValue.Equals(property.GetValue(existingMortgage)))
                        {
                            property.SetValue(existingMortgage, newValue);
                        }
                    }
;
                    await _dbContext.SaveChangesAsync();


                    foreach (var mortgageToTeanant in mortgageToTenants)
                    {
                        mortgageToTeanant.MortgageId = mortgage.MortagegeId; // קישור למשכנתא הנוכחית
                        await _dbContext.Set<MortgageToTeanant>().AddAsync(mortgageToTeanant);
                    }
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<int> CreateMortagege(Mortagege mortagege)
        {
            await _dbContext.Mortageges.AddAsync(mortagege);
            await _dbContext.SaveChangesAsync();
            return  mortagege.MortagegeId;
        }
    }
}
