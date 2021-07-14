using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Lib;
using Microsoft.AspNetCore.Http;

namespace Market.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            Media.SelectImage(uploadedFile);
            if (Media.ObjectId == null)
                return RedirectToAction(Media.RequestControllerAction, Media.RequestControllerName);
            else
                return RedirectToAction( Media.RequestControllerAction, Media.RequestControllerName, new { id = Media.ObjectId }, null);
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

   
        public IActionResult MessageBox(string msg)
        {
            ViewBag.Message = msg;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
