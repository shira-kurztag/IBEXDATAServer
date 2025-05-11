using AutoMapper;
using Common.DTO;
using DB;
using IBEXDATA.Models;

namespace Service
{
    public class OwnerService : IOwnerService
    {
        public readonly IApartmentDB _ApartmentDB;
        private readonly IMapper _mapper;
        private readonly IOwnerDB _OwnerDB;
        private readonly IBuildingDB _BuildingDB;
        private readonly IContractorDB _ContractorDB;

        public OwnerService(IOwnerDB OwnerDB, IApartmentDB ApartmentDB, IMapper mapper , IBuildingDB BuildingDB, IContractorDB ContractorDB)
        {
            _OwnerDB = OwnerDB;
            _ApartmentDB = ApartmentDB;
            _mapper = mapper;
            _BuildingDB = BuildingDB;
            _ContractorDB = ContractorDB;
        }


        public async Task<IEnumerable<Owner>> Get()
        {
            return await _OwnerDB.Get();
        }
        public async Task<IEnumerable<Owner>> GetOwnerByApartment(int apartmentId)
        {
            try
            {
                var allOwners = await _OwnerDB.Get();
                return allOwners.Where(owner => owner.ApartmentId == apartmentId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving owners by apartment ID.", ex);
            }
        }

        public async Task<OwnerDTO2> GetOwnerByApartmentId(int apartmentId)
        {
            try
            {

                if (apartmentId == null || apartmentId == 0)
                {
                    throw new ArgumentException("Invalid apartment ID.");
                }

                var Owner = await _OwnerDB.getOwnerByApartmen(apartmentId);
                string AddressAndNumberOfMunicipal = await _BuildingDB.GetPurchaseDateByApartmentId(apartmentId);
                string purchasedFrom = await _ContractorDB.NameContractorByProject(apartmentId);
                if (/*Owner.ApartmentId == null || */  Owner.OwnerId == null)
                {
                    throw new ApplicationException("An error occurred while retrieving owners by apartment ID.");

                }
                var newOwner = _mapper.Map<OwnerDTO2>(Owner);
                newOwner.AddressAndNumberOfMunicipal = AddressAndNumberOfMunicipal;
                newOwner.purchasedFrom = purchasedFrom;
                // Map additional data from Apartment into the same object
                //_mapper.Map(Apartment, newOwner);
                return newOwner;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving owners by apartment ID.", ex);
            }
        }

        public async Task UpdateOwner(OwnerDTO2 owner)
        {

            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner), "Owner cannot be null");
            }
            if (owner.OwnerId == null)
            {
                throw new ArgumentException("Owner ID cannot be null");
            }
            await _OwnerDB.UpdateOwner(owner);
            await _ApartmentDB.UpdateApartmenByOwner(owner);

          

        }
    }
}


