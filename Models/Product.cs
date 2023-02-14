using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int SellerId { get; set; }
        public int? DeliveryDays { get; set; }
        public int CatId { get; set; }
        public int? Quantity { get; set; }
        public string? ShortDecsc { get; set; }
        public string? LongDesc { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? DeliveryCharges { get; set; }
        public int? Status { get; set; }
        public string? MetaData { get; set; }
        public string? SeoData { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ProductImg { get; set; }
        public int NumOfViews { get; set; }
    }
}
