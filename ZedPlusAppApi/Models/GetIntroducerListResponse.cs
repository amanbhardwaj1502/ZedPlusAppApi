using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetIntroducerListResponse : JsonResponse
    {
        public List<GetIntroducerListVM> GetIntroducerListVM { get; set; }
    }
}