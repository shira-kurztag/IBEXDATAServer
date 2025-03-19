using Common.DTO;
using DB;
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ContractorService : IContractorService
    {
        private readonly IContractorDB _contractorRepository;



        public ContractorService(IContractorDB contractorRepository)
        {
            _contractorRepository = contractorRepository;

        }
        public async Task<List<Contractor>> GetAllContractors()
        {
            return await _contractorRepository.GetAllContractors();
        }
        public async Task CreateContractor(ContractorDTO2 contractor)
        {
            await _contractorRepository.CreateContractor(contractor);

        }
        public async Task UpdateContractor(ContractorDTO2 contractor)
        {
            await _contractorRepository.UpdateContractor(contractor);
        }
        public async Task<Contractor> GetAllContractorById(int Id)
        {
            var contractors = await _contractorRepository.GetAllContractors();
            return contractors.Find(x => x.ContractorId == Id);
        }

    }
}
