using System;
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
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

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
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

            //assert
            PagingInfo pageInfo = result.PagingInfomation;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
