using AutoMapper;
using Common.DTO;
using IBEXDATA.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class OwnerTenantDB: IOwnerTenantDB
    {
        private readonly dbContext _dbContext;
        private readonly IMapper mapper;

    
        public OwnerTenantDB(dbContext dbContext, IMapper _Mapper)
        {
            _dbContext = dbContext;
            mapper = _Mapper;
           
        }

       

        public async Task<List<OwnerTenant>> GetOwnersTeants()
        {
            return await _dbContext.OwnerTenants.ToListAsync();
        }

    }
}
