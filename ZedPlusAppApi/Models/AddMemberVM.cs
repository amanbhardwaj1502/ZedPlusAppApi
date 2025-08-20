using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class AddMemberVM
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string PinCode { get; set; }
        public long Country { get; set; }
        public long State { get; set; }
        public long District { get; set; }
        public long City { get; set; }
        public long  Userid { get; set; }
        public string Address { get; set; }
    }
}