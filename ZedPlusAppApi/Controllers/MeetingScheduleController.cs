using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class MeetingScheduleController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetMeetingScheduleList")]
        public GetMeetingScheduleListResponse GetMeetingScheduleList(int UserId)
        {
            GetMeetingScheduleListResponse resp = new GetMeetingScheduleListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetMeetingScheduleListVM> mdl1 = new List<GetMeetingScheduleListVM>();
                var result = (from tbl in db.tblMeetingSchedules
                              where tbl.Status == "Pending"
                              select new
                              {
                                  tbl.ID,
                                  tbl.MeetingDate,
                                  tbl.MeetingCity,
                                  tbl.Location,
                                  tbl.Timing,
                                  tbl.EntryDate,
                                  tbl.Status,
                                  tbl.Longitude,
                                  tbl.Latitude,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Add(new GetMeetingScheduleListVM
                        {
                            ID  = list.ID,
                            MeetingDate = list.MeetingDate,
                            MeetingCity = list.MeetingCity,
                            Location = list.Location,
                            Timing = list.Timing,
                            EntryDate = list.EntryDate,
                            Status = list.Status,
                            Latitude = list.Latitude,
                            Longitude=list.Longitude,
                        });
                    }
                    resp = new GetMeetingScheduleListResponse { GetMeetingScheduleList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetMeetingScheduleListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetMeetingScheduleListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SaveMeetingJoinMembers")]

        public JsonResponse SaveMeetingJoinMembers(tblMeetingJoinMember obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();

            try
            {
                var res = db.tblMeetingJoinMembers.FirstOrDefault(x =>  x.CustomerID == obj.CustomerID && x.MeetingID == obj.MeetingID);
                if (res == null)
                {
                    tblMeetingJoinMember tbl = new tblMeetingJoinMember();

                    tbl.CustomerID = obj.CustomerID;
                    tbl.MeetingID = obj.MeetingID;
                    tbl.TotalPersonJoin = obj.TotalPersonJoin;
                    tbl.EntryDate = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                    tbl.Status = obj.Status;                    
                    var data = db.tblMeetingJoinMembers.Add(tbl);
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
                else
                {
                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "  Already Exist" };
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