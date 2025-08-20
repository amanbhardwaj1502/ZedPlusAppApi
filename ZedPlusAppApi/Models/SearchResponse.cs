using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class SearchResponse : JsonResponse
    {
        public List<SearchVM> SearchList { get; set; }
    }
}