using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class PincodeController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPicodeDetails")]
        public PinCodeResponse GetPicodeDetails(long Pincode)
        {
            PinCodeResponse resp = new PinCodeResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                PinCodeVM mdl1 = new PinCodeVM();

                var result = (from tbl in db.tblCityMasters
                              join tbla in db.tblDistrictMasters on tbl.DistrictID equals tbla.ID into a
                              from tbla in a.DefaultIfEmpty()
                              join tblb in db.tblStateMasters on tbla.StateID equals tblb.StateID into b
                              from tblb in b.DefaultIfEmpty()
                              join tblc in db.tblCountryMasters on tblb.Country_ID equals tblc.ID into c
                              from tblc in c.DefaultIfEmpty()
                              where tbl.PinCode == Pincode

                              select new
                              {
                                  tbl.CityID,
                                  tbl.City_Name,
                                  tbla.DistrictName,
                                  tblb.State_Name,
                                  tblc.Country_Name,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Id = list.CityID;
                        mdl1.CountryName = list.Country_Name;
                        mdl1.StateName = list.State_Name;
                        mdl1.DistrictName = list.DistrictName;
                        mdl1.CityName = list.City_Name;
                    }
                    resp = new PinCodeResponse { PicodeDetails = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new PinCodeResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new PinCodeResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}