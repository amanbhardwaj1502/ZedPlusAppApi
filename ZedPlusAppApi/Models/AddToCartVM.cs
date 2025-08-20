using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class AddToCartVM
    {
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public long SizeId { get; set; }
        public long VarientId { get; set; }
        public int Quantity { get; set; }
        public string TotalAmount { get; set; }
    }
}