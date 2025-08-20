using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class CartVM
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string ProductSize { get; set; }
        public string ProductImage { get; set; }
        public string ProductColor { get; set; }
        public string ProductPrice { get; set; }
        public string CustomerName { get; set; }
        public string TotalPrice { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string ItemPV { get; set; }
    }
}