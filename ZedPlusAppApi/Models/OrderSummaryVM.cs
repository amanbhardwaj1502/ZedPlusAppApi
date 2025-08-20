using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class OrderSummaryVM
    {
        public string OrderNumber { get; set; }
        public string ProductQuantity { get; set; }
        public string ItemQuantity { get; set; }
        public string TotalPrice { get; set; }
        public string Discount { get; set; }
        public string TotalPV { get; set; }
        public string Date { get; set; }

    }
}