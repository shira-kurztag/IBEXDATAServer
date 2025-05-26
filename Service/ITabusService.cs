using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITabusService
    {
        Task<TabuDTO> GetTabusByApartmentId(int ApartmentId);

        Task UpdateTabusByOwnerId( TabuDTO tabu);
    }
}
