using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDongHo.Context;

namespace WebsiteDongHo.Controllers
{
    public class ProductController : Controller
    {
        B_ShopEntities objB_ShopEntities = new B_ShopEntities();
        // GET: Product
        public ActionResult Detail(String Slug)
        {
            var objProduct = objB_ShopEntities.Product_2119110295.Where(n => n.Slug == Slug).FirstOrDefault();
            return View(objProduct);
        }

        public ActionResult Search(string currentFilter, string SearchString, int? page)
        {
            var listProduct = new List<Product_2119110295>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                listProduct = objB_ShopEntities.Product_2119110295.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                listProduct = objB_ShopEntities.Product_2119110295.ToList();
            }
            ViewBag.CurrentFilter = SearchString;

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            listProduct = listProduct.OrderByDescending(n => n.Id).ToList();
            return View(listProduct.ToPagedList(pageNumber, pageSize));
        }
    }
}