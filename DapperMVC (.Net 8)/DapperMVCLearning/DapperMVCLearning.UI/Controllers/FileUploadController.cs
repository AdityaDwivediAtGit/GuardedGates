using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nClam;
using DapperMVCLearning.VirusScanner;

namespace DapperMVCLearning.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IFileScanner _fileScannerService;
        public FileUploadController(IConfiguration configuration)
        {
            _configuration = configuration;
            _fileScannerService = new ClamFileScanner(_configuration["ClamAVServer:URL"], int.Parse(_configuration["ClamAVServer:Port"]));
        }


        [HttpGet]
        public IActionResult UploadFileForm()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0) return Content("file not selected");

            var ms = new MemoryStream();
            file.OpenReadStream().CopyTo(ms);
            byte[] fileBytes = ms.ToArray();
            //string Result = string.Empty;
            //ClamFileScanner _fileScannerService = new ClamFileScanner(_configuration["ClamAVServer:URL"], _configuration["ClamAVServer:Port"]);
            var scanResult = await _fileScannerService.ScanFileAsync(fileBytes);

            return Ok(scanResult);

            //return Ok(Result);
        }
    }
}
