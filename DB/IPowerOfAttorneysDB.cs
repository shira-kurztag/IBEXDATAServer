using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public interface IPowerOfAttorneysDB
    {
        Task <int> AddPower(PowerOfAttorney Power);

        Task UpdatePower(PowerOfAttorney Power);
        Task<PowerOfAttorney> GetPowerById(int PowerId);
        Task Delete(int PowerId);


    }
}
