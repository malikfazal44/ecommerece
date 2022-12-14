using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class Customer
    {
        public int CustId { get; set; }
        public string? CustName { get; set; }
        public string? CustPhone { get; set; }
        public string? CustEmail { get; set; }
        public string? CustAddress { get; set; }
        public string? CustCity { get; set; }
        public DateTime? CustDob { get; set; }
        public int? SystemUserId { get; set; }
        public string? CustGender { get; set; }
        public int? CustStatus { get; set; }
        public string? MetaData { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedDate { get; set; }
        public string? CustImg { get; set; }
    }
}
