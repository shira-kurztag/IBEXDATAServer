using AutoMapper;
using Common.DTO;
using DB;
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdminApprovalService:IAdminApprovalService
    {
        private readonly IAdminApprovalDB _adminApprovalDB;

        private readonly IMapper _mapper;


        public AdminApprovalService(IAdminApprovalDB  adminApprovalDB, IMapper mapper)
        {
            _adminApprovalDB = adminApprovalDB;
            _mapper = mapper;

        }

        public async Task CreateAdminApproval(AdminApprovalDTO adminApprovalDTO)
        {
            try
            {
                foreach (var recipientId in adminApprovalDTO.ReciverId)
                {
                    // יצירת אובייקט AdminApproval ממופה
                    var adminApproval = _mapper.Map<AdminApproval>(adminApprovalDTO);

                    // הגדרת הנמען הנוכחי באופן ידני
                    adminApproval.ReciverId = recipientId;

                    // שמירה במסד הנתונים
                    await _adminApprovalDB.CreateAdminApproval(adminApproval);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("An error occurred while creating the adminApproval.", ex);
            }
        }


    }
}
