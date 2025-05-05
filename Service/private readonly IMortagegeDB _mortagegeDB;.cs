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
        private readonly IOwnerTenantDB _ownerTenantDB;
        public OwnerService(IOwnerDB OwnerDB, IOwnerTenantService ownerTenantService,IOwnerTenantDB ownerTenantDB)
        {
            _OwnerDB = OwnerDB;
            _ownerTenantService = ownerTenantService;
            _ownerTenantDB = ownerTenantDB;
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
                // הוספת לוגים כדי לוודא שהנתונים הם מה שציפית
                Console.WriteLine("Filtered owners: " + owners.Count);
                owners.ForEach(owner => Console.WriteLine(owner.ToString()));
                return await _ownerTenantService.GetAllownerTenantByOwners(owners);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving owners by apartment ID.", ex);
            }
        }


        public async Task<List<Owner>> GetOwnersByIds(List<int> ownersIds)

        {
            var teants = await _OwnerDB.Get();
            return teants
                .Where(t => ownersIds.Contains(t.OwnerId))
                .ToList();

        }
        public async Task<List<Owner>> GetAllOwnersByTenants(List<int> tenants)
        {
            var allOwnersTenants = await _ownerTenantDB.GetOwnersTeants();
            var ownerIds = allOwnersTenants
           .Where(o => tenants.Any(ot => ot == o.TenantId))
           .Select(o => o.OwnerId)
           .Distinct()
           .ToList();
            var owners = await GetOwnersByIds(ownerIds);
            return owners;
        }
    }
}
