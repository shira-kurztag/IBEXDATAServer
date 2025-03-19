using IBEXDATA.Models;

namespace Service
{
    public interface IMortagegeService
    {
        Task<List<CurrencyType>> GetAllCurrencyTypes();
        Task<List<MortagegeLevel>> GetAllMortagegeLevels();
        Task<List<MortagegesType>> GetAllMortagegesTypes();
    }
}