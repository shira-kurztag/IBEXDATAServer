using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class TenantDTO
    {
        public int? TenantStatus { get; set; }

        public string? TenantIdentity { get; set; }

        public int? IdentityType { get; set; }

        public string? IdentityFromCountry { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public string? IdFileName { get; set; }

        public bool IsSignatureByPowerOfAttorney { get; set; }

        public string? PowerOfAttorneyId { get; set; }

        public string? AddressByContract { get; set; }

        public bool SignedAsTrustee { get; set; }

        public int? PreviousTenantId { get; set; }

        public string? Usname { get; set; }

        public bool? ThereRrePreviousIdentifyingDetails { get; set; }

        public int? IdentityTypePrevious { get; set; }

        public string? IdentityFromCountryPrevious { get; set; }

        public string? TenantIdentityPrevious { get; set; }

        public DateOnly? PassportExpiredPrevious { get; set; }

        public string? OtherPrevious { get; set; }

        
    }
}
