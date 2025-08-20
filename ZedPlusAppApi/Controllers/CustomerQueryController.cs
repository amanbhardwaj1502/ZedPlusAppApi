using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class CustomerQueryController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AddCustomerQuery")]
        public JsonResponse AddCustomerQuery(tblCustomerQuery obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();

            try
            {
                tblCustomerQuery tbl = new tblCustomerQuery();

                tbl.CustomerID = obj.CustomerID;
                tbl.FullName = obj.FullName;
                tbl.EmailID = obj.EmailID;
                tbl.Subject = obj.Subject;
                tbl.Remarks = obj.Remarks;
                tbl.EntryDateTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                tbl.Status = "Pending";
                var data = db.tblCustomerQueries.Add(tbl);
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