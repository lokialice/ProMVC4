﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using EnsentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnsentitalTool.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] products = { 
            new Product { Name = "Loki", Category = "Watesports", Price = 275M},
            new Product { Name = "Alice", Category = "Soccer", Price = 48.91M},
            new Product { Name = "Soccerl Ball", Category = "Soccer", Price = 19.05M},
            new Product { Name = "Conner flag", Category = "Soccer", Price = 34.95M}
        };

    [TestMethod]
    public void Sum_Products_Correctly()
        {
            //arrange
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            var target = new LinqValueCaculator(mock.Object);
            
            //act
            var result = target.ValueProducts(products);
            
            //assert
            Assert.AreEqual(products.Sum(e => e.Price), result);
        }

    private Product[] createProduct(decimal value)
    {
        return new[] { new Product { Price = value } };
    }

    [TestMethod]
    [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
    public void Pass_Through_Variable_Discount()
    {
        //arrange
        Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
        mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>()))
            .Returns<decimal>(total => total);
        mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0)))
            .Throws<System.ArgumentOutOfRangeException>();
        mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100)))
            .Returns<decimal>(total => (total * 0.9M));
        mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100,
            Range.Inclusive))).Returns<decimal>(total => total - 5);
        var target = new LinqValueCaculator(mock.Object);

        //act
        decimal FiveDollarDiscount = target.ValueProducts(createProduct(5));
        decimal TenDollarDiscount = target.ValueProducts(createProduct(10));
        decimal FiftyDollarDiscount = target.ValueProducts(createProduct(50));
        decimal HundredDollarDiscount = target.ValueProducts(createProduct(100));
        decimal FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));

        //assert
        Assert.AreEqual(5, FiveDollarDiscount, "$5 fail");
        Assert.AreEqual(10, TenDollarDiscount, "$10 fail");
        Assert.AreEqual(50, FiftyDollarDiscount, "$50 fail");
        Assert.AreEqual(500, FiveHundredDollarDiscount, "$500 fail");
        target.ValueProducts(createProduct(0));
    }


    }
}
