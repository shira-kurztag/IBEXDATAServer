using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using IBEXDATA.Models;

namespace DB
{
    public interface IProjectDB
    {
       Task<Project> Add(Project project);
       Task<Project> GetById(int id);
       Task<Contractor> GetCompanyById(int id);
       Task<Project> GetByName(string projectName);
       Task<bool> Delete(int id);
       Task<Project> Update(int id, Project project);
       Task<List<Contractor>> GetAllContractors();
        Task<List<Project>> GetProjecctByContractor(int id);

    }
}
