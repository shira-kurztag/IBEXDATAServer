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
    public class TenantService : ITenantService
    {



        private readonly ITenantDB _tenantDB;

        private readonly IMapper _mapper;


        public TenantService(ITenantDB TenantRepository, IMapper mapper)
        {
            _tenantDB = TenantRepository;
            _mapper = mapper;

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
            foreach (var t in tenants)
            {
                if (t.ApartmentId > 0 || t.ApartmentId != null)
                {
                    Apartment apartment = await _tenantDB.GetTenantsApartment(t.ApartmentId);

                }
                else
                {
                    // טיפול במקרה שהערך אינו חוקי (null או קטן מ-1)
                    throw new InvalidOperationException("ApartmentId is invalid.");
                }
                try
                {

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
                        InsertDate = DateOnly.FromDateTime(DateTime.Now)
                    };
                    if (t.IdentityType == 2)
                    {
                        tenant.Usname = t.Usname;
                        tenant.IdentityFromCountry = t.IdentityFromCountry;

                    }

                    if (t.IsSignatureByPowerOfAttorney)
                    {
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

                        await _tenantDB.AddPower(power);
                    }
                    await _tenantDB.AddTenants(tenant);

                    if (t.ApartmentId > 0 || t.ApartmentId != null)
                    {
                        Apartment apartment = await _tenantDB.GetTenantsApartment(t.ApartmentId);


                        OwnerDTO owner = new OwnerDTO
                        {
                            ApartmentId = t.ApartmentId,
                            OwnerStatus = 1,
                            InsertDate = DateOnly.FromDateTime(DateTime.Now),
                            HavePowerOfAttorney = t.IsSignatureByPowerOfAttorney,
                            //נתונים שצריכה לשאול את תהילה לאן מתקשרים 
                            IsReported = true,
                            IsConfirmationReporting = true,
                            IsFurthermoreLackOfApproval = true,
                            IsCorrectPowerOfAttorney = true,
                            IsGivenVouchers = true,
                            IsLegalExpensesPaid = true,
                            IsFormSignedIrrevocableInstructions = true,
                            IsPaidParticipationInTheCondominium = true,
                            IsPaymentMortgageBonds = true,
                            IsLackPurchaseTaxBalance = true,
                            IsCorrectLackPurchaseTaxBalance = true,
                            IsLackBalanceForCapitalGainsTax = true,
                            IsCorrectLackBalanceForCapitalGainsTax = true,
                            IsNeedPermissionFromSalesTax = true,
                            IsHavePermissionFromSalesTax = true,

                            IsCorrectPermissionFromSalesTax = true,
                            IsThereTabooMunicipalApproval = true,
                            IsSourceFurthermoreLackOfTaxCertificates = true,
                            IsCorrectFurthermoreLackOfTaxCertificates = true,
                            IsIdentityFile = true,
                            IsSignedByTheParties = true,
                            IsSignedByTheIMI = true,
                            IsMortgageBillsReceived = true,
                            IsCorrectMortgageBillsReceived = true,
                            IsSuitableMortgageBillsReceived = true,
                            IsSignMortgageBillsReceived = true,

                            IsConfirmToExport = true,



                        };

                        if (t.PowerOfAttorneyType == 1)
                        {
                            owner.IsHavePowerOfAttorneyNotriony = true;

                        }
                        else
                        {
                            owner.IsHavePowerOfAttorneyNotriony = false;

                        }
                        if (apartment.ApartmentId == t.ApartmentId)
                        {
                            owner.OwnerApartmentStatus = apartment.ApartmentStatus;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error adding tenant: {e.Message}");
                    throw new InvalidOperationException("Tenant not added.", e);
                }
            }
        }

        public async Task<List<Tenant>> GetTenantsByIds(List<int> tenantIds)
       
        {
            var teants = await _tenantDB.GetAllTenants();
            return teants
                .Where(t => tenantIds.Contains(t.TenantId))
                .ToList();

        }

    }

}

