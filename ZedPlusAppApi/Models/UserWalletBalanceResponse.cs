using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UserWalletBalanceResponse : JsonResponse
    {
        public double TotalBalance { get; set; }
    }
}