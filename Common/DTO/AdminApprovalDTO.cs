using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public  class AdminApprovalDTO
    {

        //public int Id { get; set; }

        //public int? NameExecuteId { get; set; }

        //public int? Type { get; set; }

        public string? Message { get; set; }

        //public int? Status { get; set; }

        //public DateTime? DateUpdate { get; set; }

        //public int? ObjectId { get; set; }

        public int? MessageType { get; set; }//סוג הפעולה

        public int? OwnerId { get; set; }

        public List<int>? ReciverId { get; set; }//נמען 

        //public string? DocumentToConfirm { get; set; }

        //public string? Parameters { get; set; }

        public int? ApartmentId { get; set; }

        //public int? ConfirmLawyerId { get; set; }

        //public int? DocumentId { get; set; }

        //public string? DocumentIdList { get; set; }

        //public string? ShowAnswer { get; set; }
    }
}
