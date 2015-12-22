using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnsentialTools.Models
{
    public class ShoppingCart
    {
        private IValueCalculator calc;

        public ShoppingCart(IValueCalculator calcParam)
        {
            this.calc = calcParam;
        }

        public IEnumerable<Product> Products { get; set; }

        public decimal CalculatorProductTotal()
        {
            return this.calc.ValueProducts(Products);
        }
    }
}