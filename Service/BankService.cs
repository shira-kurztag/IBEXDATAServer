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
    public class BankService : IBankService
    {
        IBankDB _BankDB;
        public BankService(IBankDB BankDB)
        {
            _BankDB = BankDB;
        }

        public async Task<IEnumerable<Bank>> Get()
        {
            return await _BankDB.Get();
        }

        public async Task<IEnumerable<Bank>> GetNames()
        {
            return await _BankDB.GetNames();
        }

        public IEnumerable<Bank> GetBanks()
        {
            //_logger.Log("hiiiiiiiiiii");
            return _BankDB.GetBanks();
        }
        public IEnumerable<Bank> DeleteBankById(int Id)
        {
            return _BankDB.DeleteBankById(Id);
        }

        public async Task<Bank> Update(int id, Bank bank)
        {
            return await _BankDB.Update(id, bank);
        }

        public IEnumerable<Bank> AddBank(Bank newBank)
        {
            return _BankDB.AddBank(newBank);
        }
    }
}