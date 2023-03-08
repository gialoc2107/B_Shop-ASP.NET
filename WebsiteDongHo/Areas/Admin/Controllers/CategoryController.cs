using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDongHo.Context;
using static WebsiteDongHo.Common;

namespace WebsiteDongHo.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        B_ShopEntities objB_ShopEntities = new B_ShopEntities();
        // GET: Admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var listCategory = new List<Category_2119110295>();
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
                listCategory = objB_ShopEntities.Category_2119110295.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                listCategory = objB_ShopEntities.Category_2119110295.ToList();
            }
            ViewBag.CurrentFilter = SearchString;

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            listCategory = listCategory.OrderByDescending(n => n.Id).ToList();
            return View(listCategory.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category_2119110295 category_2119110295)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (category_2119110295.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(category_2119110295.ImageUpload.FileName);
                        string extension = Path.GetExtension(category_2119110295.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        category_2119110295.Avatar = fileName;
                        category_2119110295.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category/"), fileName));
                    }
                    category_2119110295.CreatedOnUtc = DateTime.Now;
                    objB_ShopEntities.Category_2119110295.Add(category_2119110295);
                    objB_ShopEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(category_2119110295);
        }

        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category_2119110295 = objB_ShopEntities.Category_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(category_2119110295);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Category_2119110295 category_2119110295)
        {
            if (category_2119110295.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(category_2119110295.ImageUpload.FileName);
                string extension = Path.GetExtension(category_2119110295.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                category_2119110295.Avatar = fileName;
                category_2119110295.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category/"), fileName));
            }
            objB_ShopEntities.Entry(category_2119110295).State = EntityState.Modified;
            objB_ShopEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var category_2119110295 = objB_ShopEntities.Category_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(category_2119110295);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category_2119110295 = objB_ShopEntities.Category_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(category_2119110295);
        }

        [HttpPost]
        public ActionResult Delete(Category_2119110295 category)
        {
            var category_2119110295 = objB_ShopEntities.Category_2119110295.Where(n => n.Id == category.Id).FirstOrDefault();
            objB_ShopEntities.Category_2119110295.Remove(category_2119110295);
            objB_ShopEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        void LoadData()
        {
            Common objCommon = new Common();

            var listCat = objB_ShopEntities.Category_2119110295.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(listCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            var listBrand = objB_ShopEntities.Brand_2119110295.ToList();
            DataTable dtBrand = converter.ToDataTable(listBrand);

            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");
        }
    }
}