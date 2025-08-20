using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class DocumentListVM
    {
        public long ID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string Status { get; set; }        
    }
}