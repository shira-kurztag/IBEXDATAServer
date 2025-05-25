
/*using AutoMapper; 

*/

using IBEXDATA.Models;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using Service;

using Newtonsoft.Json;

using DB;

using Serilog;

using AutoMapper;

using Common.DTO;

using Newtonsoft.Json.Linq;



namespace Application.Controllers

{

    [Route("api/[controller]")]

    [ApiController]

    public class FilesController : ControllerBase

    {

        private readonly IFileService _fileService;

        private static readonly Serilog.ILogger _logger = Log.ForContext<FilesController>(); // Create a logger instance 

        private readonly IMapper _mapper;





        public FilesController(IFileService fileService, dbContext context, IMapper mapper)

        {

            _fileService = fileService;



            _mapper = mapper;

        }



        [HttpGet("GetUniqId/{uniqId}")]

        public async Task<IActionResult> GetFilesByUniqId(string uniqId)

        {

            if (string.IsNullOrEmpty(uniqId))

            {

                return BadRequest("UniqId is required.");

            }



            var files = await _fileService.GetFilesByUniqId(uniqId);



            if (files != null && files.Any())

            {

                return Ok(files);

            }



            return NotFound($"No files found with UniqId {uniqId}.");

        }


        [HttpPost]
        public async Task<IActionResult> Add([FromForm] IFormFile file, [FromForm] string magardocJson)
        {
            if (file == null || string.IsNullOrEmpty(magardocJson))
            {
                _logger.Warning("File or magardocJson is missing.");
                return BadRequest("File or metadata is missing.");
            }

            if (file.Length == 0)
            {
                _logger.Warning("Received an empty file: {FileName}", file.FileName);
                return BadRequest("File is empty.");
            }

            try
            {
                _logger.Information("File received: {FileName}, Length: {FileLength}, ContentType: {ContentType}",
                    file.FileName, file.Length, file.ContentType);
                _logger.Information("Received magardocJson: {MagardocJson}", magardocJson);

                // שלב 1: עיבוד ה-JSON 
                var magardoc = JsonConvert.DeserializeObject<Magardoc>(magardocJson);
                if (magardoc == null)
                {
                    _logger.Warning("Deserialization returned null for magardocJson: {MagardocJson}", magardocJson);
                    return BadRequest("Invalid Magardoc data.");
                }

                // שלב 2: שמירת הקובץ בנתיב עם השם המקורי 
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FILES");

                // יצירת התיקיה אם אינה קיימת
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    _logger.Information("Directory created: {DirectoryPath}", directoryPath);
                }

                // שמירה עם שם הקובץ המקורי 
                var sanitizedFileName = Path.GetFileName(file.FileName); // שמירת השם המקורי 
                var filePath = Path.Combine(directoryPath, sanitizedFileName);

                _logger.Information("Saving file to path: {FilePath}", filePath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                _logger.Information("File saved successfully at path: {FilePath}", filePath);

                // עדכון שם הקובץ ב-`FileNameShow` 
                magardoc.FileNameShow = sanitizedFileName;

                // שלב 3: שמירת הנתונים למסד הנתונים 
                var result = await _fileService.Add(magardoc);
                if (result != null)
                {
                    _logger.Information("Magardoc saved successfully: {Magardoc}", JsonConvert.SerializeObject(magardoc));
                    return Ok(result);
                }
                else
                {
                    _logger.Warning("Failed to save Magardoc: {Magardoc}", JsonConvert.SerializeObject(magardoc));
                    return BadRequest("Failed to save Magardoc data.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding file and metadata.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteFile(int id)

        {

            await _fileService.DeleteFile(id);

            return Ok();

        }



        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromQuery] Magardoc magardoc)

        {

            try

            {

                var updated = await _fileService.Update(id, magardoc);

                return Ok(updated);

            }

            catch (ArgumentException ex)

            {

                return BadRequest(new { message = ex.Message });

            }

            catch (Exception ex)

            {

                _logger.Error(ex, "Failed to update the file or database for ID: {Id}", id);

                return StatusCode(500, "An error occurred while processing your request.");

            }

        }



        //מקבל שדם של טופס ומחזיר את הID של הקובץ 

        [HttpGet("GetIdFile/{fileName}")]

        public async Task<ActionResult<TipeFile>> GetIdFile(string fileName)

        {

            var tipeFile = await _fileService.GetIdFile(fileName);

            if (tipeFile == null)

            {

                return NotFound();

            }



            var tipeFileDto = _mapper.Map<TipeFile,TipeFileDTO >(tipeFile);

            return Ok(tipeFileDto);

        }



        [HttpGet("download")]

        public IActionResult DownloadFile(string fileName)

        {

            // בדיקה אם שם הקובץ ריק או חסר 

            if (string.IsNullOrEmpty(fileName))

            {

                return BadRequest("File name is required.");

            }



            // שלב 1: בדיקת קיום הנתיב 

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Common", "File", fileName);



            if (!System.IO.File.Exists(filePath))

            {

                _logger.Warning("File not found: {FileName}", fileName);

                return NotFound("הקובץ לא נמצא במערכת.");

            }



            try

            {

                // שלב 2: קבלת סוג MIME לפי שם הקובץ 

                var contentType = GetMimeType(fileName);



                // שלב 3: קריאת תוכן הקובץ 

                var fileBytes = System.IO.File.ReadAllBytes(filePath);



                // שלב 4: הוספת כותרת להורדה כפויה 

                Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");



                // שלב 5: החזרת הקובץ ללקוח 

                return File(fileBytes, contentType, fileName);

            }

            catch (Exception ex)

            {

                _logger.Error(ex, "Error downloading file: {FileName}", fileName);

                return StatusCode(500, "An error occurred while trying to download the file.");

            }

        }

        // פונקציה לקבלת סוג ה-MIME לפי סיומת הקובץ 

        private string GetMimeType(string fileName)

        {

            var extension = Path.GetExtension(fileName).ToLowerInvariant();



            return extension switch

            {

                ".jpg" => "image/jpeg",

                ".jpeg" => "image/jpeg",

                ".png" => "image/png",

                ".gif" => "image/gif",

                ".bmp" => "image/bmp",

                ".tiff" => "image/tiff",

                ".pdf" => "application/pdf",

                ".doc" => "application/msword",

                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",

                ".xls" => "application/vnd.ms-excel",

                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",

                ".txt" => "text/plain",

                ".csv" => "text/csv",

                _ => "application/octet-stream" // ברירת מחדל 

            };

        }

    }

}



