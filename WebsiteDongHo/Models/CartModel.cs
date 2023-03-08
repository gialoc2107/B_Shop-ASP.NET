using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteDongHo.Context;

namespace WebsiteDongHo.Models
{
    public class CartModel
    {
        public Product_2119110295 Product { get; set; }
        public Brand_2119110295 Brand { get; set; }
        public int Quantity { get; set; }
    }
}