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
                return View("AllUsers", _context.Users.ToList());
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                //go to different view
                return View("AllUsers", _context.Users.ToList());
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string email, [Bind("Id, Email, PhoneNumber")] User user)
        {
            var saved = false;

            user.UserName = email;

            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is User)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                proposedValues[property] = proposedValue;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }
            return View("AllUsers", _context.Users.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string? id)
        {

            if (id == null)
            {
                return View("AllUsers", _context.Users.ToList());
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                //go to different view
                return View("AllUsers", _context.Users.ToList());
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return View("AllUsers", _context.Users.ToList());
        }


        private bool UserExists(string id) => _context.Users.Any(e => e.Id == id);
    }
}
