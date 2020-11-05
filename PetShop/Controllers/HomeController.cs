using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetShop.Models;
using PagedList;
using PagedList.Mvc;

namespace PetShop.Controllers
{
    public class HomeController : Controller
    {
        PetShopDataContext db = new PetShopDataContext();

        public ActionResult Index()
        {
            var products = db.Products.Where(x => x.Discount == 0).Take(12).ToList();
            ViewBag.PromotionalProducts = db.Products.Where(x => x.Discount > 0).Take(12).ToList();
            return View(products);
        }

        public ActionResult PromotionProduct(int? page)
        {
            var products = db.Products.Where(x => x.Discount > 0).ToList();
            return View(products.ToPagedList(page ?? 1, 36));
        }

        public ActionResult Search(string searchStr)
        {
            var products = db.Products.Where(x => x.Name.ToLower().Contains(searchStr.ToLower()) || x.Supplier.Name.ToLower().Contains(searchStr.ToLower())).ToList();
            return View(products);
        }


        // product details
        public ActionResult Details(long id)
        {
            var product = db.Products.Single(x => x.Id == id);
            ViewBag.ProductsOfTheSameType = ListByCategoryId(product.CategoryId, 12);
            return View(product);
        }

        public List<Product> ListByCategoryId(long? id, int count)
        {
            var products = db.Products.Where(x => x.CategoryId == id).Take(count).ToList();
            return products;
        }

        // changed into the top 3 menu
        public ActionResult ListByMenuSide(long id)
        {
            var products = ListByCategoryId(id, 8);
            var category = db.Categories.Single(x => x.Id == id);
            ViewBag.CategoryTitle = category.Name;
            return View(products);
        }

        public ActionResult Service()
        {
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult PaymentPolicy()
        {
            return View();
        }

        public ActionResult InfoPayment()
        {
            return View();
        }

        public ActionResult ProductReturn()
        {
            return View();
        }
    }
}