using IBEXDATA.Models;

namespace DB
{
    public interface IMortagegeDB
    {
        Task<List<CurrencyType>> GetAllCurrencyTypes();
        Task<List<MortagegeLevel>> GetAllMortagegeLevels();
        Task<List<MortagegesType>> GetAllMortagegesTypes();
    }
}