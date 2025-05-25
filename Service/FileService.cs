
using DB;

using IBEXDATA.Models;

using Microsoft.Extensions.Logging;

//using Microsoft.AspNetCore.Http;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.IO;

using Serilog;



namespace Service

{

    public class FileService : IFileService

    {



        private readonly IFilesDB _filesDB;

        private readonly dbContext _context;

        private static readonly Serilog.ILogger _logger = Log.ForContext<FileService>(); // Create a logger instance 



        public FileService(IFilesDB filesDB, dbContext context)

        {

            _filesDB = filesDB;

            _context = context;

        }

        // Get all files 

        public async Task<Magardoc[]> GetFilesByUniqId(string uniqId)

        {

            if (string.IsNullOrEmpty(uniqId))

            {

                throw new ArgumentException("UniqId cannot be null or empty.");

            }



            try

            {

                // שליפת כל המסמכים בהתאמה ל-uniqId מה-DB 

                return await _filesDB.GetFilesByUniqIdAsync(uniqId);

            }

            catch (Exception ex)

            {

                throw new Exception($"Error occurred while fetching files with UniqId {uniqId}.", ex);

            }

        }



        public async Task DeleteFile(int id)

        {

            try

            {

                await _filesDB.DeleteFileAsync(id);

            }

            catch (Exception ex)

            {

                _logger.Error(ex, "Error deleting file with ID {FileId}", id);

                throw;

            }

        }



        /*       public Task<Magardoc> Add(Magardoc magardoc) 

               { 

                   throw new NotImplementedException(); 

               } 

 

               private async Task<byte[]> ConvertToByteArrayAsync(IFormFile file) 

               { 

                   using var memoryStream = new MemoryStream(); 

                   await file.CopyToAsync(memoryStream); 

                   return memoryStream.ToArray(); 

               }*/

        /* public async Task<Magardoc> Add(Magardoc magardoc) 

         { 

             return await _filesDB.Add(magardoc); 

         }*/


        public async Task<Magardoc> Add(Magardoc magardoc)
        {
            try
            {
                // הגדרת נתיב התיקייה 
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FILES");

                // יצירת התיקייה אם היא לא קיימת 
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    _logger.Information("Directory created: {DirectoryPath}", directoryPath);
                }

                // יצירת הנתיב המלא של הקובץ 
                var filePath = Path.Combine(directoryPath, magardoc.FileNameShow);

                // כתיבת תוכן הקובץ לקובץ (אם יש קובץ תוכן לשמור)
                await System.IO.File.WriteAllTextAsync(filePath, magardoc.FileNameShow);
                _logger.Information("File content saved to: {FilePath}", filePath);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error saving file {FileNameShow} to directory.", magardoc.FileNameShow);
                throw new Exception($"Error saving file {magardoc.FileNameShow} to directory", ex);
            }

            // שמירת הנתונים בבסיס הנתונים 
            return await _filesDB.Add(magardoc);
        }


        public async Task<Magardoc> Update(int id, Magardoc magardoc)

        {

            // עדכון האובייקט במסד הנתונים 

            var updatedRecord = await _filesDB.Update(id, magardoc);



            // בדיקה אם FileNameShow קיים ולא ריק, ועדכון תוכן הקובץ 

            if (!string.IsNullOrEmpty(magardoc.FileNameShow))

            {

                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Common", "File");

                var filePath = Path.Combine(directoryPath, magardoc.FileNameShow);



                // יצירת התיקייה אם היא לא קיימת 

                if (!Directory.Exists(directoryPath))

                {

                    Directory.CreateDirectory(directoryPath);

                }



                // כתיבת תוכן חדש לקובץ 

                await System.IO.File.WriteAllTextAsync(filePath, magardoc.FileNameShow);

            }



            return updatedRecord;

        }



        public async Task<TipeFile> GetIdFile(string fileName)

        {

            try

            {

                return await _filesDB.GetIdFile(fileName);

            }

            catch (Exception ex)

            {

                _logger.Error(ex, "Error retrieving files");

                throw;

            }

        }



        public async Task<(Magardoc?, string)> GetFileAsync(string fileName)

        {

            // שליפת המידע על הקובץ 

            var fileMetadata = await _filesDB.GetFileMetadataAsync(fileName);



            if (fileMetadata == null)

            {

                return (null, "קובץ זה אינו קיים במערכת.");

            }



            // הגדרת הנתיב של הקובץ בשרת 

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Common", "File", fileName);



            if (!File.Exists(filePath))

            {

                return (null, "הקובץ לא נמצא בשרת.");

            }



            return (fileMetadata, filePath);

        }

    }



}
