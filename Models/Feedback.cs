using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public int CustId { get; set; }
        public int OrderId { get; set; }
        public string? Feedback1 { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
