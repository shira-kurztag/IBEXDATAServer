using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Common.DTO
{
    public class TabuDTO
    {
        public int TabuId { get; set; }

        public int? OwnerId { get; set; }

        public int? ApartmentId { get; set; }

        public bool? IsMortgagePaid { get; set; }

        //public string? MortgagePaidDetail { get; set; }

        //public string? SubShare { get; set; }

        public double? CommonArea { get; set; }

        public string? NotarizedPoweReason { get; set; }

        public bool? IsPurchaseTax { get; set; }

        public bool? IsPurchaseTaxValid { get; set; }

        public bool? IsCapitalTax { get; set; }

        public bool? IsCapitalTaxValid { get; set; }

        public bool? IsApprovalNeededSaleTaxHas { get; set; }

        public bool? IsApprovalNeededSaleTaxValid { get; set; }

        public bool? IsMunicipalityApproval { get; set; }

        public DateOnly? IsMunicipalityApprovalValidity { get; set; }

        public bool? IsTaxApprovedTransferRights { get; set; }

        public bool? IsTaxApprovedTransferRightsSource { get; set; }

        public bool? IsTaxApprovedTransferRightsValid { get; set; }

      
        public bool? IsPartiesSigned { get; set; }

        public bool? IsSignedByMm { get; set; }

        public bool? IsMortgageBillReceived { get; set; }
        public string? FareName { get; set; }
        public double? FareAmount { get; set; }
        public int? Bloc { get; set; }
        public int? Smooth { get; set; }
        public int? SmothArea { get; set; }


    }
}
