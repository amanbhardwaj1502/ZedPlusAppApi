using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetLegalDocumentListVM
    {
        public long ID { get; set; }
        public string DocumentName { get; set; }
        public string EntryDate { get; set; }
        public string DocumentURL { get; set; }
        public string Status { get; set; }
    }
}