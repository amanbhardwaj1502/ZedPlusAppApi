using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class LegalDocumentController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetLegalDocumentList")]
        public GetLegalDocumentListResponse GetLegalDocumentList(int UserId)
        {
            GetLegalDocumentListResponse resp = new GetLegalDocumentListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetLegalDocumentListVM> mdl1 = new List<GetLegalDocumentListVM>();
                var result = (from tbl in db.tblLegalDocuments
                              where tbl.Status == "Active"
                              select new
                              {
                                  tbl.ID,
                                  tbl.DocumentName,
                                  tbl.DocumentDetail,
                                  tbl.EntryDate,
                                  tbl.Status
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Add(new GetLegalDocumentListVM
                        {
                            ID = list.ID,
                            DocumentName = list.DocumentName,
                            DocumentURL = list.DocumentDetail,
                            EntryDate = list.EntryDate,
                            Status = list.Status,
                        });
                    }
                    resp = new GetLegalDocumentListResponse { GetLegalDocumentList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetLegalDocumentListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetLegalDocumentListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetBussinessPlanList")]
        public GetBussinessPlanResponse GetBussinessPlanList(int CustomerId)
        {
            GetBussinessPlanResponse resp = new GetBussinessPlanResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetBussinessPlanVM> mdl1 = new List<GetBussinessPlanVM>();
                var result = (from tbl in db.tblBussinessPlans
                              where tbl.Status == "Active"
                              orderby tbl.ID descending
                              select new
                              {
                                  tbl.ID,
                                  tbl.DocumentUrl,
                                  tbl.EntryDate,
                                  tbl.Status
                              }).FirstOrDefault();
                if (result!=null)
                {                    
                    mdl1.Add(new GetBussinessPlanVM
                    {
                        ID = result.ID,
                        DocumentUrl = result.DocumentUrl,
                        EntryDate = result.EntryDate,
                        Status = result.Status,
                    });                    
                    resp = new GetBussinessPlanResponse { GetBussinessPlan = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetBussinessPlanResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetBussinessPlanResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}