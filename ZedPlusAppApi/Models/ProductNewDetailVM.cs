using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZedPlusAppApi.Models
{
    public class ProductNewDetailVM
    {
        public class ProductDetailDTO
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
            public decimal BasePrice { get; set; }
            public string ImageUrl { get; set; }
            public List<ProductVariantDTO> Variants { get; set; }
        }

        public class ProductVariantDTO
        {
            public int VariantID { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
            public string ImageUrl { get; set; }
            public List<VariantAttributeDTO> Attributes { get; set; }
        }

        public class VariantAttributeDTO
        {
            public string AttributeName { get; set; }
            public string AttributeValue { get; set; }
        }
    }
}
