using Common.DTO;
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public interface ITabusDB
    {
        Task<Tabu> GetTabusByOwnerId(int ApartmentId);

        Task UpdateTabusByOwnerId ( TabuDTO tabu);
    }
}
