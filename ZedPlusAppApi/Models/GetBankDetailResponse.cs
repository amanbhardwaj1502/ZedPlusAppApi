using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetBankDetailResponse : JsonResponse
    {
        public List<GetBankDetailVM> BankList{ get; set; }
    }
}