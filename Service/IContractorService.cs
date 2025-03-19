using Common.DTO;
using IBEXDATA.Models;

namespace Service
{
    public interface IContractorService
    {
        Task CreateContractor(ContractorDTO2 contractor);
        Task<Contractor> GetAllContractorById(int Id);
        Task<List<Contractor>> GetAllContractors();
        Task UpdateContractor(ContractorDTO2 contractor);
    }
}