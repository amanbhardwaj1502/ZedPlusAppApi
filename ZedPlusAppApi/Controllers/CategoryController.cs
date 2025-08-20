using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class CategoryController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetCategory")]
        public CategoryListResponse GetCategory()
        {
            CategoryListResponse resp = new CategoryListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<CategoryListVM> mdl1 = new List<CategoryListVM>();


                var result = (from tbl in db.tblCategoryMasters
                              select new
                              {
                                  tbl.CategoryID,
                                  tbl.Category_Name,
                                  tbl.CategoryImage
                              }).ToList();
                //var resultsub = (from tbl in db.tblSubCategoryMasters
                //              select new
                //              {
                //                  tbl.CategoryID,
                //                  tbl.SubCategoryID,
                //                  tbl.SubCategory_Name,
                //                  tbl.ImagePath
                //              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        //List<SubCategoryListVM> mdl2 = new List<SubCategoryListVM>();
                        //foreach (var list1 in resultsub.Where(x => x.CategoryID == list.CategoryID))
                        //{
                        //    mdl2.Add(new SubCategoryListVM
                        //    {
                        //        SubCategoryId = list1.SubCategoryID,
                        //        SubCategoryImage = list1.ImagePath,
                        //        SubCategoryName = list1.SubCategory_Name
                        //    });
                        //}
                        mdl1.Add(new CategoryListVM
                        {
                            CategoryId = list.CategoryID,
                            CategoryName = list.Category_Name,
                            CategoryImage = list.CategoryImage
                            //SubCategoryList = mdl2
                        });
                    }
                    resp = new CategoryListResponse { CategoryList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new CategoryListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new CategoryListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetSubCategory")]
        public SubCategoryListResponse GetSubCategory(int CatId)
        {
            SubCategoryListResponse resp = new SubCategoryListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<SubCategoryListVM> mdl1 = new List<SubCategoryListVM>();



                var result = (from tbl in db.tblSubCategoryMasters
                              where tbl.CategoryID == CatId
                              select new
                              {
                                  tbl.CategoryID,
                                  tbl.SubCategoryID,
                                  tbl.SubCategory_Name,
                                  tbl.ImagePath
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {

                        mdl1.Add(new SubCategoryListVM
                        {
                            SubCategoryId = list.SubCategoryID,
                            SubCategoryName = list.SubCategory_Name,
                            SubCategoryImage = list.ImagePath
                        });
                    }
                    resp = new SubCategoryListResponse { SubCategoryList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new SubCategoryListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new SubCategoryListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetCategorySlider")]
        public CategorySliderResponse GetCategorySlider(int CategoryId)
        {
            CategorySliderResponse resp = new CategorySliderResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<CategorySliderVM> mdl1 = new List<CategorySliderVM>();

                    

                var result = (from tbl in db.tblCategorySliders
                              where tbl.Status == "Active"
                              select new
                              {
                                  tbl.Id,
                                  tbl.ImagePath,
                                  tbl.Status
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {

                        mdl1.Add(new CategorySliderVM
                        {
                            Id = list.Id,
                            ImagePath = list.ImagePath,
                            Status = list.Status,
                        });
                    }
                    resp = new CategorySliderResponse { CategorySliderList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new CategorySliderResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new CategorySliderResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetSubCategorySlider")]
        public SubCategorySliderResponse GetSubCategorySlider(int SubCategoryId)
        {
            SubCategorySliderResponse resp = new SubCategorySliderResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<SubCategorySliderVM> mdl1 = new List<SubCategorySliderVM>();



                var result = (from tbl in db.tblSubcategorySliders
                              where tbl.Status == "Active"
                              select new
                              {
                                  tbl.Id,
                                  tbl.ImagePath,
                                  tbl.Status
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {

                        mdl1.Add(new SubCategorySliderVM
                        {
                            Id = list.Id,
                            ImagePath = list.ImagePath,
                            Status = list.Status,
                        });
                    }
                    resp = new SubCategorySliderResponse { SubCategorySliderList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new SubCategorySliderResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new SubCategorySliderResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}