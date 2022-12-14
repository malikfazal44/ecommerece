using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class OrderLine
    {
        public int OrderLinesId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int SellerId { get; set; }
        public DateTime? ExpectedDelieveryDate { get; set; }
        public string? CouriorName { get; set; }
        public string? TrackingNum { get; set; }
        public int? Status { get; set; }
        public string? MetaData { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
