using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class CountryController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetCountry")]
        public CountryResponse GetCountry()
        {
            CountryResponse resp = new CountryResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<CountryVM> mdl1 = new List<CountryVM>();
                var result = (from tbl in db.tblCountryMasters
                              where tbl.Status == "Active"
                              orderby tbl.Country_Name ascending
                              select new
                              {
                                  tbl.Country_Name,
                                  tbl.ID,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {

                        mdl1.Add(new CountryVM
                        {
                            CountryId = list.ID,
                            CountryName = list.Country_Name,
                        });
                    }
                    resp = new CountryResponse { CountryList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new CountryResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new CountryResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

    }
}