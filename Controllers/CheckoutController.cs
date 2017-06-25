using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingCartWeb.Controllers
{
   
    [Authorize]
    public class CheckoutController : Controller
    {
        public ActionResult AddressAndPayment()
        {
            return View("AddressAndPayment");
        }

    }
}