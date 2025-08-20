using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class ProductController : ApiController
    {

        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/GetProductList")]
        //public ProductResponse GetProductList(int SubMenuId,int SubCategoryId,int CategoryId)
        //{
        //    ProductResponse resp = new ProductResponse();
        //    List<ProductVM> mdl = new List<ProductVM>();
        //    try
        //    {
        //        db_ZedPlusShopEntities db = new db_ZedPlusShopEntities();
        //        if (SubMenuId == 0 && SubCategoryId == 0 && CategoryId==0)
        //        {
        //            var result = (from tbl in db.tblItemMasters
        //                          join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
        //                          from tbla in a.DefaultIfEmpty()
        //                          select new
        //                          {
        //                              tbl.ItemID,
        //                              tbl.Item_Color,
        //                              tbl.Item_Cost,
        //                              tbl.Item_Detail,
        //                              tbl.Item_Discount,
        //                              tbl.Item_Image,
        //                              tbl.Item_Name,
        //                              tbl.Item_PointValue,
        //                              tbl.Item_Price,
        //                              tbl.Item_Qty,
        //                              tbl.Item_Status,
        //                              tbl.Item_Sizes,
        //                              tbl.SubCategoryID,
        //                              tbl.SubMenuID,
        //                              tbl.CategoryID,
        //                              tbla.Brand_Name,
        //                              tbl.Date,

        //                          }).ToList();
        //            if (result.Count() > 0)
        //            {
        //                foreach (var list in result)
        //                {
        //                    int sizes = 0;
        //                    if (list.Item_Sizes != null)
        //                    {
        //                        if (list.Item_Sizes.Contains(','))
        //                        {
        //                            sizes = Convert.ToInt32(list.Item_Sizes.Split(',')[0]);
        //                        }
        //                        else { sizes = Convert.ToInt32(list.Item_Sizes); }
        //                    }
        //                    string size = "";
        //                    try
        //                    {
        //                        if (list.Item_Sizes != null)
        //                        {
        //                            size = db.tblSizeMasters.Where(x => x.ID == sizes).FirstOrDefault().Size_Name;
        //                        }
        //                    }
        //                    catch { }
        //                    int colors = 0;
        //                    if (list.Item_Color != null)
        //                    {
        //                        if (list.Item_Color.Contains(','))
        //                        {
        //                            colors = Convert.ToInt32(list.Item_Color.Split(',')[0]);
        //                        }
        //                        else { colors = Convert.ToInt32(list.Item_Color); }
        //                    }
        //                    string color = "";
        //                    try
        //                    {
        //                        if (list.Item_Color != null)
        //                        {
        //                            color = db.tblColorMasters.Where(x => x.ID == colors).FirstOrDefault().Color_Name;
        //                        }
        //                    }
        //                    catch { }
        //                    mdl.Add(new ProductVM
        //                    {
        //                        ID = list.ItemID,
        //                        ProductBrand = list.Brand_Name,
        //                        ProductColor = color,
        //                        ProductSize = size,
        //                        ProductPrice = list.Item_Price,
        //                        ProductCost = list.Item_Cost,
        //                        ProductDetail = list.Item_Detail,
        //                        ProductImage = list.Item_Image.Split(',')[0],
        //                        ProductPointValue = list.Item_PointValue,
        //                        ProductName = list.Item_Name,
        //                        ProductQuantity = list.Item_Qty,
        //                    });
        //                }
        //                resp = new ProductResponse { ProductList = mdl };
        //                return resp;
        //            }
        //            else
        //            {
        //                resp = new ProductResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
        //            }
        //        }
        //        else if (SubMenuId != 0 && SubCategoryId == 0 && CategoryId == 0)
        //        {
        //            var result = (from tbl in db.tblItemMasters
        //                          join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
        //                          from tbla in a.DefaultIfEmpty()
        //                          where tbl.SubMenuID == SubMenuId
        //                          select new
        //                          {
        //                              tbl.ItemID,
        //                              tbl.Item_Color,
        //                              tbl.Item_Cost,
        //                              tbl.Item_Detail,
        //                              tbl.Item_Discount,
        //                              tbl.Item_Image,
        //                              tbl.Item_Name,
        //                              tbl.Item_PointValue,
        //                              tbl.Item_Price,
        //                              tbl.Item_Qty,
        //                              tbl.Item_Status,
        //                              tbl.Item_Sizes,
        //                              tbl.SubCategoryID,
        //                              tbl.SubMenuID,
        //                              tbl.CategoryID,
        //                              tbla.Brand_Name,
        //                              tbl.Date,

        //                          }).ToList();
        //            if (result.Count() > 0)
        //            {
        //                foreach (var list in result)
        //                {
        //                    int sizes = 0;
        //                    if (list.Item_Sizes != null)
        //                    {
        //                        if (list.Item_Sizes.Contains(','))
        //                        {
        //                            sizes = Convert.ToInt32(list.Item_Sizes.Split(',')[0]);
        //                        }
        //                        else { sizes = Convert.ToInt32(list.Item_Sizes); }
        //                    }
        //                    string size = "";
        //                    try
        //                    {
        //                        if (list.Item_Sizes != null)
        //                        {
        //                            size = db.tblSizeMasters.Where(x => x.ID == sizes).FirstOrDefault().Size_Name;
        //                        }
        //                    }
        //                    catch { }
        //                    int colors = 0;
        //                    if (list.Item_Color != null)
        //                    {
        //                        if (list.Item_Color.Contains(','))
        //                        {
        //                            colors = Convert.ToInt32(list.Item_Color.Split(',')[0]);
        //                        }
        //                        else { colors = Convert.ToInt32(list.Item_Color); }
        //                    }
        //                    string color = "";
        //                    try
        //                    {
        //                        if (list.Item_Color != null)
        //                        {
        //                            color = db.tblColorMasters.Where(x => x.ID == colors).FirstOrDefault().Color_Name;
        //                        }
        //                    }
        //                    catch { }
        //                    mdl.Add(new ProductVM
        //                    {
        //                        ID = list.ItemID,
        //                        ProductBrand = list.Brand_Name,
        //                        ProductColor = color,
        //                        ProductSize = size,
        //                        ProductPrice = list.Item_Price,
        //                        ProductCost = list.Item_Cost,
        //                        ProductDetail = list.Item_Detail,
        //                        ProductImage = list.Item_Image.Split(',')[0],
        //                        ProductPointValue = list.Item_PointValue,
        //                        ProductName = list.Item_Name,
        //                        ProductQuantity = list.Item_Qty,
        //                    });
        //                }
        //                resp = new ProductResponse { ProductList = mdl };
        //                return resp;
        //            }
        //            else
        //            {
        //                resp = new ProductResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
        //            }
        //        }
        //        else if (SubMenuId == 0 && SubCategoryId != 0 && CategoryId == 0)
        //        {
        //            var result = (from tbl in db.tblItemMasters
        //                          join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
        //                          from tbla in a.DefaultIfEmpty()
        //                          where tbl.SubCategoryID == SubCategoryId
        //                          select new
        //                          {
        //                              tbl.ItemID,
        //                              tbl.Item_Color,
        //                              tbl.Item_Cost,
        //                              tbl.Item_Detail,
        //                              tbl.Item_Discount,
        //                              tbl.Item_Image,
        //                              tbl.Item_Name,
        //                              tbl.Item_PointValue,
        //                              tbl.Item_Price,
        //                              tbl.Item_Qty,
        //                              tbl.Item_Status,
        //                              tbl.Item_Sizes,
        //                              tbl.SubCategoryID,
        //                              tbl.SubMenuID,
        //                              tbl.CategoryID,
        //                              tbla.Brand_Name,
        //                              tbl.Date,

        //                          }).ToList();
        //            if (result.Count() > 0)
        //            {
        //                foreach (var list in result)
        //                {
        //                    int sizes = 0;
        //                    if (list.Item_Sizes != null)
        //                    {
        //                        if (list.Item_Sizes.Contains(','))
        //                        {
        //                            sizes = Convert.ToInt32(list.Item_Sizes.Split(',')[0]);
        //                        }
        //                        else { sizes = Convert.ToInt32(list.Item_Sizes); }
        //                    }
        //                    string size = "";
        //                    try
        //                    {
        //                        if (list.Item_Sizes != null)
        //                        {
        //                            size = db.tblSizeMasters.Where(x => x.ID == sizes).FirstOrDefault().Size_Name;
        //                        }
        //                    }
        //                    catch { }
        //                    int colors = 0;
        //                    if (list.Item_Color != null)
        //                    {
        //                        if (list.Item_Color.Contains(','))
        //                        {
        //                            colors = Convert.ToInt32(list.Item_Color.Split(',')[0]);
        //                        }
        //                        else { colors = Convert.ToInt32(list.Item_Color); }
        //                    }
        //                    string color = "";
        //                    try
        //                    {
        //                        if (list.Item_Color != null)
        //                        {
        //                            color = db.tblColorMasters.Where(x => x.ID == colors).FirstOrDefault().Color_Name;
        //                        }
        //                    }
        //                    catch { }
        //                    mdl.Add(new ProductVM
        //                    {
        //                        ID = list.ItemID,
        //                        ProductBrand = list.Brand_Name,
        //                        ProductColor = color,
        //                        ProductSize = size,
        //                        ProductPrice = list.Item_Price,
        //                        ProductCost = list.Item_Cost,
        //                        ProductDetail = list.Item_Detail,
        //                        ProductImage = list.Item_Image.Split(',')[0],
        //                        ProductPointValue = list.Item_PointValue,
        //                        ProductName = list.Item_Name,
        //                        ProductQuantity = list.Item_Qty,
        //                    });
        //                }
        //                resp = new ProductResponse { ProductList = mdl };
        //                return resp;
        //            }
        //            else
        //            {
        //                resp = new ProductResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
        //            }
        //        }
        //        else if (SubMenuId == 0 && SubCategoryId == 0 && CategoryId != 0)
        //        {
        //            var result = (from tbl in db.tblItemMasters
        //                          join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
        //                          from tbla in a.DefaultIfEmpty()
        //                          where tbl.CategoryID == CategoryId
        //                          select new
        //                          {
        //                              tbl.ItemID,
        //                              tbl.Item_Color,
        //                              tbl.Item_Cost,
        //                              tbl.Item_Detail,
        //                              tbl.Item_Discount,
        //                              tbl.Item_Image,
        //                              tbl.Item_Name,
        //                              tbl.Item_PointValue,
        //                              tbl.Item_Price,
        //                              tbl.Item_Qty,
        //                              tbl.Item_Status,
        //                              tbl.Item_Sizes,
        //                              tbl.SubCategoryID,
        //                              tbl.SubMenuID,
        //                              tbl.CategoryID,
        //                              tbla.Brand_Name,
        //                              tbl.Date,

        //                          }).ToList();
        //            if (result.Count() > 0)
        //            {
        //                foreach (var list in result)
        //                {
        //                    int sizes = 0;
        //                    if (list.Item_Sizes != null)
        //                    {
        //                        if (list.Item_Sizes.Contains(','))
        //                        {
        //                            sizes = Convert.ToInt32(list.Item_Sizes.Split(',')[0]);
        //                        }
        //                        else { sizes = Convert.ToInt32(list.Item_Sizes); }
        //                    }
        //                    string size = "";
        //                    try
        //                    {
        //                        if (list.Item_Sizes != null)
        //                        {
        //                            size = db.tblSizeMasters.Where(x => x.ID == sizes).FirstOrDefault().Size_Name;
        //                        }
        //                    }
        //                    catch { }
        //                    int colors = 0;
        //                    if (list.Item_Color != null)
        //                    {
        //                        if (list.Item_Color.Contains(','))
        //                        {
        //                            colors = Convert.ToInt32(list.Item_Color.Split(',')[0]);
        //                        }
        //                        else { colors = Convert.ToInt32(list.Item_Color); }
        //                    }
        //                    string color = "";
        //                    try
        //                    {
        //                        if (list.Item_Color != null)
        //                        {
        //                            color = db.tblColorMasters.Where(x => x.ID == colors).FirstOrDefault().Color_Name;
        //                        }
        //                    }
        //                    catch { }
        //                    mdl.Add(new ProductVM
        //                    {
        //                        ID = list.ItemID,
        //                        ProductBrand = list.Brand_Name,
        //                        ProductColor = color,
        //                        ProductSize = size,
        //                        ProductPrice = list.Item_Price,
        //                        ProductCost = list.Item_Cost,
        //                        ProductDetail = list.Item_Detail,
        //                        ProductImage = list.Item_Image.Split(',')[0],
        //                        ProductPointValue = list.Item_PointValue,
        //                        ProductName = list.Item_Name,
        //                        ProductQuantity = list.Item_Qty,
        //                    });
        //                }
        //                resp = new ProductResponse { ProductList = mdl };
        //                return resp;
        //            }
        //            else
        //            {
        //                resp = new ProductResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = new ProductResponse { Status_Code = "0", Status = "error", Message = ex.Message };
        //    }
        //    return resp;
        //}




        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetProductList")]
        public ProductResponse GetProductList(int SubMenuId, int SubCategoryId, int CategoryId)
        {
            ProductResponse resp = new ProductResponse();
            List<ProductVM> mdl = new List<ProductVM>();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                if (SubMenuId == 0 && SubCategoryId == 0 && CategoryId == 0)
                {
                    List<ProductVM> productList = new List<ProductVM>();
                    var result = (from tbl in db.tblProducts
                                  join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
                                  from tbla in a.DefaultIfEmpty()
                                  select new
                                  {
                                      tbl.ID,
                                      tbl.Item_Name,
                                      tbl.MRP,
                                      tbl.NRP,
                                      tbl.DP,
                                      tbl.BasicDiscount,
                                      Brand_Name = tbla.Brand_Name,
                                      tbl.Date,
                                      tbl.PV,
                                      tbl.Stock
                                  }).ToList();

                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            string firstImagePath = "";
                            var primaryVariant = db.ProductVariants
                                                   .FirstOrDefault(x => x.ProductID == list.ID && x.IsPrimary == true);

                            if (primaryVariant != null)
                            {
                                var variantImage = db.tblVaraintsimages
                                                     .FirstOrDefault(x => x.VariantID == primaryVariant.VariantID);

                                if (variantImage != null && !string.IsNullOrEmpty(variantImage.ImageUrl))
                                {
                                    firstImagePath = variantImage.ImageUrl.Split(',')[0];
                                }
                            }

                            productList.Add(new ProductVM
                            {
                                ID = list.ID,
                                ProductName = list.Item_Name,
                                ProductPrice = list.MRP.ToString(),
                                ProductNRP = list.NRP.ToString(),
                                ProductDP = list.DP.ToString(),
                                ProductBasicDiscount = list.BasicDiscount.ToString(),
                                ProductImage = firstImagePath,
                                ProductBrand = list.Brand_Name,
                                ProductPointValue = list.PV,
                                ProductQuantity = list.Stock.ToString()
                            });
                        }

                        resp = new ProductResponse { Status_Code = "200", Status = "success", ProductList = productList };
                    }
                    else
                    {
                        resp = new ProductResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                    }
                }
                else if (SubMenuId != 0 && SubCategoryId == 0 && CategoryId == 0)
                {
                    var result = (from tbl in db.tblProducts
                                  join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
                                  from tbla in a.DefaultIfEmpty()
                                  join tble in db.tblUnitMasters on tbl.UnitID equals tble.ID into e
                                  from tble in e.DefaultIfEmpty()
                                  where tbl.SubMenuID == SubMenuId
                                  select new
                                  {
                                      tbl.ID,
                                      tbl.Item_Name,
                                      tble.Unit_name,
                                      tbl.MRP,
                                      tbl.NRP,
                                      tbl.DP,
                                      tbl.BasicDiscount,
                                      tbla.Brand_Name,
                                      tbl.Date,
                                      tbl.PV,
                                      tbl.Stock
                                  }).ToList();
                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            string firstImagePath = "";
                            var primaryVariant = db.ProductVariants
                                                   .FirstOrDefault(x => x.ProductID == list.ID && x.IsPrimary == true);

                            if (primaryVariant != null)
                            {
                                var variantImage = db.tblVaraintsimages
                                                     .FirstOrDefault(x => x.VariantID == primaryVariant.VariantID);

                                if (variantImage != null && !string.IsNullOrEmpty(variantImage.ImageUrl))
                                {
                                    firstImagePath = variantImage.ImageUrl.Split(',')[0];
                                }
                            }
                            mdl.Add(new ProductVM
                            {
                                ID = list.ID,
                                ProductName = list.Item_Name,
                                ProductUnitName = list.Unit_name,
                                ProductPrice = list.MRP.ToString(),
                                ProductNRP = list.NRP.ToString(),
                                ProductDP = list.DP.ToString(),
                                ProductBasicDiscount = list.BasicDiscount.ToString(),
                                ProductImage = firstImagePath,
                                ProductBrand = list.Brand_Name,
                                ProductPointValue = list.PV,
                                ProductQuantity = list.Stock.ToString(),
                            });
                        }
                        resp = new ProductResponse { ProductList = mdl };
                        return resp;
                    }
                    else
                    {
                        resp = new ProductResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                    }
                }
                else if (SubMenuId == 0 && SubCategoryId != 0 && CategoryId == 0)
                {
                    var result = (from tbl in db.tblProducts
                                  join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
                                  from tbla in a.DefaultIfEmpty()
                                  join tble in db.tblUnitMasters on tbl.UnitID equals tble.ID into e
                                  from tble in e.DefaultIfEmpty()
                                  where tbl.SubcategoryID == SubCategoryId
                                  select new
                                  {
                                      tbl.ID,
                                      tbl.Item_Name,
                                      tble.Unit_name,
                                      tbl.MRP,
                                      tbl.NRP,
                                      tbl.DP,
                                      tbl.BasicDiscount,
                                      tbla.Brand_Name,
                                      tbl.Date,
                                      tbl.PV,
                                      tbl.Stock
                                  }).ToList();
                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            string firstImagePath = "";
                            var primaryVariant = db.ProductVariants
                                                   .FirstOrDefault(x => x.ProductID == list.ID && x.IsPrimary == true);

                            if (primaryVariant != null)
                            {
                                var variantImage = db.tblVaraintsimages
                                                     .FirstOrDefault(x => x.VariantID == primaryVariant.VariantID);

                                if (variantImage != null && !string.IsNullOrEmpty(variantImage.ImageUrl))
                                {
                                    firstImagePath = variantImage.ImageUrl.Split(',')[0];
                                }
                            }
                            mdl.Add(new ProductVM
                            {
                                ID = list.ID,
                                ProductName = list.Item_Name,
                                ProductUnitName = list.Unit_name,
                                ProductPrice = list.MRP.ToString(),
                                ProductNRP = list.NRP.ToString(),
                                ProductDP = list.DP.ToString(),
                                ProductBasicDiscount = list.BasicDiscount.ToString(),
                                ProductImage = firstImagePath,
                                ProductPointValue = list.PV,
                                ProductQuantity = list.Stock.ToString(),
                            });
                        }
                        resp = new ProductResponse { ProductList = mdl };
                        return resp;
                    }
                    else
                    {
                        resp = new ProductResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                    }
                }
                else if (SubMenuId == 0 && SubCategoryId == 0 && CategoryId != 0)
                {
                    var result = (from tbl in db.tblProducts
                                  join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
                                  from tbla in a.DefaultIfEmpty()
                                  join tble in db.tblUnitMasters on tbl.UnitID equals tble.ID into e
                                  from tble in e.DefaultIfEmpty()
                                  where tbl.CategoryID == CategoryId
                                  select new
                                  {
                                      tbl.ID,
                                      tbl.Item_Name,
                                      tble.Unit_name,
                                      tbl.MRP,
                                      tbl.NRP,
                                      tbl.DP,
                                      tbl.BasicDiscount,
                                      tbla.Brand_Name,
                                      tbl.Date,
                                      tbl.PV,
                                      tbl.Stock
                                  }).ToList();
                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            string firstImagePath = "";
                            var primaryVariant = db.ProductVariants
                                                   .FirstOrDefault(x => x.ProductID == list.ID && x.IsPrimary == true);

                            if (primaryVariant != null)
                            {
                                var variantImage = db.tblVaraintsimages
                                                     .FirstOrDefault(x => x.VariantID == primaryVariant.VariantID);

                                if (variantImage != null && !string.IsNullOrEmpty(variantImage.ImageUrl))
                                {
                                    firstImagePath = variantImage.ImageUrl.Split(',')[0];
                                }
                            }
                            mdl.Add(new ProductVM
                            {
                                ID = list.ID,
                                ProductName = list.Item_Name,
                                ProductUnitName = list.Unit_name,
                                ProductPrice = list.MRP.ToString(),
                                ProductNRP = list.NRP.ToString(),
                                ProductDP = list.DP.ToString(),
                                ProductBasicDiscount = list.BasicDiscount.ToString(),
                                ProductImage = firstImagePath,
                                ProductBrand = list.Brand_Name,
                                ProductPointValue = list.PV,
                                ProductQuantity = list.Stock.ToString(),

                            });
                        }
                        resp = new ProductResponse { ProductList = mdl };
                        return resp;
                    }
                    else
                    {
                        resp = new ProductResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                    }
                }
            }
            catch (Exception ex)
            {
                resp = new ProductResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }





        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/GetProductDetail")]
        //public ProductDetailResponse GetProductDetail(int ProductId)
        //{
        //    ProductDetailResponse resp = new ProductDetailResponse();
        //    List<ProductDetailVM> mdl = new List<ProductDetailVM>();
        //    try
        //    {
        //        db_ZedPlusShopEntities db = new db_ZedPlusShopEntities();

        //        var result = (from tbl in db.tblItemMasters
        //                      join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
        //                      from tbla in a.DefaultIfEmpty()
        //                      where tbl.ItemID == ProductId
        //                      select new
        //                      {
        //                          tbl.ItemID,
        //                          tbl.Item_Color,
        //                          tbl.Item_Cost,
        //                          tbl.Item_Detail,
        //                          tbl.Item_Discount,
        //                          tbl.Item_Image,
        //                          tbl.Item_Name,
        //                          tbl.Item_PointValue,
        //                          tbl.Item_Price,
        //                          tbl.Item_Qty,
        //                          tbl.Item_Status,
        //                          tbl.Item_Sizes,
        //                          tbl.SubCategoryID,
        //                          tbl.SubMenuID,
        //                          tbl.CategoryID,
        //                          tbla.Brand_Name,
        //                          tbl.Date,
        //                          tbl.MemberPrice
        //                      }).ToList();
        //        if (result.Count() > 0)
        //        {
        //            foreach (var list in result)
        //            {

        //                List<ProductSizeVM> mdl2 = new List<ProductSizeVM>();
        //                if (list.Item_Sizes != null)
        //                {
        //                    if (list.Item_Sizes.Contains(','))
        //                    {
        //                        foreach (var list1 in list.Item_Sizes.Split(',').ToList())
        //                        {
        //                            int size = 0;
        //                            try
        //                            {
        //                                size = Convert.ToInt32(list1);
        //                            }
        //                            catch { }
        //                            try
        //                            {
        //                                var res1 = from tb in db.tblSizeMasters
        //                                           where tb.ID == size
        //                                           select tb;
        //                                if (res1.Count() > 0)
        //                                {
        //                                    var sizes = db.tblSizeMasters.Where(x => x.ID == size).FirstOrDefault();
        //                                    mdl2.Add(new ProductSizeVM
        //                                    {
        //                                        SizeName = sizes.Size_Name,
        //                                        SizeId = sizes.ID
        //                                    });
        //                                }
        //                            }
        //                            catch { }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        int size = 0;
        //                        try
        //                        {
        //                            size = Convert.ToInt32(list.Item_Sizes);
        //                        }
        //                        catch { }
        //                        try
        //                        {
        //                            var res1 = from tb in db.tblSizeMasters
        //                                       where tb.ID == size
        //                                       select tb;
        //                            if (res1.Count() > 0)
        //                            {
        //                                var sizes = db.tblSizeMasters.Where(x => x.ID == size).FirstOrDefault();
        //                                mdl2.Add(new ProductSizeVM
        //                                {
        //                                    SizeName = sizes.Size_Name,
        //                                    SizeId = sizes.ID

        //                                });
        //                            }
        //                        }
        //                        catch { }
        //                    }
        //                }


        //                List<ProductImageVM> mdl4 = new List<ProductImageVM>();
        //                if (list.Item_Image != null)
        //                {
        //                    if (list.Item_Image.Contains(','))
        //                    {
        //                        foreach (var list3 in list.Item_Image.Split(',').ToList())
        //                        {

        //                            mdl4.Add(new ProductImageVM
        //                            {
        //                                ImagePath = list3,

        //                            });
        //                        }
        //                    }
        //                    else
        //                    {

        //                        mdl4.Add(new ProductImageVM
        //                        {
        //                            ImagePath = list.Item_Image,

        //                        });
        //                    }
        //                }
        //                int colors = 0;
        //                if (list.Item_Color != null)
        //                {
        //                    if (list.Item_Color.Contains(','))
        //                    {
        //                        colors = Convert.ToInt32(list.Item_Color.Split(',')[0]);
        //                    }
        //                    else { colors = Convert.ToInt32(list.Item_Color); }
        //                }
        //                string color = "";
        //                try
        //                {
        //                    if (list.Item_Color != null)
        //                    {
        //                        color = db.tblColorMasters.Where(x => x.ID == colors).FirstOrDefault().Color_Name;
        //                    }
        //                }
        //                catch { }
        //                mdl.Add(new ProductDetailVM
        //                {
        //                    ID = list.ItemID,
        //                    ProductBrand = list.Brand_Name,
        //                    Color = color,
        //                    SizeList = mdl2,
        //                    ProductPrice = list.Item_Price,
        //                    ProductCost = list.Item_Cost,
        //                    ProductDetail = list.Item_Detail,
        //                    ImageList = mdl4,
        //                    ProductPointValue = list.Item_PointValue,
        //                    ProductName = list.Item_Name,
        //                    ProductQuantity = list.Item_Qty,
        //                    MemberPrice = Convert.ToString(list.MemberPrice),
        //                });
        //            }
        //            resp = new ProductDetailResponse { ProductList = mdl };
        //            return resp;
        //        }
        //        else
        //        {
        //            resp = new ProductDetailResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = new ProductDetailResponse { Status_Code = "0", Status = "error", Message = ex.Message };
        //    }
        //    return resp;
        //}


        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/GetProductDetail")]
        //public ProductDetailResponse GetProductDetail(int ProductId)
        //{
        //    ProductDetailResponse resp = new ProductDetailResponse();
        //    List<ProductDetailVM> mdl = new List<ProductDetailVM>();
        //    try
        //    {
        //        db_zedPlusShopEntities db = new db_zedPlusShopEntities();

        //        var result = (from tbl in db.tblProducts
        //                      join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
        //                      from tbla in a.DefaultIfEmpty()
        //                      join tble in db.tblUnitMasters on tbl.UnitID equals tble.ID into e
        //                      from tble in e.DefaultIfEmpty()
        //                      where tbl.ID == ProductId
        //                      select new
        //                      {
        //                          tbl.ID,
        //                          tbl.Item_Name,
        //                          tble.Unit_name,
        //                          tbl.MRP,
        //                          tbl.NRP,
        //                          tbl.BasicDiscount,
        //                          tbla.Brand_Name,
        //                          tbl.PV,
        //                          tbl.Date,
        //                          tbl.SubMenuID,
        //                          tbl.Stock,
        //                      }).ToList();
        //        if (result.Count() > 0)
        //        {
        //            foreach (var list in result)
        //            {
        //                List<FilterGroupVM> filterGroups = new List<FilterGroupVM>();

        //                var filters = db.Filters
        //                                .Where(x => x.SubmenuID == list.SubMenuID)
        //                                .ToList();

        //                foreach (var filter in filters)
        //                {
        //                    var values = db.FilterValues
        //                                   .Where(v => v.FilterID == filter.FilterID)
        //                                   .Select(v => new ProductSizeVM
        //                                   {
        //                                       FilterValueID = v.ValueID,
        //                                       FilterValueName = v.Value
        //                                   })
        //                                   .ToList();
        //                    filterGroups.Add(new FilterGroupVM
        //                    {
        //                        GroupName = filter.FilterName,
        //                        Values = values
        //                    });
        //                }

        //                List<ProductVariantsImageVM> imageList = new List<ProductVariantsImageVM>();
        //                var primaryVariant = db.ProductVariants
        //                                       .FirstOrDefault(x => x.ProductID == list.ID && x.IsPrimary == true);
        //                if (primaryVariant != null)
        //                {
        //                    var variantImage = db.tblVaraintsimages
        //                                         .FirstOrDefault(x => x.VariantID == primaryVariant.VariantID);

        //                    if (variantImage != null && !string.IsNullOrEmpty(variantImage.ImageUrl))
        //                    {
        //                        var urls = variantImage.ImageUrl.Split(',');
        //                        foreach (var url in urls)
        //                        {
        //                            imageList.Add(new ProductVariantsImageVM
        //                            {
        //                                ImageURL = url.Trim()
        //                            });
        //                        }
        //                    }
        //                }

        //                mdl.Add(new ProductDetailVM
        //                {
        //                    ID = list.ID,
        //                    ProductName = list.Item_Name,
        //                    ProductUnitName = list.Unit_name,
        //                    ImageList = imageList,
        //                    ProductPointValue =list.PV,
        //                    ProductPrice = list.MRP?.ToString(),
        //                    ProductNRP = list.NRP?.ToString(),
        //                    ProductBasicDiscount = list.BasicDiscount?.ToString(),
        //                    ProductBrand = list.Brand_Name,
        //                    ProductQuantity = list.Stock?.ToString(),
        //                    FilterGroups = filterGroups  
        //                });
        //            }

        //            resp = new ProductDetailResponse { ProductList = mdl };
        //            return resp;
        //        }
        //        else
        //        {
        //            resp = new ProductDetailResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = new ProductDetailResponse { Status_Code = "0", Status = "error", Message = ex.Message };
        //    }

        //    return resp;
        //}





        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetProductDetails")]
        public IHttpActionResult GetProductDetails(int ProductId)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            ProductNewDetailVM.ProductDetailDTO productVM = null;

            var product = db.tblProducts.FirstOrDefault(p => p.ID == ProductId);
            if (product == null)
                return NotFound();

            // Basic product info
            productVM = new ProductNewDetailVM.ProductDetailDTO
            {
                ProductID = (int)product.ID,
                ProductName = product.Item_Name,
                Description = product.Description,
                BasePrice = (decimal)product.NRP,
                ImageUrl = db.tblVaraintsimages
                             .Where(img => img.VariantID == product.ID)
                             .Select(img => img.ImageUrl)
                             .FirstOrDefault() ?? ""
            };

            // Fetch variants
            var variantEntities = db.ProductVariants.Where(v => v.ProductID == product.ID).ToList();
            List<ProductNewDetailVM.ProductVariantDTO> variantVMs = new List<ProductNewDetailVM.ProductVariantDTO>();

            foreach (var variant in variantEntities)
            {
                var variantDTO = new ProductNewDetailVM.ProductVariantDTO
                {
                    VariantID = (int)variant.VariantID,
                        
                    Price = variant.AdditionalPrice ?? 0,
                    StockQuantity = variant.Stock ?? 0,
                    ImageUrl = db.tblVaraintsimages
                                 .Where(i => i.VariantID == variant.VariantID)
                                 .Select(i => i.ImageUrl)
                                 .FirstOrDefault() ?? ""
                };

                // Attributes
                var attributeList = (from va in db.VariantAttributeValues
                                     join a in db.Filters on (long)va.AttributeID equals a.FilterID
                                     join av in db.FilterValues on (long)va.AttributeValueID equals av.ValueID
                                     where va.VariantID == variant.VariantID
                                     select new ProductNewDetailVM.VariantAttributeDTO
                                     {
                                         AttributeName = a.FilterName,
                                         AttributeValue = av.Value
                                     }).ToList();


                variantDTO.Attributes = attributeList;
                variantVMs.Add(variantDTO);
            }

            productVM.Variants = variantVMs;

            return Ok(productVM);
        }
















    }
}