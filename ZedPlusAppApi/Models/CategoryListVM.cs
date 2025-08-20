using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class CategoryListVM
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }

        //public List<SubCategoryListVM> SubCategoryList { get; set; }
    }
}