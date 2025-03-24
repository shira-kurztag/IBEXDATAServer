using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Common;
using IBEXDATA.Models;

namespace Service
{
    public class OwnerService : IOwnerService
    {
        IOwnerDB _OwnerDB;
        private readonly IOwnerTenantService _ownerTenantService;
        public OwnerService(IOwnerDB OwnerDB, IOwnerTenantService ownerTenantService)
        {
            _OwnerDB = OwnerDB;
            _ownerTenantService = ownerTenantService;
        }   


        public async Task<IEnumerable<Owner>> Get()
        {
            return await _OwnerDB.Get();
        }

       
            public async Task<List<Tenant>> GetOwnerByApartment(int apartmentId)
        {
            try
            {
                var allOwners = await _OwnerDB.Get();
               var owners=allOwners.Where(owner => owner.ApartmentId == apartmentId).ToList();
              return await _ownerTenantService.GetAllownerTenantByOwners(owners);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving owners by apartment ID.", ex);
            }
        }
    }
}
