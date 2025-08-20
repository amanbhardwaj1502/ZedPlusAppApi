using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class DistrictResponse : JsonResponse
    {
        public List<DistrictVM> DistrictList { get; set; }
    }
}