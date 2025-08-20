using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class CartResponse : JsonResponse
    {
        public List<CartVM> CartList { get; set; }
    }
}