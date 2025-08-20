using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class ProductDetailVM
    {
        public long ID { get; set; }
        public string ProductName { get; set; }
        //public List<ProductImageVM> ImageList { get; set; }
        public string ProductPrice { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductPointValue { get; set; }
        //  public List<ProductSizeVM> SizeList { get; set; } 
        public string ProductBrand { get; set; }
        public string MemberPrice { get; set; }
        public string ProductNRP { get; set; }
        public string ProductDP { get; set; }
        public string ProductUnitName { get; set; }
        public string ProductBasicDiscount { get; set; }
        //  public string ProductFabricType { get; set; } 
        //   public string ProductStyleName { get; set; }
        //  public string ProductSize { get; set; } 
        //   public string ProductColor { get; set; }
        public string MemberDiscountType { get; set; }
        public List<FilterGroupVM> FilterGroups { get; set; }

        public List<ProductVariantsImageVM> ImageList { get; set; }
    }
}