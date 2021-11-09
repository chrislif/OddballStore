using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OddballStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OddballStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        //update profile
        public IActionResult UpdateProfile()
        {
            if (ModelState.IsValid)
            {
                //TODO: Update DB record
                return View("Profile");
            }
            else
            {
                return View("Index");

            }
        }

        public IActionResult Items()
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
