using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartWeb.Models;

namespace ShoppingCartWeb.Controllers
{
   
    public class CheckoutController : Controller
    {
        public ActionResult Index()
        {
            return View("AddressAndPayment");
        }

    }
}