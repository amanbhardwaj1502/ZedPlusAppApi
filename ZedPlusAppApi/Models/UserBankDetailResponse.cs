using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UserBankDetailResponse : JsonResponse
    {
        public BankDetailsVM BankDetails { get; set; }
    }
}