using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoRazorSyntax.Models;

namespace DemoRazorSyntax.Controllers
{
    public class HomeController : Controller
    {
        Product myProduct = new Product { 
            ProductID = 1,
            Name = "Loki",
            Description = "A boat for one persion",
            Category = "Watersports",
            Price = 276M
        };

        public ActionResult Index()
        {
            return View(myProduct);
        }

        public ActionResult NameAndPrice()
        {
            return View(myProduct);
        }

        public ActionResult DemoExpression()
        {
            ViewBag.ProductCount = 1;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier = null;

            return View(myProduct);
        }

        public ActionResult DemoArray()
        {
            Product[] array = {
                new Product { Name = "Loki", Price = 321M},
                new Product { Name = "Alice", Price = 212M},
                new Product { Name = "Nhi", Price = 291M},
                new Product { Name = "Yen", Price = 21M}
            };
            return View(array);
        }
    }
}
