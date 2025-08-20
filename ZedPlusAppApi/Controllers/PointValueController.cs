using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class PointValueController : ApiController
    {
        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/CustomerPointValue")]
        //public customerPointValueResponse CustomerPointValue(int customerID)
        //{
        //    customerPointValueResponse resp = new customerPointValueResponse();
        //    using (var db = new db_ZedPlusShopEntities())
        //    {
        //        try
        //        {
        //            var Result = db.sp_pointvalue(customerID).ToList();

        //            if(Result != null && Result.Count() > 0)
        //            {
        //                resp.PointValue = Result;
        //                resp.Status_Code = "200";
        //                resp.Status = "Success";
        //                resp.Message = "Success";
        //            }
        //            else
        //            {
        //                resp.Status_Code = "0";
        //                resp.Status = "error";
        //                resp.Message = "Data Not Found";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            resp.Status_Code = "0";
        //            resp.Status = "error";
        //            resp.Message = ex.Message;
        //        }
        //    }
        //    return resp;
        //}
        //public class customerPointValueResponse
        //{
        //    public string Status_Code { get; set; }
        //    public string Status { get; set; }
        //    public string Message { get; set; }
        //    public object PointValue { get; set; }

      


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/CustomerTotalPointValue")]
        public customerPointValueResponse CustomerTotalPointValue(int Userid)
        {
            customerPointValueResponse resp = new customerPointValueResponse();
            List<PointValueVM> mdl1 = new List<PointValueVM>();
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            try
            {              
                var res = from tbl in db.tblOrders
                          join tbla in db.tblCustomers on tbl.CustomerID equals tbla.CustomerID into a
                          from tbla in a.DefaultIfEmpty()
                          where tbl.CustomerID == Userid
                          select new 
                          {
                              tbl.TotalPV,
                              tbla.CustomerName,
                              tbla.Position
                          };
                double sum = 0;
                foreach (var x in res)
                {
                    try
                    {
                        sum += Convert.ToDouble(x.TotalPV);
                    }
                    catch { }

                }
                tblCustomer tblcust = db.tblCustomers.FirstOrDefault(x => x.CustomerID == Userid);
                if (sum >= 1200 && sum < 3000)
                {
                    tblcust.Position = "Introducer";
                    db.Entry(tblcust).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (sum >= 3000)
                {
                    tblcust.Position = "Member";
                    db.Entry(tblcust).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else if (sum < 1200)
                {
                    tblcust.Position = "Guest";
                    db.Entry(tblcust).State = EntityState.Modified;
                    db.SaveChanges();
                }
                if (res.Count() > 0)
                {
                    mdl1.Add(new PointValueVM
                    {
                        Name = res.FirstOrDefault().CustomerName,
                        TotalPV = sum,
                        Position = res.FirstOrDefault().Position                     
                    });
                    resp = new customerPointValueResponse { PointValueList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new customerPointValueResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new customerPointValueResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}