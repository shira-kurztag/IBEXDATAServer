using Common.DTO;
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public interface IOwnerDB
    {
        Task<IEnumerable<Owner>> Get();
        Task<int> AddOwner(Owner owner);
        Task<Owner> getOwnerByApartmen(int apartmenID);
        Task Delete(int ownerId);
        Task UpdateOwner(OwnerDTO2 owner);
        Task<Owner> GetOwnerById(int ownerId);
    }
}
