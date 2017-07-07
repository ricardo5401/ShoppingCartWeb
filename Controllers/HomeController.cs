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
using ShoppingCartWeb.Filters;
using Microsoft.Extensions.Logging;

namespace ShoppingCartWeb.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))]
    public class HomeController : Controller
    {
        
        private readonly ILogger _logger;

        public HomeController(ILoggerFactory loggerFactory){
            _logger = loggerFactory.CreateLogger("HomeController");
        }
        
        public IActionResult Index()
        {
            var key = SessionHelper.GetShoppingCartId(HttpContext);
            Console.WriteLine("HomeController Index - CartId: " + key);
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
    }
}
