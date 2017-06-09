using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartWeb.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace ShoppingCartWeb.Controllers
{
    public class AlbumController : Controller
    {
        public IActionResult MostrarDetalle(int id)
        {
            var httpClient = new HttpClient();
            
                var response = httpClient.GetAsync("http://localhost:5001/api/albums/"+id).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                
                Console.WriteLine(result);
                Albums albums = JsonConvert.DeserializeObject<Albums>(result);
                Console.WriteLine(albums.ArtistId);
                
            return View(albums);
        }

    }
}
