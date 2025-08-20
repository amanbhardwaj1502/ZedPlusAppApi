using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedPlusAppApi.Models
{
    public class FilterGroupVM
    {
        public string GroupName { get; set; }
        public List<ProductSizeVM> Values { get; set; }
    }
}
