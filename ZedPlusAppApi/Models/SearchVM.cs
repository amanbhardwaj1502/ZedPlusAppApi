using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class SearchVM
    {
        public long ItemId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
    }
}