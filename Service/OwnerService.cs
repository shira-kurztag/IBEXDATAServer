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
        public OwnerService(IOwnerDB OwnerDB)
        {
            _OwnerDB = OwnerDB;
        }

        public async Task<IEnumerable<Owner>> Get()
        {
            return await _OwnerDB.Get();
        }
        public async Task<IEnumerable<Owner>> GetOwnerByApartment(int apartmentId)
        {
            try
            {
                var allOwners = await _OwnerDB.Get();
                return allOwners.Where(owner => owner.ApartmentId == apartmentId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving owners by apartment ID.", ex);
            }
        }
    }
}
