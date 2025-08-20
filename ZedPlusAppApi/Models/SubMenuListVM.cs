using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class SubMenuListVM
    {
        public long SubMenuId { get; set; }
        public long? CategoryId { get; set; }
        public long? SubCategoryId { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuImage { get; set; }
    }
}