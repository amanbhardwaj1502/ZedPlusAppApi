using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class SubMenuSliderResponse : JsonResponse
    {
        public List<SubMenuSliderVM> SubMenuSlider { get; set; }
    }
}