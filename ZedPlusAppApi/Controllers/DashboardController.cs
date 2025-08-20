using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class DashboardController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetDashboardSlider")]
        public DashboardSliderResponse GetDashboardSlider()
        {
            DashboardSliderResponse resp = new DashboardSliderResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<DashboardSliderVM> mdl1 = new List<DashboardSliderVM>();



                var result = (from tbl in db.tblDashboardSliders
                              where  tbl.Status == "Active"
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

                        mdl1.Add(new DashboardSliderVM
                        {
                            Id = list.Id,
                            ImagePath = list.ImagePath,
                            Status = list.Status,
                        });
                    }
                    resp = new DashboardSliderResponse { DashboardSliderList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new DashboardSliderResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new DashboardSliderResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}