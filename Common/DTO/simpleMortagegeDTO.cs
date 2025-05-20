using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class simpleMortagegeDTO
    {
        public int MortagegeId { get; set; }
        public string? ToTheBank { get; set; }

        public double? Amount { get; set; }


        public string? LevelMortagege { get; set; }


        public int? MortagegeStatus { get; set; }

        public string? AmountType { get; set; }

        public string? Note { get; set; }

        public string? NoteOrConditioning { get; set; }//האם יש הערות/התניות למשכנתא?

        public bool IsApprovalCompany { get; set; }

        /// <summary>
        /// MortgageToTeanantנוסיף ל
        /// </summary>

        public List<string>? TeanantId { get; set; }


        public string? MortagegesType { get; set; }

        public bool IsAllTenantlpprovat { get; set; }
    }
}
