using Common.DTO;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class BankDB : IBankDB
    {
        private readonly dbContext _context;
        private static readonly Serilog.ILogger _logger = Log.ForContext<ProjectDB>(); // Create a logger instance

        public BankDB(dbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bank>> Get()
        {
            try
            {
                var banks = await _context.Banks.ToListAsync();

                if (banks != null)
                {
                    _logger.Information("Successfully get all the banks.");
                    return banks;
                }
                else
                {
                    _logger.Warning("Bank list not loaded successfully.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Get method of Get in BankDB.");
                return null;
            }
        }

        public async Task<IEnumerable<Bank>> GetNames()
        {
            try
            {
                var banks = await _context.Banks.ToListAsync();

                if (banks != null)
                {
                    _logger.Information("Successfully get all the banks.");
                    return banks;
                }
                else
                {
                    _logger.Warning("Bank list not loaded successfully.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Get method of Get in BankDB.");
                return null;
            }
        }


        public IEnumerable<Bank> GetBanks()
        {
            return _context.Banks.Include(e => e.BillsMortgageForTabus).ToList();
        }
        public async Task<IEnumerable<Bank>> filterBank(string LastNameBank)
        {

            var p = _context.Banks.Include(e => e.BankId);

            var q = _context.Banks.Where(Banks =>
            (LastNameBank == null ? (true) : (Banks.LastNameBank.Contains(LastNameBank)))

    );

            List<Bank> banks = await q.ToListAsync();

            return banks;
        }
        public IEnumerable<Bank> AddBank(Bank newBank)
        {
            var bank = new Bank
            {
                BankId = newBank.BankId,
                BankText = newBank.BankText,
                BankStatus = newBank.BankStatus,
                LastNameBank = newBank.LastNameBank
            };
            _context.Banks.Add(bank);
            _context.SaveChanges();
            return GetBanks();

        }

        public IEnumerable<Bank> DeleteBankById(int Id)
        {
            Bank? bank = _context.Banks.Find(Id);
            _context.Banks.Remove(bank);
            _context.SaveChanges();
            return GetBanks();
        }
        public IEnumerable<Bank> UpdateBank(int bankId, BankDTO bank)
        {
            var existingBank = _context.Banks.Find(bankId);

            if (existingBank == null)
            {
                throw new ArgumentException("Bank not found");
            }
            existingBank.BankText = bank.BankText;

            _context.SaveChanges();

            return _context.Banks.ToList();
        }


        public async Task<Bank> Update(int id, Bank bank)
        {
            try
            {
                //var existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
                //if (existingProject == null)
                //{
                //    _logger.Warning("Project with ID {ProjectId} not found.", id);
                //    return null;
                //}

                bank.BankId = id;
                _context.Banks.Update(bank);
                await _context.SaveChangesAsync();

                _logger.Information("Successfully updated Bank with ID {BankId}.", id);
                return bank;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Update method of BankDB.");
                return null;
            }
            // _context.Entry(existingProject).CurrentValues.SetValues(project);

        }

    }
}
