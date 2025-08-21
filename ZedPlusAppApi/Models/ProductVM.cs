using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class ProductVM
    {
        public long ID { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        public string ProductSubMenu { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public string ProductMRP { get; set; }
        public string ProductPrice { get; set; }
        public string ProductStock { get; set; }
        public string ProductNRP { get; set; }
        public string ProductDP { get; set; }
        // public string ProductBasicDiscount { get; set; }
        //public string ProductGst { get; set; }
        //public string ProductHsn { get; set; }
        //public string ProductBarcode { get; set; }      
        //public string QuantityPercartoon { get; set; }       
        public string ProductBrand { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }

    }
}