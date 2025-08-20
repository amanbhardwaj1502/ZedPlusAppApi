using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class GrievanceController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/GrievanceEntry")]
        public JsonResponse GrievanceEntry(tblGrievance obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();

            try
            {
                tblGrievance tbl = new tblGrievance();

                tbl.CustomerID = obj.CustomerID;
                tbl.Title = obj.Title;
                tbl.Description = obj.Description;
                tbl.DocumentPath = obj.DocumentPath;
                tbl.EntryDateTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                tbl.Status = "Pending";
                var data = db.tblGrievances.Add(tbl);
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


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetGrievanceList")]
        public GetGrievanceListResponse GetGrievanceList(int CustomerId)
        {
            GetGrievanceListResponse resp = new GetGrievanceListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetGrievanceListVM> mdl1 = new List<GetGrievanceListVM>();
                var result = (from tbl in db.tblGrievances
                              join tblA in db.tblCustomers on tbl.CustomerID equals tblA.CustomerID into tbla
                              from tblA in tbla.DefaultIfEmpty()
                              where tbl.CustomerID == CustomerId
                              select new
                              {
                                  tbl.ID,
                                  tblA.CustomerName,
                                  tbl.Title,
                                  tbl.Description,
                                  tbl.DocumentPath,
                                  tbl.EntryDateTime,
                                  tbl.Status,
                                  tbl.Remarks,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Add(new GetGrievanceListVM
                        {
                            ID = list.ID,
                            CustomerName = list.CustomerName,
                            Title = list.Title,
                            Description = list.Description,
                            Remarks = list.Remarks,
                            DocumentPath = list.DocumentPath,
                            EntryDate = list.EntryDateTime,
                            Status = list.Status,
                        });
                    }
                    resp = new GetGrievanceListResponse { GetGrievanceList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetGrievanceListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetGrievanceListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
        
    }
}