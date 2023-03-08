using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDongHo.Context;
using WebsiteDongHo.Models;

namespace WebsiteDongHo.Controllers
{    
    public class PaymentController : Controller
    {
        B_ShopEntities objB_ShopEntities = new B_ShopEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var listCart = (List<CartModel>)Session["cart"];

                Order_2119110295 objOder = new Order_2119110295();
                objOder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOder.UserId = int.Parse(Session["idUser"].ToString());
                objOder.CreatedOnUtc = DateTime.Now;
                objOder.Status = 1;
                objB_ShopEntities.Order_2119110295.Add(objOder);

                objB_ShopEntities.SaveChanges();

                int intOrderId = objOder.Id;

                List<OrderDetail_2119110295> listOrderDetail = new List<OrderDetail_2119110295>();

                foreach (var item in listCart)
                {
                    OrderDetail_2119110295 objOrderDetail = new OrderDetail_2119110295();
                    objOrderDetail.Quantity = item.Quantity;
                    objOrderDetail.OrderId = intOrderId;
                    objOrderDetail.ProductId = item.Product.Id;
                    listOrderDetail.Add(objOrderDetail);
                }
                objB_ShopEntities.OrderDetail_2119110295.AddRange(listOrderDetail);
                objB_ShopEntities.SaveChanges();
            }
            return View();
        }
    }
}