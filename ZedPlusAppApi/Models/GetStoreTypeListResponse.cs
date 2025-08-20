using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetStoreTypeListResponse : JsonResponse
    {
        public List<GetStoreTypeListVM> GetStoreTypeList { get; set; }
    }
}