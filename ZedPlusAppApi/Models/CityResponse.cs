using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class CityResponse : JsonResponse
    {
        public List<CityVM> CityList { get; set; }
    }
}