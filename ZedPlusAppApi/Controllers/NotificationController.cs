using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class NotificationController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetNotificationList")]
        public GetNotificationResponse GetNotificationList(int UserId)
        {
            GetNotificationResponse resp = new GetNotificationResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetNotificationVM> mdl1 = new List<GetNotificationVM>();
                var result = (from tbl in db.tblNotifications
                              where tbl.Status == "Active"
                              select new
                              {
                                  tbl.ID,
                                  tbl.NotificationDate,
                                  tbl.NotificationTitle,
                                  tbl.NotificationDetail,
                                  tbl.EntryDate,
                                  tbl.Status,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Add(new GetNotificationVM
                        {
                            ID = list.ID,
                            NotificationDate = list.NotificationDate,
                            NotificationTitle = list.NotificationTitle,
                            NotificationDetail = list.NotificationDetail,
                            EntryDate = list.EntryDate,
                            Status = list.Status,
                        });
                    }
                    resp = new GetNotificationResponse { GetNotification = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetNotificationResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetNotificationResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}