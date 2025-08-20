using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UpdateAnIntroducerVM
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string FatherName { get; set; }
        public long Country { get; set; }
        public long State { get; set; }
        public long District { get; set; }
        public long City { get; set; }
        public long CustomerId { get; set; }
        public string SponsorID { get; set; }
        public string PraposerID { get; set; }
        public string Status { get; set; }
    }
}