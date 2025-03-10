using IBEXDATA.Models;

namespace Service
{
    public interface IBankService
    {
        Task<IEnumerable<Bank>> Get();
        Task<IEnumerable<Bank>> GetNames();
    }
}