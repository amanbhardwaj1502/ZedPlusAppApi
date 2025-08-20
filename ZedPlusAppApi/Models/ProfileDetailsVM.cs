using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class ProfileDetailsVM
    {

        public Nullable<long> Id { get; set; }
        public string Name { get; set; }
        public string Mobilenumber { get; set; }
        public string EmailID { get; set; }
        public string DateOfBirth { get; set; }
        public string Image { get; set; }
    }
}