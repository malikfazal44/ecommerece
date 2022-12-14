using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class Subscriber
    {
        public int SubscriberId { get; set; }
        public int? Status { get; set; }
        public string? SubscriberName { get; set; }
        public string? SubscriberEmail { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
