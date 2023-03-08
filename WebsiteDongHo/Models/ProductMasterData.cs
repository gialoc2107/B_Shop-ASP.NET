using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteDongHo.Models
{
    public partial class ProductMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Hình sản phẩm")]
        public string Avatar { get; set; }
        [Display(Name = "Danh mục")]
        public Nullable<int> CategoryId { get; set; }
        [Display(Name = "Mô tả ngắn")]
        public string ShortDescription { get; set; }
        [Display(Name = "Mô tả chi tiết")]
        public string FullDescription { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá gốc")]
        [Display(Name = "Giá gốc")]
        public Nullable<double> Price { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        public Nullable<double> PriceDiscount { get; set; }
        [Display(Name = "Khuyến mãi")]
        public Nullable<int> Discount { get; set; }
        [Display(Name = "Loại sản phẩm")]
        public Nullable<int> TypeId { get; set; }
        [Display(Name = "Đường dẫn")]
        public string Slug { get; set; }
        [Display(Name = "Thương hiệu")]
        public Nullable<int> BrandId { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
        public string Manufacturer { get; set; }
        public Nullable<int> ArticleNumber { get; set; }
        public string Guarantee { get; set; }
        public string DeliveryTime { get; set; }
        public string Availabilty { get; set; }
        public string Trademark { get; set; }
        public string ProductNumber { get; set; }
        public string Origin { get; set; }
        public string Machine { get; set; }
        public string DialThickness { get; set; }
        public string Dial_Diameter { get; set; }
        public string Glasses { get; set; }
        public string Strap { get; set; }
        public Nullable<int> WaterProof { get; set; }
        public string Especially { get; set; }
        public string Avatar1 { get; set; }
        public string Avatar2 { get; set; }
        public string Avatar3 { get; set; }
        public string Avatar4 { get; set; }

    }
}