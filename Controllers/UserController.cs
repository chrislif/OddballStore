using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OddballStore.Data;
using OddballStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OddballStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ApplicationDbContext _context;

        public UserController(ILogger<UserController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ActionResult AllUsers()
        {
            return View(_context.Users.ToList());
        }

        //GET: Home/EditUser/s
        [HttpGet]
        public async Task<IActionResult> EditUser(string? id)
        {
            if (id == null)
            {
                return View();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return View();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, [Bind("Id, Email, UserName")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public ActionResult DeleteUser()
        {
            return View();
        }

        private bool UserExists(string id) => _context.Users.Any(e => e.Id == id);
    }
}
