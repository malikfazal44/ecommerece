using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class Category
    {
        public int CatId { get; set; }
        public string? CatName { get; set; }
        public string? CatDesc { get; set; }
        public int? CatStatus { get; set; }
        public string? MetaData { get; set; }
        public string? SeoData { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CatImage { get; set; }
    }
}
