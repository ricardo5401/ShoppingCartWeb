using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartWeb.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ShoppingCartWeb.Helpers;
using ShoppingCartWeb.Network;

namespace ShoppingCartWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", TokenManager.RequestToken());

            var response = httpClient.GetAsync(ShoppingCartAPI.AlbumURL).Result;
            var result = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(result);
            IEnumerable<Albums> user = JsonConvert.DeserializeObject<IEnumerable<Albums>>(result);
                
            return View(user);
        }
        //static IEnumerable<AspNetUsers> loadData(){

            /*HttpClient client= new HttpClient();
            HttpResponseMessage response=client.GetAsync("http://localhost:64137/API/user");
            HttpContent content= response.Content;*/
            
               /* string data= await content.ReadAsStringAsync();
                if(data!=null)
                {
                   
                    Console.WriteLine(data);
                }*/
            /*    var products = response.Content.ReadAsAsync<IEnumerable<AspNetUsers>>().Result;
                return products;
        }*/

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
