using AutoMapper;
using Common.DTO;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class TenantDB : ITenantDB
    {

        private readonly dbContext _dbContext;
        private readonly IMapper mapper;

        private readonly ILogger<TenantDB> _logger;
        public TenantDB(dbContext dbContext, IMapper _Mapper, ILogger<TenantDB> logger)
        {
            _dbContext = dbContext;
            mapper = _Mapper;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));


        }



        public async Task<int> AddTenants(Tenant tenants)
        {
            await _dbContext.Tenants.AddAsync(tenants);

            //await _dbContext.SaveChangesAsync();
            return tenants.TenantId;


        }

        public async Task<List<Tenant>> GetAllTenants()

        {
            _logger.LogInformation("Starting UpdateContractor");


            return await _dbContext.Tenants.ToListAsync();
        }

        public async Task<List<OwnerTenant>> GetPartAssetByOwnerTenants()
        {
            return await _dbContext.OwnerTenants.ToListAsync();

        }
        //בדיקה האם קיים דירה מסוימת 
        public async Task<Apartment> GetTenantsApartment(int ApartmentID)
        {
            return await _dbContext.Apartments
                                   .FirstOrDefaultAsync(a => a.ApartmentId == ApartmentID);

        }


        public async Task UpdateTenant(Tenant tenant)
        {
            if (tenant == null)
            {
                throw new ArgumentNullException(nameof(tenant), "Tenant cannot be null");
            }

            // מצא את הדייר הקיים במסד הנתונים
            var existingTenant = await _dbContext.Tenants
                                                 .FirstOrDefaultAsync(a => a.TenantId == tenant.TenantId);

            if (existingTenant == null)
            {
                throw new InvalidOperationException($"Tenant with ID {tenant.TenantId} not found");
            }

            // עדכון כל השדות
            existingTenant.TenantStatus = tenant.TenantStatus;
            existingTenant.TenantIdentity = tenant.TenantIdentity;
            existingTenant.IdentityType = tenant.IdentityType;
            existingTenant.IdentityFromCountry = tenant.IdentityFromCountry;
            existingTenant.LastName = tenant.LastName;
            existingTenant.FirstName = tenant.FirstName;
            existingTenant.IdFileName = tenant.IdFileName;
            existingTenant.IsSignatureByPowerOfAttorney = tenant.IsSignatureByPowerOfAttorney;
            existingTenant.PowerOfAttorneyId = tenant.PowerOfAttorneyId;
            existingTenant.AddressByContract = tenant.AddressByContract;
            existingTenant.UpdateDate = DateOnly.FromDateTime(DateTime.Now); // עדכון תאריך עדכון
            existingTenant.SignedAsTrustee = tenant.SignedAsTrustee;
            existingTenant.PreviousTenantId = tenant.PreviousTenantId;
            existingTenant.Usname = tenant.Usname;
            existingTenant.ThereRrePreviousIdentifyingDetails = tenant.ThereRrePreviousIdentifyingDetails;
            existingTenant.IdentityTypePrevious = tenant.IdentityTypePrevious;
            existingTenant.IdentityFromCountryPrevious = tenant.IdentityFromCountryPrevious;
            existingTenant.TenantIdentityPrevious = tenant.TenantIdentityPrevious;
            existingTenant.PassportExpiredPrevious = tenant.PassportExpiredPrevious;
            existingTenant.OtherPrevious = tenant.OtherPrevious;

            // שמירת השינויים למסד הנתונים
        }
    }
}