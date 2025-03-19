using Common.DTO;
using IBEXDATA.Models;

namespace DB
{
    public interface IBankDB
    {
        IEnumerable<Bank> AddBank(Bank newBank);
        IEnumerable<Bank> DeleteBankById(int Id);
        Task<IEnumerable<Bank>> filterBank(string LastNameBank);
        Task<IEnumerable<Bank>> Get();
        IEnumerable<Bank> GetBanks();
        Task<IEnumerable<Bank>> GetNames();
        Task<Bank> Update(int id, Bank bank);
        IEnumerable<Bank> UpdateBank(int bankId, BankDTO bank);
    }
}