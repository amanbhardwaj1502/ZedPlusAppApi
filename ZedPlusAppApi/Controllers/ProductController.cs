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
                    //var result = (from tbl in db.tblProductMasters
                    //              join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
                    //              from tbla in a.DefaultIfEmpty()
                    //              join tblc in db.tblCategoryMasters on tbl.CategoryID equals tblc.CategoryID into c
                    //              from tblc in c.DefaultIfEmpty() 
                    //              join tbls in db.tblSubCategoryMasters on tbl.SubcategoryID equals tbls.SubCategoryID into s
                    //              from tbls in s.DefaultIfEmpty() 
                    //              join tblsm in db.tblSubMenuMasters on tbl.SubMenuID equals tblsm.ID into sm
                    //              from tblsm in sm.DefaultIfEmpty()
                    //              join tblst in db.tblStockEntries.Where(x => x.IsDefault == true)
                    //              on tbl.ID equals tblst.ProductID into st
                    //              from tblst in st.DefaultIfEmpty()
                    //                  //join tblst in db.tblStockEntries on tbl.ID equals tblst.ProductID && tblst.IsDefault == "1" into st
                    //                  //from tblst in st.DefaultIfEmpty()
                    //              join tblimg in db.tblVaraintsimages on tblst.ID equals tblimg.ID into im
                    //              from tblimg in im.DefaultIfEmpty()

                    //                  //join tblj in db.ProductImages on tbl.ID equals tblj.ProductID into j
                    //                  //from tblj in j.DefaultIfEmpty()
                    //              select new
                    //              {
                    //                  tbl.ID,
                    //                  tbl.Item_Name,
                    //                  tblc.Category_Name,
                    //                  tbls.SubCategory_Name,
                    //                  tblsm.SubMenu_Name,
                    //                  tbl.Description,
                    //                  tbl.Status,
                    //                  tblst.Price,
                    //                  tblst.Stock,
                    //                  tblst.QtyperCartoon,
                    //                  tblst.MRP,
                    //                  tblst.NRP,
                    //                  tblst.DP,
                    //                  tblst.GST,
                    //                  tblst.HSN,
                    //                  tblst.BARCODE,
                    //                  tblimg.ImageUrl,
                    //                //  tbl.MRP,
                    //                //  tbl.NRP,
                    //               //   tbl.BasicDiscount,
                    //                  //tblj.ImageURL,
                    //                  tbla.Brand_Name,
                    //                  tbl.Date,
                    //                  //tbl.Quantity
                    //              }).ToList();

                    var result = (from tbl in db.tblProducts
                                  join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
                                  from tbla in a.DefaultIfEmpty()
                                  join tblc in db.tblCategoryMasters on tbl.CategoryID equals tblc.CategoryID into c
                                  from tblc in c.DefaultIfEmpty()
                                  join tbls in db.tblSubCategoryMasters on tbl.SubcategoryID equals tbls.SubCategoryID into s
                                  from tbls in s.DefaultIfEmpty()
                                  join tblsm in db.tblSubMenuMasters on tbl.SubMenuID equals tblsm.ID into sm
                                  from tblsm in sm.DefaultIfEmpty()

                                  join tblst in db.tblStockEntries on tbl.ID equals tblst.ProductID
                                  where tblst.IsDefault == true
                                  join tblimg in db.tblVaraintsimages on tblst.ID equals tblimg.ID into im
                                  from tblimg in im.DefaultIfEmpty()
                                  select new
                                  {
                                      tbl.ID,
                                      tbl.Item_Name,
                                      tblc.Category_Name,
                                      tbls.SubCategory_Name,
                                      tblsm.SubMenu_Name,
                                      tbl.Description,
                                      tbl.Status,
                                      tblst.Price,
                                      tblst.Stock,
                                      tblst.QtyperCartoon,
                                      tblst.MRP,
                                      tblst.NRP,
                                      tblst.DP,
                                      tblst.GST,
                                      tblst.HSN,
                                      tblst.BARCODE,
                                      tbla.Brand_Name,
                                      tbl.Date,
                                      tblimg.ImageUrl
                                  }).ToList();

                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            mdl.Add(new ProductVM
                            {
                                ID = list.ID,
                                ProductName = list.Item_Name,
                                ProductCategory = list.Category_Name,
                                ProductBrand = list.Brand_Name,
                                ProductSubCategory = list.SubCategory_Name,
                                ProductSubMenu = list.SubMenu_Name,
                                ProductDescription = list.Description,
                                ProductImage = list.ImageUrl?.ToString() ?? "0",
                                ProductMRP = list.MRP?.ToString() ?? "0",
                                ProductPrice = list.Price?.ToString() ?? "0",
                                ProductStock = list.Stock?.ToString() ?? "0",
                                ProductNRP = list.NRP?.ToString() ?? "0",
                                ProductDP = list.DP?.ToString() ?? "0",
                                Status = list.Status,
                                Date = list.Date,
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
                else if (SubMenuId != 0 && SubCategoryId == 0 && CategoryId == 0)
                {
                    var result = (from tbl in db.tblProducts
                                  join tbla in db.tblBrandMasters on tbl.BrandID equals tbla.BrandID into a
                                  from tbla in a.DefaultIfEmpty()
                                  join tblc in db.tblCategoryMasters on tbl.CategoryID equals tblc.CategoryID into c
                                  from tblc in c.DefaultIfEmpty()
                                  join tbls in db.tblSubCategoryMasters on tbl.SubcategoryID equals tbls.SubCategoryID into s
                                  from tbls in s.DefaultIfEmpty()
                                  join tblsm in db.tblSubMenuMasters on tbl.SubMenuID equals tblsm.ID into sm
                                  from tblsm in sm.DefaultIfEmpty()
                                  join tblst in db.tblStockEntries on tbl.ID equals tblst.ProductID
                                  where tblst.IsDefault == true
                                  join tblimg in db.tblVaraintsimages on tblst.ID equals tblimg.ID into im
                                  from tblimg in im.DefaultIfEmpty()

                                  where tbl.SubMenuID == SubMenuId
                                  select new
                                  {
                                      tbl.ID,
                                      tbl.Item_Name,
                                      tblc.Category_Name,
                                      tbls.SubCategory_Name,
                                      tblsm.SubMenu_Name,
                                      tbl.Description,
                                      tbl.Status,
                                      tblst.Price,
                                      tblst.Stock,
                                      tblst.QtyperCartoon,
                                      tblst.MRP,
                                      tblst.NRP,
                                      tblst.DP,
                                      tblst.GST,
                                      tblst.HSN,
                                      tblst.BARCODE,
                                      tbla.Brand_Name,
                                      tbl.Date,
                                      tblimg.ImageUrl
                                  }).ToList();
                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            mdl.Add(new ProductVM
                            {
                                ID = list.ID,
                                ProductName = list.Item_Name,
                                ProductCategory = list.Category_Name,
                                ProductBrand = list.Brand_Name,
                                ProductSubCategory = list.SubCategory_Name,
                                ProductSubMenu = list.SubMenu_Name,
                                ProductDescription = list.Description,
                                ProductImage = list.ImageUrl?.ToString() ?? "0",
                                ProductMRP = list.MRP?.ToString() ?? "0",
                                ProductPrice = list.Price?.ToString() ?? "0",
                                ProductStock = list.Stock?.ToString() ?? "0",
                                ProductNRP = list.NRP?.ToString() ?? "0",
                                ProductDP = list.DP?.ToString() ?? "0",
                                Status = list.Status,
                                Date = list.Date,
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
                                  join tblc in db.tblCategoryMasters on tbl.CategoryID equals tblc.CategoryID into c
                                  from tblc in c.DefaultIfEmpty()
                                  join tbls in db.tblSubCategoryMasters on tbl.SubcategoryID equals tbls.SubCategoryID into s
                                  from tbls in s.DefaultIfEmpty()
                                  join tblsm in db.tblSubMenuMasters on tbl.SubMenuID equals tblsm.ID into sm
                                  from tblsm in sm.DefaultIfEmpty()
                                  join tblst in db.tblStockEntries on tbl.ID equals tblst.ProductID
                                  where tblst.IsDefault == true
                                  join tblimg in db.tblVaraintsimages on tblst.ID equals tblimg.ID into im
                                  from tblimg in im.DefaultIfEmpty()

                                  where tbl.SubcategoryID == SubCategoryId
                                  select new
                                  {
                                      tbl.ID,
                                      tbl.Item_Name,
                                      tblc.Category_Name,
                                      tbls.SubCategory_Name,
                                      tblsm.SubMenu_Name,
                                      tbl.Description,
                                      tbl.Status,
                                      tblst.Price,
                                      tblst.Stock,
                                      tblst.QtyperCartoon,
                                      tblst.MRP,
                                      tblst.NRP,
                                      tblst.DP,
                                      tblst.GST,
                                      tblst.HSN,
                                      tblst.BARCODE,
                                      tbla.Brand_Name,
                                      tbl.Date,
                                      tblimg.ImageUrl
                                  }).ToList();
                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            mdl.Add(new ProductVM
                            {
                                ID = list.ID,
                                ProductName = list.Item_Name,
                                ProductCategory = list.Category_Name,
                                ProductBrand = list.Brand_Name,
                                ProductSubCategory = list.SubCategory_Name,
                                ProductSubMenu = list.SubMenu_Name,
                                ProductDescription = list.Description,
                                ProductImage = list.ImageUrl?.ToString() ?? "0",
                                ProductMRP = list.MRP?.ToString() ?? "0",
                                ProductPrice = list.Price?.ToString() ?? "0",
                                ProductStock = list.Stock?.ToString() ?? "0",
                                ProductNRP = list.NRP?.ToString() ?? "0",
                                ProductDP = list.DP?.ToString() ?? "0",
                                Status = list.Status,
                                Date = list.Date,
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
                                  join tblc in db.tblCategoryMasters on tbl.CategoryID equals tblc.CategoryID into c
                                  from tblc in c.DefaultIfEmpty()
                                  join tbls in db.tblSubCategoryMasters on tbl.SubcategoryID equals tbls.SubCategoryID into s
                                  from tbls in s.DefaultIfEmpty()
                                  join tblsm in db.tblSubMenuMasters on tbl.SubMenuID equals tblsm.ID into sm
                                  from tblsm in sm.DefaultIfEmpty()
                                  join tblst in db.tblStockEntries on tbl.ID equals tblst.ProductID
                                  where tblst.IsDefault == true
                                  join tblimg in db.tblVaraintsimages on tblst.ID equals tblimg.ID into im
                                  from tblimg in im.DefaultIfEmpty()
                                  where tbl.CategoryID == CategoryId
                                  select new
                                  {
                                      tbl.ID,
                                      tbl.Item_Name,
                                      tblc.Category_Name,
                                      tbls.SubCategory_Name,
                                      tblsm.SubMenu_Name,
                                      tbl.Description,
                                      tbl.Status,
                                      tblst.Price,
                                      tblst.Stock,
                                      tblst.QtyperCartoon,
                                      tblst.MRP,
                                      tblst.NRP,
                                      tblst.DP,
                                      tblst.GST,
                                      tblst.HSN,
                                      tblst.BARCODE,
                                      tbla.Brand_Name,
                                      tbl.Date,
                                      tblimg.ImageUrl
                                  }).ToList();
                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            mdl.Add(new ProductVM
                            {
                                ID = list.ID,
                                ProductName = list.Item_Name,
                                ProductCategory = list.Category_Name,
                                ProductBrand = list.Brand_Name,
                                ProductSubCategory = list.SubCategory_Name,
                                ProductSubMenu = list.SubMenu_Name,
                                ProductDescription = list.Description,
                                ProductImage = list.ImageUrl?.ToString() ?? "0",
                                ProductMRP = list.MRP?.ToString() ?? "0",
                                ProductPrice = list.Price?.ToString() ?? "0",
                                ProductStock = list.Stock?.ToString() ?? "0",
                                ProductNRP = list.NRP?.ToString() ?? "0",
                                ProductDP = list.DP?.ToString() ?? "0",
                                Status = list.Status,
                                Date = list.Date,
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
                resp = new ProductResponse { Status_Code = "0", Status = "error", Message = "DataBase Timeout" /*ex.Message*/ };
            }
            return resp;
        }















    }
}