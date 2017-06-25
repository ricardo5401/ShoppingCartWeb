using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartWeb.Models;
using ShoppingCartWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Owin;
using ShoppingCartWeb.Extensions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartWeb.Controllers
{

    [Authorize]
    public class CheckoutController : Controller
    {
        const string PromoCode = "FREE";
        private readonly UserManager<ApplicationUser> _userManager;
        public CheckoutController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<ActionResult> AddressAndPayment()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Order order = new Order();
            if (user != null)
            {
                order.Email = user.Email;
                order.Username = user.UserName;
                order.Phone = user.PhoneNumber;
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult AddressAndPayment([FromBody] Order orderVM)
        {
            if(ModelState.IsValid)
            {
                var cart = ShoppingCartRepository.GetCart(HttpContext);
                var order = cart.CreateOrder(orderVM);

                if (order != null)
                    return RedirectToAction("Complete", new { id = order.OrderId });
                else
                    return View(orderVM);
            }
            else
                return View(orderVM);
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            return View(id);
        }
    }
}