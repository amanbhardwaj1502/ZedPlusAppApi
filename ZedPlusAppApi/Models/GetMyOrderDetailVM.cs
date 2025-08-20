using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetMyOrderDetailVM
    {
        public long OrderID { get; set; }
        public string OrderNumber { get; set; }
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductPrice { get; set; }
        public string OrderQuantity { get; set; }
        public string OrderDate { get; set; }
        public string MobileNo { get; set; }
        public string OrderStatus { get; set; }
        public long? TotalPrice { get; set; }
        public string ReceiverName { get; set; }
        public string ShippingAddress { get; set; }
        public string ExceptedDeliveryDate { get; set; }
    }
}