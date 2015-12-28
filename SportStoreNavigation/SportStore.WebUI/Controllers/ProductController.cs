using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Abstract;
using SportStore.WebUI.Infrastructure;
using SportStore.WebUI.Models;

namespace SportStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(string category,int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel { 
                Products = repository.Products
                .Where(p => p.Category == null || p.Category == category)
                .OrderBy(p=> p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfomation = new PagingInfo
                {
                    CurrentPage = page,
                    ItemPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}
