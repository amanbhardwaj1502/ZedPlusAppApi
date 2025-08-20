using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetCutomerDetailsVM
    {
        public long CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobileNumber { get; set; }
    }
}