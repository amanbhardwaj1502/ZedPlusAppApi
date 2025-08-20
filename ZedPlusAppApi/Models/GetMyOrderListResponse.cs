using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetMyOrderListResponse : JsonResponse
    {
        public List<GetMyOrderListVM> GetMyOrderList { get; set; }
    }
}