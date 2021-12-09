using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OddballStore.Data;
using OddballStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace OddballStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

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
            var json = HttpContext.Session.GetString("cart");
            List<CartItem> cartList = new List<CartItem>();

            if (json != null)
            {
                cartList = JsonSerializer.Deserialize<List<CartItem>>(json);
            }

            return View(cartList);
        }




        [AllowAnonymous]
        public async Task<IActionResult> Items()
        {
            return View(await _context.Items.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Items([Bind("ItemID, Name, Description, Price, Thumbnail")] Item item)
        {
            List<CartItem> cartList;

            var json = HttpContext.Session.GetString("cart");
            if (json != null)
            {
                cartList = JsonSerializer.Deserialize<List<CartItem>>(json);
            }
            else
            {
                cartList = new List<CartItem>();
            }

            var itemInCart = false;

            if (cartList.Count > 0)
            {
                for (int i = 0; i < cartList.Count; i++)
                {
                    if (cartList[i].Item.ItemID == item.ItemID)
                    {
                        cartList[i].Quantity += 1;
                        itemInCart = true;
                    }
                }
            }

            if (!itemInCart)
            {
                cartList.Add(new CartItem(item, 1));
            }

            json = JsonSerializer.Serialize(cartList);
            HttpContext.Session.SetString("cart", json);

            return View(await _context.Items.ToListAsync());
        }

        [HttpPost]
        public IActionResult Checkout()
        {

            List<CartItem>  cartList = new List<CartItem>();
            var json = JsonSerializer.Serialize(cartList);
            HttpContext.Session.SetString("cart", json);

            return View("Cart", cartList);
        }

        [HttpPost]
        public IActionResult Quantity(int quantityNum, int itemID)
        {
            var json = HttpContext.Session.GetString("cart");
            List<CartItem> cartList = new List<CartItem>();

            if (json != null)
            {
                cartList = JsonSerializer.Deserialize<List<CartItem>>(json);

                for (int i = 0; i < cartList.Count; i++)
                {
                    if (cartList[i].Item.ItemID == itemID)
                    {
                        cartList[i].Quantity = quantityNum;
                    }
                }

                json = JsonSerializer.Serialize(cartList);
                HttpContext.Session.SetString("cart", json);
            }

            return View("Cart", cartList);
        }

        [HttpPost]
        public IActionResult Delete(int itemID)
        {
            var json = HttpContext.Session.GetString("cart");
            List<CartItem> cartList = new List<CartItem>();

            if (json != null)
            {
                cartList = JsonSerializer.Deserialize<List<CartItem>>(json);

                for (int i = 0; i < cartList.Count; i++)
                {
                    if (cartList[i].Item.ItemID == itemID)
                    {
                        cartList.RemoveAt(i);
                    }
                }

                json = JsonSerializer.Serialize(cartList);
                HttpContext.Session.SetString("cart", json);
            }

            return View("Cart", cartList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
