using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebsiteDongHo.Models;

namespace WebsiteDongHo.Context
{
    [MetadataType(typeof(ProductMasterData))]
    public partial class Product_2119110295
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(UserMasterData))]
    public partial class Users_2119110295
    {
    }

    [MetadataType(typeof(CategoryMasterData))]
    public partial class Category_2119110295
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(BrandMasterData))]
    public partial class Brand_2119110295
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(OrderMasterData))]
    public partial class Order_2119110295
    {
        
    }
}