using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebsiteDongHo.Context;
using WebsiteDongHo.Models;

namespace WebsiteDongHo.Controllers
{
    public class HomeController : Controller
    {
        B_ShopEntities objB_ShopEntities = new B_ShopEntities();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objB_ShopEntities.Category_2119110295.ToList();
            objHomeModel.ListProduct = objB_ShopEntities.Product_2119110295.ToList();
            return View(objHomeModel);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objB_ShopEntities.Users_2119110295.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users_2119110295 users_2119110295)
        {
            if (ModelState.IsValid)
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
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        //create a string MD5
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