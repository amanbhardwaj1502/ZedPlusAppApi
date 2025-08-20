using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class BankListResponse : JsonResponse
    {
        public List<BankListVM>BankList{get; set;}
    }
}