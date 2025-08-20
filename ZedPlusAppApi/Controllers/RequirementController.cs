using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class RequirementController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetRequirementList")]
        public GetRequirementListResponse GetRequirementList()
        {
            GetRequirementListResponse resp = new GetRequirementListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetRequirementListVM> mdl1 = new List<GetRequirementListVM>();
                var result = (from tbl in db.tblRequirements
                              where tbl.Status == "Pending"
                              select new
                              {
                                  tbl.ID,
                                  tbl.Title,
                                  tbl.ReqDescription,
                                  tbl.Date,
                                  tbl.EntryDateTime,
                                  tbl.Status,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Add(new GetRequirementListVM
                        {
                            ID = list.ID,
                            Title = list.Title,
                            ReqDescription = list.ReqDescription,
                            Date = list.Date,
                            EntryDateTime = list.EntryDateTime,
                            Status = list.Status,
                        });
                    }
                    resp = new GetRequirementListResponse { GetRequirementList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetRequirementListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetRequirementListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ApplyForRequirement")]
        public JsonResponse ApplyForRequirement(tblCustomerApply obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();

            try
            {
                tblCustomerApply tbl = new tblCustomerApply();

                tbl.RequirementID=obj.RequirementID;
                tbl.CustomerID = obj.CustomerID;
                tbl.FullName=obj.FullName;
                tbl.MobileNumber = obj.MobileNumber;
                tbl.City= obj.City;
                tbl.Remarks = obj.Remarks;
                tbl.EntryDateTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                tbl.Status = "Pending";
                var data = db.tblCustomerApplies.Add(tbl);
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