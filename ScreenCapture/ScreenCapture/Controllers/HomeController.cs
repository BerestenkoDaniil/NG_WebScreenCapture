using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScreenCapture.Models;
using System.Diagnostics;
using System.IO;

namespace ScreenCapture.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private object _env;

        public object RequestID { get; private set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Screenshot()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //[HttpGet]
        //public IActionResult FileDownload(string filename)
        //{
        //    string DownloadFileName = filename;
        //    if (filename != null)
        //    {
        //        var Folder = RequestID.ToString();
        //        string fileview = Path.Combine(_env.WebRootPath, "Documents", Folder, filename);
        //        WebClient User = new WebClient();
        //        Byte[] fileBuffer = System.IO.File.ReadAllBytes(fileview);
        //        if (fileBuffer != null)
        //        {
        //            //return File(fileBuffer, "application/octet-stream", filename);
        //        }
        //    }
        //    return null;

        //}
        [HttpPost]
        public IActionResult Upload(FileModel model)
        {
            if (ModelState.IsValid)
            {

                string path = "/Screenshot";

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = model.FileName + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
            }
            return View();
        }
    }
}