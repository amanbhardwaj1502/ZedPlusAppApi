using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class LoginController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/UserLogin")]
        public UserLoginResponse UserLogin(string mobileNo)
        {
            UserLoginResponse resp = new UserLoginResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                UserLoginVM model = new UserLoginVM();


                Int64 mobileNo1 = Convert.ToInt64(mobileNo);
                var result = db.tblCustomers.FirstOrDefault(p => p.CustomerPhone == mobileNo1 && p.Status != "DeActive");

                if (result != null)
                {
                    var res = from tbl in db.tblOrders
                              join tbla in db.tblCustomers on tbl.CustomerID equals tbla.CustomerID into a
                              from tbla in a.DefaultIfEmpty()
                              where tbl.CustomerID == result.CustomerID
                              select new
                              {
                                  tbl.TotalPV,
                                  tbla.CustomerName,
                                  tbla.Status,
                                  tbla.CustomerCode,
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
                    tblCustomer tblcust = db.tblCustomers.FirstOrDefault(x => x.CustomerID == result.CustomerID);
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
                    if (result.Status != "DeActive" && result.Status != "Delete")
                    {
                        model.Id = result.CustomerID;
                        model.CustomerCode = result.CustomerCode;
                        model.Name = result.CustomerName;
                        model.Mobile = Convert.ToString(result.CustomerPhone);
                        model.Email = result.CustomerEmail;
                        model.Status = result.Status;
                        model.Position = result.Position;
                        db.SaveChanges();
                        resp = new UserLoginResponse { EmpLogin = model };
                    }
                    else { resp = new UserLoginResponse { Status_Code = "0", Status = "error", Message = "Your account is "+ result.Status + "" }; }
                }
                else
                {
                    resp = new UserLoginResponse { Status_Code = "0", Status = "error", Message = "Mobile Number Has Wrong" };
                }
            }
            catch (Exception ex)
            {
                resp = new UserLoginResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AccountDeactivate")]
        public JsonResponse AccountDeactivate(int customerid)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {

                tblCustomer tbl = db.tblCustomers.FirstOrDefault(p => p.CustomerID == customerid);
                if (tbl != null)
                {
                    tbl.Status = "DeActive";
                    db.Entry(tbl).State = EntityState.Modified;
                    db.SaveChanges();
                    resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Account Deactive Successfully" };
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

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AccountDelete")]
        public JsonResponse AccountDelete(int customerid)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {

                tblCustomer tbl = db.tblCustomers.FirstOrDefault(p => p.CustomerID == customerid);
                if (tbl != null)
                {
                    tbl.Status = "Delete";
                    db.Entry(tbl).State = EntityState.Modified;
                    db.SaveChanges();
                    resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Account Deleted Successfully" };
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