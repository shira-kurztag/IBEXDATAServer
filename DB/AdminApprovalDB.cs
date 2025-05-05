using AutoMapper;
using IBEXDATA.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public  class AdminApprovalDB: IAdminApprovalDB
    {

        private readonly dbContext _dbContext;
        private readonly IMapper mapper;

        private readonly ILogger<AdminApproval> _logger;
        public AdminApprovalDB(dbContext dbContext, IMapper _Mapper, ILogger<AdminApproval> logger)
        {
            _dbContext = dbContext;
            mapper = _Mapper;
            _logger = logger;

        }
        public async Task CreateAdminApproval(AdminApproval adminApproval)
        {
            await _dbContext.AdminApprovals.AddAsync(adminApproval);
            await _dbContext.SaveChangesAsync();
           
        }
    }
}
