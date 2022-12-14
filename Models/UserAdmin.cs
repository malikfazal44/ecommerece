using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class UserAdmin
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public string? MetaData { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
