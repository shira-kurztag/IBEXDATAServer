using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IOwnerTenantService
    {
        Task<List<Tenant>> GetAllownerTenantByOwners(List<Owner> owners);

    }
}
