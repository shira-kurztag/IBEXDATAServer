using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Common;
using IBEXDATA.Models;
using AutoMapper;
using Common.DTO;

namespace Service
{
    public class OwnerService : IOwnerService
    {
        IOwnerDB _OwnerDB;
        private readonly IOwnerTenantService _ownerTenantService;
        private readonly IOwnerTenantDB _ownerTenantDB;
       private readonly IMapper _mapper;
        public OwnerService(IOwnerDB OwnerDB, IOwnerTenantService ownerTenantService,IOwnerTenantDB ownerTenantDB, IMapper mapper)
        {
            _OwnerDB = OwnerDB;
            _ownerTenantService = ownerTenantService;
            _ownerTenantDB = ownerTenantDB;
            _mapper = mapper;
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
        //public async Task<List<SimpleOwnerDTO>> GetAllOwnersByTenants(List<int> tenants)
        //{
        //    var allOwnersTenants = await _ownerTenantDB.GetOwnersTeants();
        //    var ownerIds = allOwnersTenants
        //   .Where(o => tenants.Any(ot => ot == o.TenantId))
        //   .Select(o => o.OwnerId)
        //   .Distinct()
        //   .ToList();
        //    var owners = await GetOwnersByIds(ownerIds);
        //    var simpleOwner = _mapper.Map<SimpleOwnerDTO>(Owner);

        //    return simpleOwner;
        //}


        public async Task<List<SimpleOwnerDTO>> GetAllOwnersByTenants(List<int> tenants)
        {
            var allOwnersTenants = await _ownerTenantDB.GetOwnersTeants();

            var ownerIds = allOwnersTenants
                .Where(o => tenants.Contains(o.TenantId))
                .Select(o => o.OwnerId)
                .Distinct()
                .ToList();

            var owners = await GetOwnersByIds(ownerIds);

            // Assuming owners is a List<Owner>
            var simpleOwners = _mapper.Map<List<SimpleOwnerDTO>>(owners);

            return simpleOwners;
        }
    }
}
