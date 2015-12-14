using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace LanguageFeature.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> produtEnum)
        {
            decimal total = 0;
            foreach (Product prod in produtEnum)
            {
                total = total + prod.Price;
            }
            return total;
        }

        public static IEnumerable<Product> FilterByCategory (
            this IEnumerable<Product> prodEnum, string categoryParam)
        {
            foreach (Product prod in prodEnum)
            {
                if (prod.Category == categoryParam)
                {
                    yield return prod;
                }
            }
        }

        public static IEnumerable<Product> Filter (
            this IEnumerable<Product> prodEnum, Func<Product, bool> selectorParam){

                foreach (Product prod in prodEnum)
                {
                    if (selectorParam(prod))
                    {
                        yield return prod;
                    }
                }
        }

    }
}