using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class UserLoginVM
    {
        public long Id { get; set; }
        public string CustomerCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }
        public string Position { get; set; }
        public string Date { get; set; }

    }
}