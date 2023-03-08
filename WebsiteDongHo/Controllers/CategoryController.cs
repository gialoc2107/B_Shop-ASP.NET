using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDongHo.Context;
using PagedList;

namespace WebsiteDongHo.Controllers
{
    public class CategoryController : Controller
    {
        B_ShopEntities objB_ShopEntities = new B_ShopEntities();
        // GET: Category
        public ActionResult Index()
        {
            var listCategory = objB_ShopEntities.Category_2119110295.ToList();
            return View(listCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objB_ShopEntities.Product_2119110295.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
        public ActionResult Casio()
        {
            var listProduct = objB_ShopEntities.Product_2119110295.ToList();
            return View(listProduct);
        }
        public ActionResult Citizen()
        {
            var listProduct = objB_ShopEntities.Product_2119110295.ToList();
            return View(listProduct);
        }
    }
}