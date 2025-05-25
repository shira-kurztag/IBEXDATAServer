using IBEXDATA.Models;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

using Serilog;

using System;

using System.Collections.Generic;

using System.IO;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace DB

{

    public class FilesDB : IFilesDB

    {

        //private readonly ILogger<FilesDB> _logger; 

        private readonly dbContext _context;

        private static readonly Serilog.ILogger _logger = Log.ForContext<FilesDB>(); // Create a logger instance 





        public FilesDB(dbContext context)

        {

            // _logger = logger; 

            _context = context;

        }



        // Get all files 

        public async Task<Magardoc[]> GetFilesByUniqIdAsync(string uniqId)

        {

            try

            {

                // חיפוש כל המסמכים בעלי אותו uniqId 

                return await _context.Magardocs

                                     .Where(file => file.UniqId == uniqId)

                                     .ToArrayAsync();

            }

            catch (Exception ex)

            {

                throw new Exception($"Error occurred while fetching files from the database with UniqId {uniqId}.", ex);

            }

        }



        /*   public async Task<Magardoc> AddMagardocAsync(Magardoc magardoc) 

           { 

               try 

               { 

                   // הוספת האובייקט לטבלה 

                   _context.Magardocs.Add(magardoc); 

                   await _context.SaveChangesAsync(); // שמירת השינויים 

                   return magardoc; 

               } 

               catch (Exception ex) 

               { 

                   _logger.LogError(ex, "Error adding Magardoc to the database"); 

                   throw; 

               } 

           }*/



        public async Task<Magardoc> Add(Magardoc magardoc)
        {
            try
            {
                await _context.Magardocs.AddAsync(magardoc);
                await _context.SaveChangesAsync();

                if (magardoc != null)
                {
                    _logger.Information("Successfully added a new project.");
                    return magardoc;
                }
                else
                {
                    _logger.Warning("Project was not added.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in Add method of Add in ProjectDB.");
                return null;
            }
        }
        // Delete file by ID 

        public async Task DeleteFileAsync(int id)

        {

            var file = await _context.Magardocs.FindAsync(id);

            if (file != null)

            {

                _context.Magardocs.Remove(file);

                await _context.SaveChangesAsync();

            }

        }



        //public Task<Magardoc> AddMagardocAsync(Magardoc magardoc) 

        //{ 

        //    throw new NotImplementedException(); 

        //} 



        /*  public Task<Magardoc> AddMagardocAsync(Magardoc magardoc) 

          { 

              throw new NotImplementedException(); 

          } 

  */



        public async Task<Magardoc> Update(int id, Magardoc magardoc)

        {

            try

            {

                magardoc.Id = id;

                _context.Magardocs.Update(magardoc);

                await _context.SaveChangesAsync();



                _logger.Information("Successfully updated Project with ID {ProjectId}.", id);

                return magardoc;

            }

            catch (Exception ex)

            {

                _logger.Error(ex, "Error in Update method of ProjectDB.");

                return null;

            }

            // _context.Entry(existingProject).CurrentValues.SetValues(project); 



        }



        public Task<int> GetById(int id)

        {

            throw new NotImplementedException();

        }



        public async Task<TipeFile> GetIdFile(string fileName)

        {

            try

            {



                var tipeFiles = _context.TipeFiles.FirstOrDefaultAsync(x => x.Description == fileName);

                return await tipeFiles;

            }

            catch (Exception ex)

            {

                _logger.Error(ex, "Error fetching files from the database");

                throw;

            }

        }



        public async Task<Magardoc?> GetFileMetadataAsync(string fileName)

        {

            return await _context.Magardocs

                .FirstOrDefaultAsync(m => m.FileNameShow == fileName);

        }

    }



}