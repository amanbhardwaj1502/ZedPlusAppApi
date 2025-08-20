using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class CategorySliderResponse : JsonResponse
    {
        public List<CategorySliderVM> CategorySliderList { get; set; }
    }
}