using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DB;
using Common;
using Microsoft.EntityFrameworkCore;


namespace Service
{
    public class ProjectService : IProjectService
    {
        IProjectDB _ProjectDB;

        public ProjectService(IProjectDB ProjectDB)
        {
            _ProjectDB = ProjectDB;
        }

        public async Task<Project> Add(Project project)
        {
            return await _ProjectDB.Add(project);
        }

        public async Task<Project> GetById(int id)
        {
            return await _ProjectDB.GetById(id);
        }
        public async Task<Contractor> GetCompanyById(int id)
        {
            return await _ProjectDB.GetCompanyById(id);
        }
        public async Task<Project> GetByName(string projectName)
        {
            return await _ProjectDB.GetByName(projectName);
        }

        public async Task<bool> Delete(int id)
        {
            return await _ProjectDB.Delete(id);
        }

        public async Task<Project> Update(int id, Project project)
        {
            return await _ProjectDB.Update(id, project);
        }

        public async Task<List<Contractor>> GetAllContractors()
        {
            return await _ProjectDB.GetAllContractors(); // קרא ל-repository להשגת כל הקבלנים
        }

  
    }
}
