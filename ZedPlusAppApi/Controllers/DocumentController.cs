using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class DocumentController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetDocumentList")]
        public DocumentResponse GetDocumentList(int userId)
        {
            DocumentResponse resp = new DocumentResponse();
            List<DocumentListVM> mdl = new List<DocumentListVM>();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();

                var result = (from tbl in db.tblDocumentMasters                             
                              select new
                              {
                                  tbl.ID,
                                  tbl.Document_Name,
                                  tbl.Document_Path,
                                  tbl.Status,                              

                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl.Add(new DocumentListVM
                        {
                            ID = list.ID,
                            DocumentName = list.Document_Name,
                            DocumentPath = list.Document_Path,                         
                            Status = list.Status
                        });
                    }
                    resp = new DocumentResponse { DocumentList = mdl };
                    return resp;
                }
                else
                {
                    resp = new DocumentResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new DocumentResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}