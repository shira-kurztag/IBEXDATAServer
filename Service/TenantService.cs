using AutoMapper;
using Common.DTO;
using DB;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class TenantService : ITenantService
    {
        private readonly ITenantDB _tenantDB;
        private readonly IOwnerDB _IOwnerDB;
        private readonly IOwnerTenantDB _IOwnerTenantDB;
        public readonly IApartmentDB _IApartmentDB;
        public readonly IPowerOfAttorneysDB _IPowerOfAttorneysDB;
        private readonly IMapper _mapper;
        private readonly dbContext _dbContext;
        private readonly ILogger<TenantService> _logger;
        private List<int> _list = new List<int>();
        private int _ownerId = 0;

        public TenantService(ITenantDB TenantRepository, IMapper mapper, IOwnerDB IOwnerDB, IApartmentDB IApartmentDB, IPowerOfAttorneysDB IPowerOfAttorneysDB, IOwnerTenantDB IOwnerTenantDB, dbContext dbContext, ILogger<TenantService> logger)
        {
            _tenantDB = TenantRepository;
            _mapper = mapper;
            _IApartmentDB = IApartmentDB;
            _IOwnerDB = IOwnerDB;
            _IPowerOfAttorneysDB = IPowerOfAttorneysDB;
            _IOwnerTenantDB = IOwnerTenantDB;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<Tenant>> GetAllTenants()
        {
            return await _tenantDB.GetAllTenants();
        }

        public async Task<Tenant> GetTenantById(int Id)
        {
            var contractors = await _tenantDB.GetAllTenants();
            return contractors.Find(x => x.TenantId == Id);
        }

        public async Task<double> GetPartAssetByOwnerTenants(int Id)
        {
            var OwnerTenants = await _tenantDB.GetPartAssetByOwnerTenants();
            var curOwnerTenants = OwnerTenants.Find(x => x.TenantId == Id);

            if (curOwnerTenants == null || curOwnerTenants.PartAsset == null)
            {
                throw new InvalidOperationException("Tenant not found or PartAsset is null.");
            }

            return curOwnerTenants.PartAsset.Value;
        }
        public async Task AddTenants(List<TenantDTO> tenants)
        {
            if (tenants == null || tenants.Count == 0)
            {
                throw new ArgumentNullException(nameof(tenants), "Tenants cannot be null or empty");
            }

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (tenants[0].ApartmentId > 0 && tenants[0].ApartmentId != null && tenants != null && tenants.Count > 0 && tenants[0] != null)
                    {
                        try
                        {
                            _logger.LogInformation("Fetching apartment");
                            Apartment apartment = await _tenantDB.GetTenantsApartment(tenants[0].ApartmentId) ?? throw new InvalidOperationException("Apartment not found");

                            _logger.LogInformation("Fetching owner");
                            Owner owner1 = await _IOwnerDB.getOwnerByApartmen(tenants[0].ApartmentId);

                            if (owner1.ApartmentId == null || owner1.OwnerId == null)
                            {
                                _logger.LogInformation("Creating new owner");
                                OwnerDTO owner = new OwnerDTO
                                {
                                    ApartmentId = tenants[0].ApartmentId,
                                    OwnerStatus = 1,
                                    InsertDate = DateOnly.FromDateTime(DateTime.Now),
                                    HavePowerOfAttorney = tenants[0].IsSignatureByPowerOfAttorney,
                                    IsHavePowerOfAttorneyNotriony = tenants[0].PowerOfAttorneyType == 1,
                                    OwnerApartmentStatus = apartment.ApartmentStatus
                                };

                                var newOwner = _mapper.Map<Owner>(owner);
                                _ownerId = await _IOwnerDB.AddOwner(newOwner);
                                _logger.LogInformation($"New owner added with ID: {_ownerId}");
                            }
                            else
                            {
                                _ownerId = owner1.OwnerId;
                                _logger.LogInformation($"Existing owner found with ID: {_ownerId}");
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.LogError($"Error adding owner: {e.Message}");
                            throw new InvalidOperationException("Failed to add owner.", e);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("ApartmentId is invalid.");
                    }

                    var savedTenants = new List<Tenant>();
                    foreach (var t in tenants)
                    {
                        if (t.ApartmentId <= 0 || t.ApartmentId == null)
                        {
                            throw new InvalidOperationException("ApartmentId is invalid.");
                        }

                        try
                        {
                            _logger.LogInformation("Fetching apartment");
                            Apartment apartment = await _tenantDB.GetTenantsApartment(t.ApartmentId) ?? throw new InvalidOperationException("Apartment not found");

                            _logger.LogInformation("Creating new tenant");
                            Tenant tenant = new Tenant
                            {
                                TenantIdentity = t.TenantIdentity,
                                IdentityType = t.IdentityType,
                                LastName = t.LastName,
                                FirstName = t.FirstName,
                                IdFileName = t.IdFileName,
                                IsSignatureByPowerOfAttorney = t.IsSignatureByPowerOfAttorney,
                                PowerOfAttorneyId = t.PowerOfAttorneyId,
                                PreviousTenantId = t.PreviousTenantId,
                                OtherPrevious = t.OtherPrevious,
                                TenantIdentityPrevious = t.TenantIdentityPrevious,
                                InsertDate = DateOnly.FromDateTime(DateTime.Now),
                                TenantStatus = 1,
                                Usname = t.IdentityType == 2 ? t.Usname : null,
                                IdentityFromCountry = t.IdentityType == 2 ? t.IdentityFromCountry : null
                            };

                            if (t.IsSignatureByPowerOfAttorney)
                            {
                                _logger.LogInformation("Creating new power of attorney");
                                PowerOfAttorney power = new PowerOfAttorney
                                {
                                    PowerOfAttorneyId = t.PowerOfAttorneyId,
                                    FirstName = t.FirstNamePower,
                                    LastName = t.LastNamePower,
                                    IdFileName = t.IdFileNamePower,
                                    PowerOfAttorneyType = t.PowerOfAttorneyType,
                                    FromDate = t.FromDate,
                                    FileName = t.FileName,
                                    InsertDate = DateOnly.FromDateTime(DateTime.Now),
                                    Address = t.Address,
                                    NumberPhone = t.NumberPhone,
                                    NumberPhone2 = t.NumberPhone2
                                };

                                await _IPowerOfAttorneysDB.AddPower(power);
                            }

                            await _tenantDB.AddTenants(tenant);
                            savedTenants.Add(tenant);
                            _logger.LogInformation($"New tenant added with ID: {tenant.TenantId}");
                        }
                        catch (Exception e)
                        {
                            _logger.LogError($"Error adding tenant: {e.Message}");
                            throw new InvalidOperationException("Tenant not added.", e);
                        }
                    }

                    _logger.LogInformation("Saving tenants");
                    await _dbContext.SaveChangesAsync();

                    foreach (var tenant in savedTenants)
                    {
                        _logger.LogInformation("Creating new owner tenant");
                        OwnerTenant ownerTenant = new OwnerTenant
                        {
                            OwnerId = _ownerId,
                            TenantId = tenant.TenantId,
                            PartAsset = tenants.First(t => t.TenantIdentity == tenant.TenantIdentity).PartAsset,
                            InsertDate = DateOnly.FromDateTime(DateTime.Now)
                        };

                        await _IOwnerTenantDB.AddOwnerTenant(ownerTenant);
                    }

                    _logger.LogInformation("Saving changes");
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error adding tenant or owner: {e.Message}");
                    if (e.InnerException != null)
                    {
                        _logger.LogError($"Inner exception: {e.InnerException.Message}");
                    }
                    await transaction.RollbackAsync();
                    throw new InvalidOperationException("Operation failed.", e);
                }
            }
        }

        public async Task UpdateTenant(List<TenantDTO2> tenants)
        {
            if (tenants == null || tenants.Count == 0)
            {
                throw new ArgumentNullException(nameof(tenants), "Tenants cannot be null or empty");
            }

            // Start a transaction
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                foreach (var tenant in tenants)
                {
                    var newOwner = _mapper.Map<OwnerTenant>(tenant);
                    var newTenant = _mapper.Map<Tenant>(tenant);

                    if (tenant.IsSignatureByPowerOfAttorney == true)
                    {
                        var newPower = _mapper.Map<PowerOfAttorney>(tenant);
                        newPower.Id = tenant.powerId;
                        await _IPowerOfAttorneysDB.UpdatePower(newPower);
                    }

                    await _tenantDB.UpdateTenant(newTenant);
                    await _IOwnerTenantDB.UpdateOwnerTenant(newOwner);
                }
                await _dbContext.SaveChangesAsync();

                // Commit the transaction if everything succeeds
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                // Rollback the transaction if any error occurs
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Failed to update tenants. Transaction rolled back.");
                throw;
            }
        }
    }
}