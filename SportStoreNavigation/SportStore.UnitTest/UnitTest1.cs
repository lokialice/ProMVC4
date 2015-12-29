﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportStore.WebUI.Models;
using SportStore.WebUI.HTMLHelper;
using System.Web.Mvc;

namespace SportStore.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
            
                new Product {ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"}

            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null,2).Model;

            //assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");

        }

        [TestMethod]
        public void Can_Generate_Page_Link()
        {
            //arrange - define an HTML helper - we need to do this
            //in order to apply the extension method
            HtmlHelper myHelper = null;

            //arrange - create PagingInfo data 
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemPerPage = 10
            };

            //arrange - set up the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //assert
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>" + @"<a class=""selected"" href=""Page2"">2</a>"
                + @"<a href= ""Page3"">3</a>");

        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {

                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"}

            }.AsQueryable());

            //arrange
            ProductController controller = new ProductController(mock.Object);

            //act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null,2).Model;

            //assert
            PagingInfo pageInfo = result.PagingInfomation;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            //arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product { ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product { ProductID = 3, Name = "P3", Category = "Cat3"},
                new Product { ProductID = 4, Name = "P4", Category = "Cat4"},
                new Product { ProductID = 5, Name = "P5", Category = "Cat5"}
            }.AsQueryable());

            //arrange - create a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //action
            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1", Category = "Apples"},
                new Product { ProductID = 2, Name = "P2", Category = "Apples"},
                new Product { ProductID = 3, Name = "P3", Category = "Plums"},
                new Product { ProductID = 4, Name = "P4", Category = "Oranges"},                
            }.AsQueryable());

            //arrange - create the controller
            NavController target = new NavController(mock.Object);

            //act = get the set of categories
            string[] results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1", Category = "Apples"},
                new Product { ProductID = 4, Name = "P2", Category = "Oranges"}
            }.AsQueryable());

            //arrange - create the controller 
            NavController target = new NavController(mock.Object);

            //arrange - define the category to selected
            string categoryToSelect = "Apples";

            //action
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            // arrange
            // - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { 
                new Product { ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product { ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product { ProductID = 3, Name = "P3", Category = "Cat3"},
                new Product { ProductID = 4, Name = "P4", Category = "Cat4"},
                new Product { ProductID = 5, Name = "P5", Category = "Cat5"},
            }.AsQueryable());

            //arrange - create a controller and make the page size 3 items
            ProductController target = new ProductController(mock.Object);
            target.PageSize = 3;

            //action - test the product counts for different categories
            int res1 = ((ProductsListViewModel)target
                        .List("Cat1").Model).PagingInfomation.TotalItems;
            int res2 = ((ProductsListViewModel)target
                        .List("Cat2").Model).PagingInfomation.TotalItems;
            int res3 = ((ProductsListViewModel)target
                        .List("Cat3").Model).PagingInfomation.TotalItems;
            int resAll = ((ProductsListViewModel)target
                        .List(null).Model).PagingInfomation.TotalItems;

            //assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);

        }
    }
}
