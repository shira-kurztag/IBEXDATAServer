using Common.DTO;
using IBEXDATA.Models;

namespace Service
{
    public interface IMortagegeService
    {
        Task<List<CurrencyType>> GetAllCurrencyTypes();
        Task<List<MortagegeLevel>> GetAllMortagegeLevels();
        Task<List<MortagegesType>> GetAllMortagegesTypes();
       
        Task SaveFullMortagege(MortagegeDTO mortagegeDTO, int mortagegeId);
        Task<int> CreateMortagege(MortagegeDTO mortagegeDTO);
        Task<List<TypeMessage>> GetAllTypeMessages();

    }
}