using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class SubMenuController : ApiController
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetSubMenu")]
        public SubMenuListResponse GetSubMenu(int SubCatId)
        {
            SubMenuListResponse resp = new SubMenuListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<SubMenuListVM> mdl1 = new List<SubMenuListVM>();

                var result = (from tbl in db.tblSubMenuMasters
                              where tbl.SubCategoryID == SubCatId
                              select new
                              {
                                  tbl.ID,
                                  tbl.SubMenu_Name,
                                  tbl.ImageUrl,
                                  tbl.CategoryID,
                                  tbl.SubCategoryID 
                                  
                              }).ToList();

                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {

                        mdl1.Add(new SubMenuListVM
                        {
                            SubMenuId = list.ID,
                            SubMenuName = list.SubMenu_Name,
                            SubMenuImage = list.ImageUrl,
                            CategoryId = list.CategoryID,
                            SubCategoryId = list.SubCategoryID,
                        });
                    }
                    resp = new SubMenuListResponse { SubMenuList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new SubMenuListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new SubMenuListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetSubMenuSlider")]
        public SubMenuSliderResponse GetSubMenuSlider(int SubMenuId)
        {
            SubMenuSliderResponse resp = new SubMenuSliderResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<SubMenuSliderVM> mdl1 = new List<SubMenuSliderVM>();

                var result = (from tbl in db.tblSubMenuSliders
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

                        mdl1.Add(new SubMenuSliderVM
                        {
                            ID = list.Id,
                            ImagePath = list.ImagePath,
                            Status = list.Status
                        });
                    }
                    resp = new SubMenuSliderResponse { SubMenuSlider = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new SubMenuSliderResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new SubMenuSliderResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

    }
}