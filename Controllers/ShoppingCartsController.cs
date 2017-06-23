using ShoppingCartWeb.Models;
using ShoppingCartWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartWeb.Repositories;
using System.Linq;

namespace ShoppingCartWeb.Controllers
{
    public class ShoppingCartsController : Controller
    {        
        // GET: ShoppingCart
        public ActionResult Index()
        {
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
            };
            // Return the view
            return View(viewModel);
        }
}