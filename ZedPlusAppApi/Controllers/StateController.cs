using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class StateController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetState")] 
        public StateResponse GetState(int CountryId)
        {
            StateResponse resp = new StateResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<StateVM> mdl1 = new List<StateVM>();



                var result = (from tbl in db.tblStateMasters
                              where tbl.Country_ID == CountryId && tbl.State_Status == "Active"
                              orderby tbl.State_Name ascending
                              select new
                              {
                                  tbl.State_Name,
                                  tbl.StateID,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {

                        mdl1.Add(new StateVM
                        {
                            StateId = list.StateID,
                            StateName = list.State_Name 
                        });
                    }
                    resp = new StateResponse { StateList  = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new StateResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new StateResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}