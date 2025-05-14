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
        Task<bool> HasMortgageInProcess(int apartmentId);
         Task<long> createBankCertificate(BankCertificate bankCertificate);
        Task UpdateBankCertificate(BankCertificate bankCertificate);

        Task<List<int>> GetAllMortgageBanksByApartment(int apartmentId);
    }
}