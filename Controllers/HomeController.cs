using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OddballStore.Data;
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
        private readonly ApplicationDbContext _context;
        private UserManager<User> userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<User> usrMgr)
        {
            _logger = logger;
            _context = context;
            userManager = usrMgr;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Cart()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Checkout()
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

        [Authorize(Roles = "Admin")]
        public ActionResult AllUsers()
        {
            // _context.Users.ToList();

            return View(userManager.Users.ToList());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Items()
        {
            return View(await _context.Items.ToListAsync());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
