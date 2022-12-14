﻿using System;
using System.Collections.Generic;

namespace ecommerece.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public string? Address1 { get; set; }
        public string? Remarks { get; set; }
        public string? City { get; set; }
        public int? Status { get; set; }
        public int CustomerId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
