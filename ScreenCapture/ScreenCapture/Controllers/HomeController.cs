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
        public ContentResult UploadImage(string imageData)
        {
            try
            {
                string fileName = DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".png";
                string path = Path.Combine(_environment.WebRootPath, "Screenshot");

                if (Directory.Exists(path))
                {
                    using (FileStream fs = new FileStream(Path.Combine(path,fileName), FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            byte[] data = Convert.FromBase64String(imageData);
                            bw.Write(data);
                            bw.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Content("Uploaded");
        }
    
    [HttpPost]
    public IActionResult Save()
    {
        string base64 = Request.Form["imgCropped"];
        byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);

        string filePath = Path.Combine(_environment.WebRootPath, "Images", "Cropped.png");
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }
        return RedirectToAction("Index");
    }
    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}

    }
}


