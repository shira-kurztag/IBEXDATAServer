using Common.DTO;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class TabusDB : ITabusDB
    {
        private readonly ILogger<ApartmentDB> _logger;

        private readonly dbContext _context;
        public TabusDB(dbContext context, ILogger<ApartmentDB> logger)
        {

            _logger = logger;
            _context = context;
        }

        public async Task<Tabu> GetTabusByOwnerId(int ownerId)
        {
            var tabu = await _context.Tabus
                .Where(t => t.OwnerId == ownerId)
                .FirstOrDefaultAsync();
            if(tabu == null)
            {
                throw new InvalidOperationException($"No Tabus found for the given OwnerId: {ownerId}.");
            }
            return tabu;
        }

        public async Task UpdateTabu(TabuDTO tabu)
        {
            var tabu1 = await _context.Tabus
                            .Where(t => t.OwnerId == tabu.OwnerId)
                            .FirstOrDefaultAsync();
            if(tabu1 == null)
            {
                throw new InvalidOperationException($"No Tabus found for the given OwnerId: {tabu.OwnerId}.");
            }


        }

    }
}
