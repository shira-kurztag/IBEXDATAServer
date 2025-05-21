using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public interface ITabusDB
    {
        Task<Tabus> GetTabusByOwnerId(int OwnerId);
    }
}
