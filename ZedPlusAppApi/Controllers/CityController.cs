using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class CityController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetCity")]
        public CityResponse GetCity(int DistrictId)
        {
            CityResponse resp = new CityResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<CityVM> mdl1 = new List<CityVM>();



                var result = (from tbl in db.tblCityMasters
                              where tbl.DistrictID == DistrictId && tbl.City_Status == "Active"
                              orderby tbl.City_Name ascending
                              select new
                              {
                                  tbl.CityID,
                                  tbl.City_Name,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {

                        mdl1.Add(new CityVM
                        {
                            CityId = list.CityID,
                            CityName = list.City_Name
                        });
                    }
                    resp = new CityResponse { CityList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new CityResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new CityResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}