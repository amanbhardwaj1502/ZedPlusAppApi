using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class DistrictController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetDistrict")]
        public DistrictResponse GetDistrict(int StateId)
        {
            DistrictResponse resp = new DistrictResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<DistrictVM> mdl1 = new List<DistrictVM>();



                var result = (from tbl in db.tblDistrictMasters
                              where tbl.StateID == StateId && tbl.Status == "Active"
                              orderby tbl.DistrictName ascending
                              select new
                              {
                                  tbl.DistrictName,
                                  tbl.ID,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {

                        mdl1.Add(new DistrictVM
                        {
                            DistrictId = list.ID,
                            DistrictName = list.DistrictName
                        });
                    }
                    resp = new DistrictResponse { DistrictList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new DistrictResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new DistrictResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}