using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UserUplinedetailsVM
    {
        public Nullable<long> Id { get; set; }
        public string Name { get; set; }
        public long? sponsorID { get; set; }    
        public string sponsorCode { get; set; }
        public string sponsorName { get; set; }
    }
}