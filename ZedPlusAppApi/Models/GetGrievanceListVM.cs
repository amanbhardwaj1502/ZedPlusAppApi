using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetGrievanceListVM
    {
        public long ID { get; set; }
        public string CustomerName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string DocumentPath { get; set; }
        public string EntryDate { get; set; }
        public string Status { get; set; }
  
    }
}