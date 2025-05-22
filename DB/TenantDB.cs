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
using AutoMapper;

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

        public async Task Delete(int tenantId)
        {
            var tenant = await _dbContext.Tenants.FindAsync(tenantId); // שימוש ב-FindAsync
            if (tenant != null)
            {
                _dbContext.Tenants.Remove(tenant); // אין צורך ב-await כאן
                await _dbContext.SaveChangesAsync(); // שימוש ב-SaveChangesAsync
            }
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

        public async Task <TenantDTO2> GetTenantById(int ownerTenant1)
        {
            var t= await _dbContext.Tenants.Where(a => a.TenantId == ownerTenant1)
                .Select(a => new TenantDTO2
                {
                    TenantId = a.TenantId,
                    LastName= a.LastName,
                    FirstName = a.FirstName,
                    TenantIdentity = a.TenantIdentity,
                    IdentityType = a.IdentityType,
                    TenantStatus = a.TenantStatus,
                    IdFileName = a.IdFileName,

                    IdentityFromCountry = a.IdentityFromCountry,
                    Usname = a.Usname,
                    PreviousTenantId = a.PreviousTenantId,
                    IdentityTypePrevious = a.IdentityTypePrevious,
                    TenantIdentityPrevious = a.TenantIdentityPrevious,
                    OtherPrevious = a.OtherPrevious,

                    IsSignatureByPowerOfAttorney = a.IsSignatureByPowerOfAttorney,
                    PowerOfAttorneyId = a.PowerOfAttorneyId,
                
                }).FirstOrDefaultAsync();

            ///אם הדייר לא קיים אז צריך שיחזיר אוביקט ריק 
            if (t == null)
            {
                throw new InvalidOperationException($"Tenant with ID {ownerTenant1} not found.");
            }
            return t;
//        public double? PartAsset { get; set; } // חלק בנכס
//        public int ApartmentId { get; set; }//דירה מויממת לדייר
//        public int powerId { get; set; }
    }

        public async Task<Tenant> GetTenantById1(int tenantId)
        {
            var t=await _dbContext.Tenants
                .FirstOrDefaultAsync(a => a.TenantId == tenantId);
            if (t == null)
            {
                throw new InvalidOperationException($"Tenant with ID {tenantId} not found.");
            }
            return t;
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