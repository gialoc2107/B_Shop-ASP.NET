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
    public class ProductController : Controller
    {
        B_ShopEntities objB_ShopEntities = new B_ShopEntities();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
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

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product_2119110295 product_2119110295)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (product_2119110295.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(product_2119110295.ImageUpload.FileName);
                        string extension = Path.GetExtension(product_2119110295.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        product_2119110295.Avatar = fileName;
                        product_2119110295.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    product_2119110295.CreatedOnUtc = DateTime.Now;
                    objB_ShopEntities.Product_2119110295.Add(product_2119110295);
                    objB_ShopEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product_2119110295);
        }

        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var product_2119110295 = objB_ShopEntities.Product_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(product_2119110295);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Product_2119110295 product_2119110295)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (product_2119110295.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(product_2119110295.ImageUpload.FileName);
                        string extension = Path.GetExtension(product_2119110295.ImageUpload.FileName);
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        product_2119110295.Avatar = fileName;
                        product_2119110295.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objB_ShopEntities.Entry(product_2119110295).State = EntityState.Modified;
                    objB_ShopEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product_2119110295);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            this.LoadData();
            var product_2119110295 = objB_ShopEntities.Product_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(product_2119110295);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product_2119110295 = objB_ShopEntities.Product_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(product_2119110295);
        }

        [HttpPost]
        public ActionResult Delete(Product_2119110295 product)
        {
            var product_2119110295 = objB_ShopEntities.Product_2119110295.Where(n => n.Id == product.Id).FirstOrDefault();
            objB_ShopEntities.Product_2119110295.Remove(product_2119110295);
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

            List<ProductType> listProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            listProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Casio";
            listProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 03;
            objProductType.Name = "Citizen";
            listProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 04;
            objProductType.Name = "Đề xuất";
            listProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(listProductType);

            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }
    }
}