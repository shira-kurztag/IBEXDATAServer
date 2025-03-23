using System;

namespace Common.DTO
{
    public class TenantDTO
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string TenantIdentity { get; set; }
        public int? IdentityType { get; set; }
        public string TenantStatus { get; set; }
        public string IdFileName { get; set; }
        public string IdentityFromCountry { get; set; }
        public string Usname { get; set; }
        public int? PreviousTenantId { get; set; }
        public int? IdentityTypePrevious { get; set; }
        public string TenantIdentityPrevious { get; set; }
        public string OtherPrevious { get; set; }
        public bool IsSignatureByPowerOfAttorney { get; set; }
        public string PowerOfAttorneyId { get; set; }
        public string LastNamePower { get; set; }
        public string FirstNamePower { get; set; }
        public string IdFileNamePower { get; set; }
        public int? PowerOfAttorneyType { get; set; }
        public DateOnly? FromDate { get; set; }
        public string FileName { get; set; }
        public string Address { get; set; }
        public string NumberPhone { get; set; }
        public string NumberPhone2 { get; set; }
        public double? PartAsset { get; set; } // חלק בנכס
        public int  ApartmentId { get; set; }//דירה מויממת לדייר



    }
}