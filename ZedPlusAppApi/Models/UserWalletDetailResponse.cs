using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UserWalletDetailResponse : JsonResponse
    {
        public List<UserWalletDetailVM> WalletList { get; set; }
    }
}