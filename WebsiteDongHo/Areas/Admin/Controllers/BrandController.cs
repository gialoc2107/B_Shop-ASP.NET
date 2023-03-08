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
    public class BrandController : Controller
    {
        B_ShopEntities objB_ShopEntities = new B_ShopEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var listBrand = new List<Brand_2119110295>();
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
                listBrand = objB_ShopEntities.Brand_2119110295.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                listBrand = objB_ShopEntities.Brand_2119110295.ToList();
            }
            ViewBag.CurrentFilter = SearchString;

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            listBrand = listBrand.OrderByDescending(n => n.Id).ToList();
            return View(listBrand.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand_2119110295 brand_2119110295)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (brand_2119110295.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(brand_2119110295.ImageUpload.FileName);
                        string extension = Path.GetExtension(brand_2119110295.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        brand_2119110295.Avatar = fileName;
                        brand_2119110295.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand/"), fileName));
                    }
                    brand_2119110295.CreatedOnUtc = DateTime.Now;
                    objB_ShopEntities.Brand_2119110295.Add(brand_2119110295);
                    objB_ShopEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(brand_2119110295);
        }
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var brand_2119110295 = objB_ShopEntities.Brand_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(brand_2119110295);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Brand_2119110295 brand_2119110295)
        {
            if (brand_2119110295.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(brand_2119110295.ImageUpload.FileName);
                string extension = Path.GetExtension(brand_2119110295.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                brand_2119110295.Avatar = fileName;
                brand_2119110295.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand/"), fileName));
            }
            objB_ShopEntities.Entry(brand_2119110295).State = EntityState.Modified;
            objB_ShopEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var brand_2119110295 = objB_ShopEntities.Brand_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(brand_2119110295);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var brand_2119110295 = objB_ShopEntities.Brand_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(brand_2119110295);
        }

        [HttpPost]
        public ActionResult Delete(Brand_2119110295 brand)
        {
            var brand_2119110295 = objB_ShopEntities.Brand_2119110295.Where(n => n.Id == brand.Id).FirstOrDefault();
            objB_ShopEntities.Brand_2119110295.Remove(brand_2119110295);
            objB_ShopEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        void LoadData()
        {
            Common objCommon = new Common();

            var listBrand = objB_ShopEntities.Brand_2119110295.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtBrand = converter.ToDataTable(listBrand);
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");
        }
    }
}