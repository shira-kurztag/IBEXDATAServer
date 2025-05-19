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
        Task<bool> HasMortgageInProcess(int apartmentId);

        Task<long> createBankCertificate(BankCertificate bankCertificate);
        Task UpdateBankCertificates(int mortgageId, int[] listIdOwnerOfmort, List<BankCertificate> bankCertificates);
        Task<List<int>> GetAllMortgageBanksByApartment(int apartmentId);


       Task<List<int>> GetAllIdMortgageByTeant(int tenantId);

       Task<Mortagege> GetMortgageById(int mortgageId);


     
    }
}