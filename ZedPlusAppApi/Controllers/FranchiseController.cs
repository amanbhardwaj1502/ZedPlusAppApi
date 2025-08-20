using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class FranchiseController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/FranchiseRequest")]

        public JsonResponse FranchiseRequest(tblFranchiseRequest obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();

            try
            {
                tblFranchiseRequest tbl = new tblFranchiseRequest();

                tbl.CustomerID = obj.CustomerID;
                tbl.StoreTypeID = obj.StoreTypeID;
                tbl.FullName = obj.FullName;
                tbl.MobileNo = obj.MobileNo;
                tbl.Remarks = obj.Remarks;
                tbl.EntryDateTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                tbl.Status = "Pending";
                db.tblFranchiseRequests.Add(tbl);
                db.SaveChanges();

                if (tbl.ID > 0)
                {
                    resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Successfully Add  Detail" };
                }
                else
                {
                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Something went wrong.Please try again." };
                }               

            }
            catch (Exception ex)
            {
                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }

            return resp;
        }
    }
}