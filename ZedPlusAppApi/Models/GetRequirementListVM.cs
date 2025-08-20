using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetRequirementListVM
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string ReqDescription { get; set; }
        public string Date { get; set; }
        public string EntryDateTime { get; set; }
        public string Status { get; set; }
    }
}