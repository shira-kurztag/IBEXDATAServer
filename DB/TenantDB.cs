using AutoMapper;
using Common.DTO;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class TenantDB : ITenantDB
    {

        private readonly dbContext _dbContext;
        private readonly IMapper mapper;

        private readonly ILogger<TenantDB> _logger;
        public TenantDB(dbContext dbContext, IMapper _Mapper, ILogger<TenantDB> logger)
        {
            _dbContext = dbContext;
            mapper = _Mapper;
            _logger = logger;

        }

        public async Task<List<Tenant>> GetAllTenants()
        {
            return await _dbContext.Tenants.ToListAsync();
        }

        public async Task<List<OwnerTenant>> GetPartAssetByOwnerTenants()
        {
            return await _dbContext.OwnerTenants.ToListAsync();
        }

    }
}

