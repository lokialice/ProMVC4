using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnsentialTools.Models;
using Ninject;

namespace EnsentialTools.Controllers
{
    public class HomeController : Controller
    {
        private Product[] products = { 
            new Product { Name = "Loki", Category = "Watesports", Price = 275M},
            new Product { Name = "Alice", Category = "Soccer", Price = 48.91M},
            new Product { Name = "Soccerl Ball", Category = "Soccer", Price = 19.05M},
            new Product { Name = "Conner flag", Category = "Soccer", Price = 34.95M}
        };

        private IValueCalculator calc;
        public HomeController(IValueCalculator calcParam)
        {
            this.calc = calcParam;
        }

        public ActionResult Index()
        {            
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };

            decimal totalValue = cart.CalculatorProductTotal();
            return View(totalValue);
        }

    }
}
