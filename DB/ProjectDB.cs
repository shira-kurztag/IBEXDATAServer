using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DB
{
    public class ProjectDB : IProjectDB
    {
        private readonly dbContext _context;
        private static readonly Serilog.ILogger _logger = Log.ForContext<ProjectDB>(); // Create a logger instance

        public ProjectDB(dbContext context)
        {
            _context = context;
        }

        public async Task<Project> Add(Project project)
        {
            try
            {
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();

                if (project != null)
                {
                    _logger.Information("Successfully added a new project.");
                    return project;
                }
                else
                {
                    _logger.Warning("project was not added.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Add method of Add in ProjectDB.");
                return null;
            }
        }

        public async Task<Project> GetById(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<Contractor> GetCompanyById(int id)
        {
            return await _context.Contractors.FirstOrDefaultAsync(c => c.ContractorId == id);
        }

        public async Task<Project> GetByName(string projectName)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.ProjectName == projectName);
        }

        public async Task<List<Project>> GetProjecctByContractor(int id)
        {
            return await _context.Projects.Where(p => p.ContractingCompanyId == id).ToListAsync();
        }


        public async Task<bool> Delete(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
            if (project == null)
            {
                _logger.Warning("Project with ID {ProjectId} not found.", id);
                return false;
            }

            project.ProjectStatus = 0;
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            _logger.Information("Successfully soft deleted Project with ID {ProjectId}.", id);
            return true;
        }

        public async Task<Project> Update(int id, Project project)
        {
            try
            {
                //var existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
                //if (existingProject == null)
                //{
                //    _logger.Warning("Project with ID {ProjectId} not found.", id);
                //    return null;
                //}

                project.ProjectId = id;
                _context.Projects.Update(project);
                await _context.SaveChangesAsync();

                _logger.Information("Successfully updated Project with ID {ProjectId}.", id);
                return project;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Update method of ProjectDB.");
                return null;
            }
            // _context.Entry(existingProject).CurrentValues.SetValues(project);

        }

        public async Task<List<Contractor>> GetAllContractors()
        {
            return await _context.Contractors.ToListAsync();
        }

 
    }
}
