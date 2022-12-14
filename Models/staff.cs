using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class staff
    {
        public int StaffId { get; set; }
        public string? StaffName { get; set; }
        public int? StaffPhone { get; set; }
        public string? StaffEmail { get; set; }
        public string? City { get; set; }
        public string? StaffAddress { get; set; }
        public DateTime? StaffDob { get; set; }
        public int SystemUserId { get; set; }
        public int? Role { get; set; }
        public int? Status { get; set; }
        public string? MetaData { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? StaffImage { get; set; }
    }
}
