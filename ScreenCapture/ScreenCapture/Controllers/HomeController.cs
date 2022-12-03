using Google;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScreenCapture.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace ScreenCapture.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _environment;



        public object RequestID { get; private set; }


        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _environment = env;
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
        [HttpPost]
        public IActionResult UploadImage()
        {
            string base64 = Request.Form["Screenshot"];
            byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
            string filename = DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";

            string filePath = Path.Combine(_environment.WebRootPath, "Screenshot", filename);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            return RedirectToAction("Screenshot");
        }
    
        [HttpPost]
        public IActionResult Save()
        {
            string base64 = Request.Form["imgCropped"];
            byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
            string filename = DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";

                string filePath = Path.Combine(_environment.WebRootPath, "Images", filename);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
            return RedirectToAction("Index");
        }
    }
}


