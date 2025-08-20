using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetBussinessPlanVM
    {
        public long ID { get; set; }
        public string DocumentUrl { get; set; }
        public string EntryDate { get; set; }
        public string Status { get; set; }
    }
}