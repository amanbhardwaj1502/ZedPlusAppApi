using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class AddMoneyController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Addmoney")]

        public JsonResponse AddMoney(tbl_Wallet obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {
                tbl_Wallet tbl = new tbl_Wallet();
                tbl.UserId = obj.UserId;
                tbl.TransitionAmount = obj.TransitionAmount;
                tbl.TransitionTypes = obj.TransitionTypes;
                tbl.TransitionDate = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                tbl.TransitionStatus = obj.TransitionStatus ;
                var data = db.tbl_Wallet.Add(tbl);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/UserWalletBalance")]

        public UserWalletBalanceResponse UserWalletBalance(int Userid)
        {
            UserWalletBalanceResponse resp = new UserWalletBalanceResponse();

            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            try
            {
                var tbl = db.tbl_Wallet.Where(x => x.UserId == (Userid)).ToList();
                double total = 0;
                foreach (var i in tbl)
                {
                    if(i.TransitionTypes == "Deposit")
                    {
                        try
                        {
                            total += Convert.ToDouble(i.TransitionAmount);
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            total -= Convert.ToDouble(i.TransitionAmount);
                        }
                        catch { }

                    }

                }
                if (tbl.Count() > 0)
                {
                    resp = new UserWalletBalanceResponse { TotalBalance = total };
                }
                else
                {
                    resp = new UserWalletBalanceResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new UserWalletBalanceResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetWalletHistory")]

        public UserWalletDetailResponse GetWalletHistory(int UserId)
        {
            UserWalletDetailResponse resp = new UserWalletDetailResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<UserWalletDetailVM> mdl = new List<UserWalletDetailVM>();

                var result = (from tbl in db.tbl_Wallet
                              join tbla in db.tblCustomers on tbl.UserId equals tbla.CustomerID into a
                              from tbla in a.DefaultIfEmpty()
                              where tbl.UserId == UserId
                              select new
                              {
                                  tbl.Id,
                                  tbl.TransitionAmount,
                                  tbl.TransitionTypes,
                                  tbl.TransitionStatus,
                                  tbl.TransitionDate,
                                  tbla.CustomerName,                                
                              }).ToList();

                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl.Add(new UserWalletDetailVM
                        {
                            Id = list.Id,
                            TransitionAmount = list.TransitionAmount,
                            TransitionTypes = list.TransitionTypes,
                            TransitionStatus = list.TransitionStatus,
                            TransitionDate = list.TransitionDate,
                            UserName = list.CustomerName,                         
                        });
                    }
                    resp = new UserWalletDetailResponse { WalletList = mdl };
                    return resp;
                }
                else
                {
                    resp = new UserWalletDetailResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new UserWalletDetailResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}