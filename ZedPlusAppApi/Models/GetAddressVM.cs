using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetAddressVM
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string AddressType { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }

    }
}