using AutoMapper;
using Common.DTO;
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

            CreateMap<Contractor, ContractorDTO>();
            CreateMap<Building, BuildingDTO>();
            CreateMap<Mortagege, MortagegeDTO>().ReverseMap();

        }
    }
}
