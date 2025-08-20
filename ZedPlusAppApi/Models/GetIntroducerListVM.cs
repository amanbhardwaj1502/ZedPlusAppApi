using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetIntroducerListVM
    {
        public long IntroducerID { get; set; }
        public string IntroducerName { get; set; }
        public string IntroducerEmail { get; set; }
        public string IntroducerMobile { get; set; }
        public string IntroducerAddress { get; set; }
        public string FatherName { get; set; }
        public string CustomerCode { get; set; }
        public string SponserName { get; set; }
        public string SponserCode { get; set; }
        public string PraposerName { get; set; }
        public string PraposerCode { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string City { get; set; }
             
    }
}