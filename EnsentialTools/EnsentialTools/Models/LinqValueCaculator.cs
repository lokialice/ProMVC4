using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnsentialTools.Models
{
    public class LinqValueCaculator:IValueCalculator
    {
        private IDiscountHelper discounter;

        public LinqValueCaculator(IDiscountHelper discountParam)
        {
            this.discounter = discountParam;
        }

        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return discounter.ApplyDiscount(products.Sum(p => p.Price));
        }
    }
}