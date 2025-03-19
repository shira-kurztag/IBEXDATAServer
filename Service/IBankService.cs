using IBEXDATA.Models;

namespace Service
{
    public interface IBankService
    {
        IEnumerable<Bank> AddBank(Bank newBank);
        IEnumerable<Bank> DeleteBankById(int Id);
        Task<IEnumerable<Bank>> Get();
        IEnumerable<Bank> GetBanks();
        Task<IEnumerable<Bank>> GetNames();
        Task<Bank> Update(int id, Bank bank);
    }
}