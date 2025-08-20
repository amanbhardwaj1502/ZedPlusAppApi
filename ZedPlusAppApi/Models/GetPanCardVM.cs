using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetPanCardVM
    {
        public Nullable<long> Id { get; set; }
        public String CustomerName { get; set; }
        public string PanCardNumber { get; set; }
        public string PanCardImage { get; set; }
        public string Status { get; set; }
        public string DateTime { get; set; }
    }
}