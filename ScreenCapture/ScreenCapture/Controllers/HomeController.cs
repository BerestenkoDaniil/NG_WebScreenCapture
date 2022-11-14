using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using Org.BouncyCastle.Asn1.X509;
using ScreenCapture.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.InkML;
using System.Net;

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
        [HttpGet]
        public IActionResult FileDownload(string filename)
        {
            string DownloadFileName = filename;
            if (filename != null)
            {
                var Folder = RequestID.ToString();
                string fileview = Path.Combine(_env.WebRootPath, "Documents", Folder, filename);
                WebClient User = new WebClient();
                Byte[] fileBuffer = System.IO.File.ReadAllBytes(fileview);
                if (fileBuffer != null)
                {
                    //return File(fileBuffer, "application/octet-stream", filename);
                }
            }
            return null;

        }
    }
}