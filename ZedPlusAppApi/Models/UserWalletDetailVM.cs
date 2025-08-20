using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UserWalletDetailVM
    {
        public long Id { get; set; }
        public string TransitionAmount { get; set; }
        public string TransitionTypes { get; set; }
        public string TransitionDate { get; set; }
        public string TransitionStatus { get; set; }
        public string UserName { get; set; }

    }
}