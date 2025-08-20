using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class CategoryListResponse :JsonResponse
    {
        public List<CategoryListVM> CategoryList { get; set; }
    }
}