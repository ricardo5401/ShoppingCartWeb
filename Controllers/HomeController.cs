﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartWeb.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ShoppingCartWeb.Helpers;
using ShoppingCartWeb.Repositories;
using ShoppingCartWeb.Network;

namespace ShoppingCartWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var key = HomeController.GetShoppingCartId(HttpContext);
            Console.WriteLine(key);
            var albumRepository =  new AlbumRepository();             
            return View(albumRepository.GetAll());
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }

        public static string GetShoppingCartId(HttpContext context)
        {
            var key = context.Session.GetString("CartId");
            if (string.IsNullOrEmpty(key))
            {
                key = context.Session.Id;
                context.Session.SetString("CartId", key);
            }
            return key;
        }
    }
}
