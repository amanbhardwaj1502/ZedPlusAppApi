using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class OrderSummaryResponse : JsonResponse
    {
        public List<OrderSummaryVM> OrderList { get; set; }
    }
}