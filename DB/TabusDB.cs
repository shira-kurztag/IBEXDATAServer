using AutoMapper;
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
        private readonly IMapper _mapper;

        private readonly dbContext _context;
        public TabusDB(dbContext context, ILogger<ApartmentDB> logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<Tabu> GetTabusByApartmentId(int ApartmentId)
        {
            var tabu = await _context.Tabus
                .Where(t => t.ApartmentId == ApartmentId)
                .FirstOrDefaultAsync();
            if(tabu == null)
            {
                throw new InvalidOperationException($"No Tabus found for the given OwnerId: {ApartmentId}.");
            }
            return tabu;
        }

        public async Task UpdateTabusByOwnerId(TabuDTO tabu)
        {
            var existingTabu = await _context.Tabus
                .Where(t => t.TabuId == tabu.TabuId)
                .FirstOrDefaultAsync();
            if (existingTabu == null)
            {
                throw new InvalidOperationException($"No Tabus found for the given OwnerId: {tabu.TabuId}.");
            }
             _mapper.Map(tabu, existingTabu);

            var affected = await _context.SaveChangesAsync();
            if (affected == 0)
            {
                throw new Exception("No changes were saved to the database.");
            }

        }
    }
}
