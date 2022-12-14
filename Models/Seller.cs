using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class Seller
    {
        public int SellerId { get; set; }
        public string? SellerName { get; set; }
        public string? SellerPhone { get; set; }
        public string? SellerEmail { get; set; }
        public string? SellerAddress { get; set; }
        public string? CompanyName { get; set; }
        public string? WebsiteUrl { get; set; }
        public int SellerCnic { get; set; }
        public string? City { get; set; }
        public string? ShortDesc { get; set; }
        public string? LongDesc { get; set; }
        public string? SellerGender { get; set; }
        public DateTime? SellerDob { get; set; }
        public int SystemUserId { get; set; }
        public int? Type { get; set; }
        public int? MaritalStatus { get; set; }
        public int? Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? MetaData { get; set; }
        public string? SeoData { get; set; }
        public string? SellerImg { get; set; }
    }
}
