using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteDongHo.Context;
using static WebsiteDongHo.Common;

namespace WebsiteDongHo.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        B_ShopEntities objB_ShopEntities = new B_ShopEntities();
        // GET: Admin/User
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var listUser = new List<Users_2119110295>();
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
                listUser = objB_ShopEntities.Users_2119110295.Where(n => n.FirstName.Contains(SearchString)).ToList();
            }
            else
            {
                listUser = objB_ShopEntities.Users_2119110295.ToList();
            }
            ViewBag.CurrentFilter = SearchString;

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            listUser = listUser.OrderByDescending(n => n.Id).ToList();
            return View(listUser.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Users_2119110295 users_2119110295)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    var check = objB_ShopEntities.Users_2119110295.FirstOrDefault(s => s.Email == users_2119110295.Email);
                    if (check == null)
                    {
                        users_2119110295.Password = GetMD5(users_2119110295.Password);
                        objB_ShopEntities.Configuration.ValidateOnSaveEnabled = false;
                        objB_ShopEntities.Users_2119110295.Add(users_2119110295);
                        objB_ShopEntities.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Email already exists";
                        return View();
                    }                    
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(users_2119110295);
        }

        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var users_2119110295 = objB_ShopEntities.Users_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(users_2119110295);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(int id, Users_2119110295 users_2119110295)
        {
            users_2119110295.Password = GetMD5(users_2119110295.Password);
            objB_ShopEntities.Configuration.ValidateOnSaveEnabled = false;
            objB_ShopEntities.Entry(users_2119110295).State = EntityState.Modified;
            objB_ShopEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var users_2119110295 = objB_ShopEntities.Users_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(users_2119110295);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var users_2119110295 = objB_ShopEntities.Users_2119110295.Where(n => n.Id == id).FirstOrDefault();
            return View(users_2119110295);
        }

        [HttpPost]
        public ActionResult Delete(Users_2119110295 user)
        {
            var users_2119110295 = objB_ShopEntities.Users_2119110295.Where(n => n.Id == user.Id).FirstOrDefault();
            objB_ShopEntities.Users_2119110295.Remove(users_2119110295);
            objB_ShopEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        void LoadData()
        {
            Common objCommon = new Common();

            var listUser = objB_ShopEntities.Users_2119110295.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtUser = converter.ToDataTable(listUser);
            ViewBag.ListUser = objCommon.ToSelectList(dtUser, "Id", "FirstName");
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}