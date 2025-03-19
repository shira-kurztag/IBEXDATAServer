using Common.DTO;
using IBEXDATA.Models;

namespace DB
{
    public interface IContractorDB
    {
        Task CreateContractor(ContractorDTO2 contractor);
        Task<List<Contractor>> GetAllContractors();
        Task UpdateContractor(ContractorDTO2 contractor);
    }
}