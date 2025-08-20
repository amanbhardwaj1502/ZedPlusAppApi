using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetStoreTypeListVM
    {
        public long ID { get; set; }
        public string StoreType { get; set; }
        public string Status { get; set; }
    }
}