using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetAadhaarDetailVM
    {
        public Nullable<long> Id { get; set; }
        public String CustomersName { get; set; }
        public string AadharNumder { get; set; }
        public string AadharFrontImage { get; set; }
        public string AadharBankImage { get; set; }
        public string Status { get; set; }
        public string DateTime { get; set; }

    }
}