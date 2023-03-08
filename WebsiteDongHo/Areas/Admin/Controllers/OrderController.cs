using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDongHo.Context;
using static WebsiteDongHo.Common;

namespace WebsiteDongHo.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        B_ShopEntities objB_ShopEntities = new B_ShopEntities();
        // GET: Admin/Order
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var listOrder = new List<Order_2119110295>();
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
                listOrder = objB_ShopEntities.Order_2119110295.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                listOrder = objB_ShopEntities.Order_2119110295.ToList();
            }
            ViewBag.CurrentFilter = SearchString;

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            listOrder = listOrder.OrderByDescending(n => n.Id).ToList();
            return View(listOrder.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Order_2119110295 order_2119110295)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    order_2119110295.CreatedOnUtc = DateTime.Now;
                    objB_ShopEntities.Order_2119110295.Add(order_2119110295);
                    objB_ShopEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(order_2119110295);
        }

        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var order_2119110295 = objB_ShopEntities.Order_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(order_2119110295);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Order_2119110295 order_2119110295)
        {
            objB_ShopEntities.Entry(order_2119110295).State = EntityState.Modified;
            objB_ShopEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var order_2119110295 = objB_ShopEntities.Order_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(order_2119110295);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var order_2119110295 = objB_ShopEntities.Order_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(order_2119110295);
        }

        [HttpPost]
        public ActionResult Delete(Order_2119110295 order)
        {
            var order_2119110295 = objB_ShopEntities.Order_2119110295.Where(n => n.Id == order.Id).FirstOrDefault();
            objB_ShopEntities.Order_2119110295.Remove(order_2119110295);
            objB_ShopEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        void LoadData()
        {
            Common objCommon = new Common();

            var listOrder = objB_ShopEntities.Order_2119110295.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtOrder = converter.ToDataTable(listOrder);
            ViewBag.ListOrder = objCommon.ToSelectList(dtOrder, "Id", "Name");
        }
    }
}