using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IProjectService
    {
        Task<Project> Add(Project project);
        Task<Project> GetById(int id);
        Task<Contractor> GetCompanyById(int id);
        Task<Project> GetByName(string projectName);
        Task<bool> Delete(int id);
        Task<Project> Update(int id, Project project);
        Task<List<Contractor>> GetAllContractors();
    }
}
