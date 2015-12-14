using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeature.Models;
using System.Text;

namespace LanguageFeature.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            //create a new Product object
            Product product = new Product();

            //set the property value
            product.ProductID = 102;
            product.Name = "Loki Alice";

            //get the property
            string productName = product.Name;

            //generate the view
            return View("Result", (object)String.Format("Product Name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            //create a new product object
            Product product = new Product();

            //set the property values
            product.ProductID = 100;
            product.Name = "Loki Alice";
            product.Price = 275M;
            product.Description = " A boat for one person";
            product.Category = "Watersports";

            return View("Result", (object)String.Format("Category: {0}", product.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] fruits = { "apple", "orange", "plum" };

            List<int> quantity = new List<int> { 10, 20, 30, 40 };

            Dictionary<string, int> fruitsDict = new Dictionary<string, int> {
                {"apple",10},{"orange",20},{"plum",40}
            };

            return View("Result", (object)fruitsDict["apple"]);
        }

        public ViewResult UseExtensionEnumerble()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>{
                    new Product { Name = "Kayak", Price = 209M},
                    new Product { Name = "Loki", Price = 291M},
                    new Product { Name = "Alice", Price = 201M}
                }
            };
            
            //create and populate an array of products objects
            Product[] prodArray = {
                new Product { Name = "Kayak", Price = 209M},
                new Product { Name = "Loki", Price = 291M},
                new Product { Name = "Alice", Price = 221M}
            };

            //get the total value of product in the cart
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = prodArray.TotalPrices();

            return View("Result", (object)String.Format("Cart Total: {0}, Array Total: {1}", cartTotal, arrayTotal));
        }

        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product { Name = "KayAk", Price = 291M, Category = "Watersports"},
                    new Product { Name = "LifeJacket", Price = 282M, Category = "Soccer"},
                    new Product { Name = "Soccer Ball", Price = 212M, Category = "Soccer"}
                }
            };

            decimal total = 0;
            foreach (Product prod in products.FilterByCategory("Soccer"))
            {
                total = total + prod.Price;
            }

            return View("Result", (object)String.Format("Total: {0}", total));
        }
        

        //using the fillter extension method with a func
        public ViewResult UseFilterExtensionWithFunc()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product { Name = "KayAk", Price = 291M, Category = "Watersports"},
                    new Product { Name = "LifeJacket", Price = 282M, Category = "Soccer"},
                    new Product { Name = "Soccer Ball", Price = 212M, Category = "Soccer"}
                }
            };

            Func<Product, bool> categoryFilter = delegate(Product prod)
            {
                return prod.Category == "Soccer";
            };

            decimal total = 0;
            foreach (Product prod in products.Filter(categoryFilter))
            {
                total = total + prod.Price;
            }

            return View("Result", (object)String.Format("Total: {0}", total));
        }
        //using the fillter extension method with a lambda
        public ViewResult UseFilterExtensionWithLambda()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product { Name = "KayAk", Price = 291M, Category = "Watersports"},
                    new Product { Name = "LifeJacket", Price = 282M, Category = "Soccer"},
                    new Product { Name = "Soccer Ball", Price = 212M, Category = "Soccer"}
                }
            };

            Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";

            decimal total = 0;
            foreach (Product prod in products.Filter(categoryFilter))
            {
                total = total + prod.Price;
            }

            return View("Result", (object)String.Format("Total: {0}", total));
        }
        //using the fillter extension method with lambda without func
        public ViewResult UseFilterExtensionLambdaWithoutFunc()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product { Name = "KayAk", Price = 291M, Category = "Watersports"},
                    new Product { Name = "LifeJacket", Price = 282M, Category = "Soccer"},
                    new Product { Name = "Soccer Ball", Price = 212M, Category = "Soccer"},
                    new Product { Name = "Soccer Ball", Price = 10M, Category = "Soccer"},
                    new Product { Name = "Soccer Ball", Price = 2M, Category = "Soccer"}
                }
            };           

            decimal total = 0;
            foreach (Product prod in products.Filter(prod => prod.Category == "Soccer"))
            {
                total = total + prod.Price;
            }

            return View("Result", (object)String.Format("Total: {0}", total));
        }

        //extending the filtering expressed by the lambda expression
        public ViewResult UseFilterExtensionExtendingLambdaExpression()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product { Name = "KayAk", Price = 291M, Category = "Watersports"},
                    new Product { Name = "LifeJacket", Price = 282M, Category = "Soccer"},
                    new Product { Name = "Soccer Ball", Price = 212M, Category = "Soccer"},
                    new Product { Name = "Soccer Ball", Price = 10M, Category = "Soccer"},
                    new Product { Name = "Soccer Ball", Price = 2M, Category = "Soccer"}
                }
            };           

            decimal total = 0;
            foreach (Product prod in products.Filter(prod => prod.Category == "Soccer" || prod.Price > 20))
            {
                total = total + prod.Price;
            }

            return View("Result", (object)String.Format("Total: {0}", total));
        }

        //creating an array of anonymously typed object
        public ViewResult CreateAnonArray()
        {
            var oddAndEnds = new[]{
                new { Name= "MVC", Category = "Pattern"},
                new { Name= "Hat", Category = "Clothing"},
                new { Name= "Apple", Category = "Fruits"},
                new { Name= "Orange", Category = "Fruits"},
            };

            StringBuilder result = new StringBuilder();
            foreach (var item in oddAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }

            return View("Result", (object)result.ToString());
        }

        //query without linq
        public ViewResult FindProducts()
        {
            Product[] products = { 
                new Product {Name = "KayAk",Category = "Watersport", Price = 275M},
                new Product {Name = "Lifejacket",Category = "Watersport", Price = 49.8M},
                new Product {Name = "Soccer Ball",Category = "Soccer", Price = 75M},
                new Product {Name = "Corrner flag",Category = "Soccer", Price = 7.5M},
            };

            //define the array to hold result
            Product[] foundProducts = new Product[4];

            //sort the contents of the array
            Array.Sort(products, (item1, item2) =>
            {
                return Comparer<decimal>.Default.Compare(item1.Price, item2.Price);
            });

            //get the first three items in the array as the results
            Array.Copy(products, foundProducts, 4);

            //create the result
            StringBuilder result = new StringBuilder();
            foreach (Product p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
            }
            return View("Result",(object)result.ToString());
        }

        // using linq to query data
        public ViewResult FindProductsUsingLinQ()
        {
            Product[] products = { 
                new Product {Name = "KayAk",Category = "Watersport", Price = 275M},
                new Product {Name = "Lifejacket",Category = "Watersport", Price = 49.8M},
                new Product {Name = "Soccer Ball",Category = "Soccer", Price = 75M},
                new Product {Name = "Corrner flag",Category = "Soccer", Price = 7.5M},
            };
            var foundProducts = from match in products
                                orderby match.Price ascending
                                select new
                                {
                                    match.Name,
                                    match.Price
                                };
            //create the result
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }           
            return View("Result", (object)result.ToString());
        }

        //using linq dot notation
        public ViewResult FindProductsUsingLinQDotNotation()
        {
            Product[] products = { 
                new Product {Name = "KayAk",Category = "Watersport", Price = 275M},
                new Product {Name = "Lifejacket",Category = "Watersport", Price = 49.8M},
                new Product {Name = "Soccer Ball",Category = "Soccer", Price = 75M},
                new Product {Name = "Corrner flag",Category = "Soccer", Price = 7.5M},
            };
            var foundProducts = products.OrderByDescending(e => e.Price)
                                                          .Take(3)
                                                          .Select(e => new { 
                                                            e.Name,
                                                            e.Price
                                                          });
            //create the result
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }
            return View("Result", (object)result.ToString());
        }

        //using deferred linq extension methods in a query
        public ViewResult FindProductsUsingDeferredLinQ()
        {
            Product[] products = { 
                new Product {Name = "KayAk",Category = "Watersport", Price = 275M},
                new Product {Name = "Lifejacket",Category = "Watersport", Price = 49.8M},
                new Product {Name = "Soccer Ball",Category = "Soccer", Price = 75M},
                new Product {Name = "Corrner flag",Category = "Soccer", Price = 7.5M},
            };
            var foundProducts = products.OrderByDescending(e => e.Price)
                                                          .Take(3)
                                                          .Select(e => new
                                                          {
                                                              e.Name,
                                                              e.Price
                                                          });
            products[2] = new Product { Name = "Kaka", Category = "Soccer", Price = 4225M };
            //create the result
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }
            return View("Result", (object)result.ToString());
        }

        //an immediately excuted LINQ Query
        public ViewResult SumProducts()
        {
            Product[] products = { 
                new Product {Name = "KayAk",Category = "Watersport", Price = 275M},
                new Product {Name = "Lifejacket",Category = "Watersport", Price = 49.8M},
                new Product {Name = "Soccer Ball",Category = "Soccer", Price = 75M},
                new Product {Name = "Corrner flag",Category = "Soccer", Price = 7.5M},
            };
            var result = products.Sum(e => e.Price);
            products[2] = new Product { Name = "Lily", Category = "Soccer", Price = 345M };

            return View("Result", (object)String.Format("Sum: {0:c}",result));
        }
    }
}
