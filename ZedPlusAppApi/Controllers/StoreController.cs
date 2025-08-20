using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class StoreController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetStoreTypeList")]
        public GetStoreTypeListResponse GetStoreListList(int CustomerId)
        {
            GetStoreTypeListResponse resp = new GetStoreTypeListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetStoreTypeListVM> mdl1 = new List<GetStoreTypeListVM>();
                var result = (from tbl in db.tblStoreTypeMasters
                              where tbl.Status == "Active"
                              select new
                              {
                                  tbl.ID,
                                  tbl.Store_type,
                                  tbl.Status,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Add(new GetStoreTypeListVM
                        {
                            ID = list.ID,
                            StoreType = list.Store_type,
                            Status = list.Status,
                        });
                    }
                    resp = new GetStoreTypeListResponse { GetStoreTypeList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetStoreTypeListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetStoreTypeListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}