using AutoMapper;
using IBEXDATA.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task SaveFullMortgage(Mortagege mortgage, List<MortgageToTeanant> mortgageToTenants)
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
            return mortagege.MortagegeId;
        }

        public async Task<bool> HasMortgageInProcess(int apartmentId)
        {
            return await _dbContext.Mortageges
                .Join(_dbContext.MortgageToTeanants,
                      mortgage => mortgage.MortagegeId,
                      mortgageToTenant => mortgageToTenant.MortgageId, 
                      (mortgage, mortgageToTenant) => new { mortgage, mortgageToTenant })
                .Join(_dbContext.Tenants,
                      combined => combined.mortgageToTenant.TeanantId, 
                      tenant => tenant.TenantId,
                      (combined, tenant) => new { combined.mortgage, tenant })
                .Join(_dbContext.OwnerTenants,
                      combined => combined.tenant.TenantId,
                      ownerTenant => ownerTenant.TenantId,
                      (combined, ownerTenant) => new { combined.mortgage, ownerTenant })
                .Join(_dbContext.Owners,
                      combined => combined.ownerTenant.OwnerId,
                      owner => owner.OwnerId,
                      (combined, owner) => new { combined.mortgage, owner })
                .Join(_dbContext.Apartments,
                      combined => combined.owner.ApartmentId,
                      apartment => apartment.ApartmentId,
                      (combined, apartment) => new { combined.mortgage, apartment })
                .AnyAsync(result => result.apartment.ApartmentId == apartmentId &&
                                    result.mortgage.MortagegeStatus == 1);
        }
        public async Task<long> createBankCertificate(BankCertificate bankCertificate)
        {
            await _dbContext.BankCertificates.AddAsync(bankCertificate);
            await _dbContext.SaveChangesAsync();
            return bankCertificate.BankCertificatesId;

        }



        public async Task UpdateBankCertificate(BankCertificate bankCertificate)
        {
          
            var existingCertificate = await _dbContext.BankCertificates.FindAsync(bankCertificate.BankCertificatesId);
            if (existingCertificate != null)
            {
              
                _dbContext.Entry(existingCertificate).CurrentValues.SetValues(bankCertificate);

              
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("BankCertificate not found in the database.");
            }
        }




        public async Task<List<int>> GetAllMortgageBanksByApartment(int apartmentId)
        {
            return await _dbContext.Mortageges
                .Join(_dbContext.MortgageToTeanants,
                      mortgage => mortgage.MortagegeId,
                      mortgageToTenant => mortgageToTenant.MortgageId,
                      (mortgage, mortgageToTenant) => new { mortgage, mortgageToTenant })
                .Join(_dbContext.Tenants,
                      combined => combined.mortgageToTenant.TeanantId,
                      tenant => tenant.TenantId,
                      (combined, tenant) => new { combined.mortgage, tenant })
                .Join(_dbContext.OwnerTenants,
                      combined => combined.tenant.TenantId,
                      ownerTenant => ownerTenant.TenantId,
                      (combined, ownerTenant) => new { combined.mortgage, ownerTenant })
                .Join(_dbContext.Owners,
                      combined => combined.ownerTenant.OwnerId,
                      owner => owner.OwnerId,
                      (combined, owner) => new { combined.mortgage, owner })
                .Where(result => result.owner.ApartmentId == apartmentId && result.mortgage.ToTheBank.HasValue)
                .Select(result => result.mortgage.ToTheBank.Value)
                .Distinct()
                .ToListAsync();
        }
    }
}
