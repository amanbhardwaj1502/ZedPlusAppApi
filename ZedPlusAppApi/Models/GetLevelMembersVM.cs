using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetLevelMembersVM
    {
        public string MemeberName { get; set; }
        public int TotalPV { get; set; }
        public long CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string State { get; set; }
        public long Pincode { get; set; }
        public string Position { get; set; }
        public string City { get; set; }
    }
}