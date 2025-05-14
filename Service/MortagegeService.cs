using AutoMapper;
using Common.DTO;
using DB;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MortagegeService : IMortagegeService
    {
        private readonly IMortagegeDB _mortagegeDB;

        private readonly IMapper _mapper;


        public MortagegeService(IMortagegeDB MortagegeDB, IMapper mapper)
        {
            _mortagegeDB = MortagegeDB;
            _mapper = mapper;

        }
        public async Task<List<MortagegesType>> GetAllMortagegesTypes()
        {
            return await _mortagegeDB.GetAllMortagegesTypes();
        }
        public async Task<List<CurrencyType>> GetAllCurrencyTypes()
        {
            return await _mortagegeDB.GetAllCurrencyTypes();

        }
        public async Task<List<MortagegeLevel>> GetAllMortagegeLevels()
        {
            return await _mortagegeDB.GetAllMortagegeLevels();

        }

        public async Task SaveFullMortagege(MortagegeDTO mortagegeDTO, int mortagegeId)
        {
            var mortagege = _mapper.Map<Mortagege>(mortagegeDTO);
            var mortgageToTeanants = new List<MortgageToTeanant>();
            if (mortagege.AmountType == -1)
                mortagege.AmountType = null;
            // חוזר על כל מזהה דייר ברשימת TeanantId
            foreach (var tenantId in mortagegeDTO.TeanantId)
            {
                var mortgageToTeanant = new MortgageToTeanant
                {
                    MortgageId = mortagegeId, // מזהה המשכנתה
                    TeanantId = tenantId      // מזהה הדייר
                };

                mortgageToTeanants.Add(mortgageToTeanant);
            }
            mortagege.MortagegeId = mortagegeId;
            await _mortagegeDB.SaveFullMortgage(mortagege, mortgageToTeanants);
        }

        public async Task<int> CreateMortagege(MortagegeDTO mortagegeDTO)
        {
            var mortagege = _mapper.Map<Mortagege>(mortagegeDTO);
         
            
          return  await _mortagegeDB.CreateMortagege(mortagege);
        }
        public async Task<List<TypeMessage>> GetAllTypeMessages()
        {

            return await _mortagegeDB.GetAllTypeMessages();
        }

   
        public async Task<bool> HasMortgageInProcess(int apartmentId)
        {
            return await _mortagegeDB.HasMortgageInProcess(apartmentId);
        }

        public async Task<long> createBankCertificate(BankCertificate bankCertificate)
        {
            return await _mortagegeDB.createBankCertificate(bankCertificate);
        }

        public async Task UpdateBankCertificates(int mortgageId, int[] listIdOwnerOfmort,  List<BankCertificate> bankCertificates)
        {


             await _mortagegeDB.UpdateBankCertificate(bankCertificate);
        }

        public async Task<List<int>> GetAllMortgageBanksByApartment(int apartmentId)
        {
            return await _mortagegeDB.GetAllMortgageBanksByApartment(apartmentId);
        }
    }
}
