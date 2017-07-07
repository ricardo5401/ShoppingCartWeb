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
using ShoppingCartWeb.Helpers;
using Stripe;

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
            Console.WriteLine("AddresPaymet - New Order");
            var user = await _userManager.GetUserAsync(HttpContext.User);
            

            var cart = ShoppingCartRepository.GetCart(HttpContext);
            var CartId = SessionHelper.GetShoppingCartId(HttpContext);
            var items = cart.GetCartItems(CartId);

            Order order = new Order();

            decimal total= 0; 

            foreach(var item in items.CartItems){
                total+= item.GetAlbum().Price;
            }
            order.Total = total;
            // check to migrate
            CheckCartId(user.UserName);
            if (user != null)
            {
                order.Email = user.Email;
                order.Username = user.UserName;
                order.Phone = user.PhoneNumber;
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult AddressAndPayment(Order orderVM)
        {
            Console.WriteLine("AddresPaymet - Checking model state");
            if (ModelState.IsValid)
            {
                var cart = ShoppingCartRepository.GetCart(HttpContext);
                var order = cart.CreateOrder(orderVM);

                if (order != null)
                {
                    Console.WriteLine("AddresPaymet - Created with OrderId: " + order.OrderId);
                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
                else
                {
                    Console.WriteLine("AddresPaymet - Order Rejected, check API logs");
                    return Ok(orderVM);
                }
            }
            else
            {
                Console.WriteLine("AddresPaymet - Invalid Model");
                return Ok(orderVM);
            }
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            Console.WriteLine("Complete order, OrderId: " + id);
            return View(id);
        }
        [HttpPost]
        public ActionResult Payment(string stripeEmail, string stripeToken, string monto)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken,

            });

            var charge = charges.Create(new StripeChargeCreateOptions
            {
                Amount = Convert.ToInt32(monto),
                Description = "Sample Charge",
                Currency = "usd",
                CustomerId = customer.Id

            });


            return RedirectToAction("Index", "Home");
        }

        private void CheckCartId(string UserName)
        {
            Console.WriteLine("Checking CartId");
            var currentCartId = SessionHelper.GetShoppingCartId(HttpContext);
            if (currentCartId != UserName)
            {
                SessionHelper.SetShoppingCartId(HttpContext, UserName);
                var cart = ShoppingCartRepository.GetCart(HttpContext);
                cart.MigrateCart(currentCartId, UserName);
            }
        }
    }
}