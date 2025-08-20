using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class ProductResponse: JsonResponse
    {
        public List<ProductVM> ProductList { get; set; }
    }
}