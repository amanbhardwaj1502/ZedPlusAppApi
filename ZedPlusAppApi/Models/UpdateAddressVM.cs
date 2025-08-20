using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UpdateAddressVM
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public long DistrictId { get; set; }
        public long StateId { get; set; }
        public long CustomerId { get; set; }
        public string AddressType { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
    }
}