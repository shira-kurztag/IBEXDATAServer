using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class MortagegeDTO
    {


        public int? ToTheBank { get; set; }

        public double? Amount { get; set; }


        public int? LevelMortagege { get; set; }


        public int? MortagegeStatus { get; set; }

        public int? AmountType { get; set; }

        public string? Note { get; set; }

        public string? NoteOrConditioning { get; set; }//האם יש הערות/התניות למשכנתא?

        public bool IsApprovalCompany { get; set; }

        /// <summary>
        /// MortgageToTeanantנוסיף ל
        /// </summary>

        public List<int>? TeanantId { get; set; } 


        public int? MortagegesType { get; set; }

        public bool IsAllTenantlpprovat { get; set; }
       

    }
}
