using Common.DTO;
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> Get();

        Task<List<Tenant>> GetOwnerByApartment(int apartmentId);


        Task<List<Owner>> GetOwnersByIds(List<int> ownersIds);
        Task<List<SimpleOwnerDTO>> GetAllOwnersByTenants(List<int> tenants);
    

    }
}
