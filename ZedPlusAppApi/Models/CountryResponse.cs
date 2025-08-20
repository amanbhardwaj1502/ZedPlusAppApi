using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class CountryResponse : JsonResponse
    {
        public List<CountryVM> CountryList { get; set; }
    }
}