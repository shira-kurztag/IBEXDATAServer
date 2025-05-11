using AutoMapper;
using Common.DTO;
using DB;
using IBEXDATA.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Application
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            // Mapping from ProjectCreateDTO to Project
            CreateMap<ProjectCreateDTO, Project>();
            CreateMap<Project, ProjectCreateDTO>();

            // Mapping from Project to ProjectDTO
            CreateMap<Project, ProjectDTO>();

            // Mapping from Bank to BankDTO
            CreateMap<Bank, BankDTO>();
            CreateMap<BankDTO, Bank>();
            CreateMap<Bank, BankNamesDTO>();

            CreateMap<ContractorDTO2, Contractor>().ReverseMap();
            CreateMap<Building, BuildingDTO>();
            CreateMap<Mortagege, MortagegeDTO>().ReverseMap();

            CreateMap<TenantDTO, Tenant>().ReverseMap();
            CreateMap<TenantDTO2, Tenant>().ReverseMap();
            CreateMap<Owner, OwnerDTO2>().ReverseMap();
            CreateMap<Apartment, OwnerDTO2>().ReverseMap();

            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<OwnerTenant, TenantDTO2>().ReverseMap();
            CreateMap<PowerOfAttorney, TenantDTO2>().ReverseMap();

        }
    }
}
