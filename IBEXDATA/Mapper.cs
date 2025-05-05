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
            CreateMap<AdminApprovalDTO, AdminApproval>().ReverseMap();
            CreateMap<AdminApprovalDTO, AdminApproval>()
         .ForMember(dest => dest.ReciverId, opt => opt.Ignore());
            CreateMap<ProjectCreateDTO, Project>();
            CreateMap<Project, ProjectCreateDTO>();

         
            CreateMap<Project, ProjectDTO>();

       
            CreateMap<Bank, BankDTO>();
            CreateMap<BankDTO, Bank>();
            CreateMap<Bank, BankNamesDTO>();

            CreateMap<ContractorDTO2, Contractor>().ReverseMap();
            CreateMap<Building, BuildingDTO>();
            CreateMap<Mortagege, MortagegeDTO>().ReverseMap();

            CreateMap<TenantDTO, Tenant>().ReverseMap();

        }
    }
}
