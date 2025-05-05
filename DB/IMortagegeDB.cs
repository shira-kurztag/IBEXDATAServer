using IBEXDATA.Models;

namespace DB
{
    public interface IMortagegeDB
     {
        Task<List<CurrencyType>> GetAllCurrencyTypes();
        Task<List<MortagegeLevel>> GetAllMortagegeLevels();
        Task<List<MortagegesType>> GetAllMortagegesTypes();
        Task<int> CreateMortagege(Mortagege mortagege);
        Task SaveFullMortgage(Mortagege mortgage,List< MortgageToTeanant> mortgageToTenant);
        Task<List<TypeMessage>> GetAllTypeMessages();
    }
}