using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteDongHo.Models
{
    public partial class OrderMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên đơn hàng")]
        [Display(Name = "Tên đơn hàng")]
        public string Name { get; set; }
        [Display(Name = "Trạng thái")]
        public Nullable<int> Status { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Id người dùng")]
        public Nullable<int> UserId { get; set; }
    }
}