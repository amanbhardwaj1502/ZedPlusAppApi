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
    public class BankDetailsController : ApiController
    {

        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/SaveBankDetail")]
        //public JsonResponse SaveBankDetail(int id, BankDetailsVM obj)
        //{
        //    db_ZedPlusShopEntities db = new db_ZedPlusShopEntities();
        //    JsonResponse resp = new JsonResponse();
        //    try
        //    {
        //        if (id > 0)
        //        {
        //            tblRegistration tbl = db.tblRegistrations.FirstOrDefault(p => p.Reg_ID == id);
        //            if (tbl != null)
        //            {                    
        //                tbl.FullName = obj.FullName;
        //                tbl.BankName = obj.BankName;
        //                tbl.AccountNumber = obj.AccountNumber;
        //                tbl.IFSCcode = obj.IFSCCode;
        //                tbl.BranchName = obj.BranchName;
        //                tbl.AadhaarCard = obj.AadhaarCard;
        //                tbl.PanCard = obj.PanCard;
        //                db.Entry(tbl).State = EntityState.Modified;
        //                db.SaveChanges();
        //                resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Bank Detail Update Successfully" };
        //            }
        //            else
        //            {
        //                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Something went wrong.Please try again." };
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
        //    }
        //    return resp;
        //}



        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/getBankDetails")]
        //public UserBankDetailResponse getBankDetails(long ID)
        //{
        //    UserBankDetailResponse resp = new UserBankDetailResponse();
        //    try
        //    {
        //        db_ZedPlusShopEntities db = new db_ZedPlusShopEntities();
        //        BankDetailsVM model = new BankDetailsVM();

        //        var result = db.tblRegistrations.FirstOrDefault(p => p.Reg_ID == ID);

        //        if (result != null)
        //        {                  
        //                model.Id = result.Reg_ID;
        //                model.FullName = result.FullName;
        //                model.BankName = result.BankName;
        //                model.AccountNumber = result.AccountNumber;
        //                model.IFSCCode = result.IFSCcode;
        //                model.BranchName = result.BranchName;
        //                model.AadhaarCard = result.AadhaarCard;
        //                model.PanCard = result.PanCard;                    

        //                resp = new UserBankDetailResponse { BankDetails = model };                    
        //        }
        //        else
        //        {
        //            resp = new UserBankDetailResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = new UserBankDetailResponse { Status_Code = "0", Status = "error", Message = ex.Message };
        //    }
        //    return resp;
        //}



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetBankList")]
        public BankListResponse GetBankList()
        {
            BankListResponse resp = new BankListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<BankListVM> mdl1 = new List<BankListVM>();



                var result = (from tbl in db.tblBankMasters
                              where tbl.Bank_Status == "Active"
                              select new
                              {
                                  tbl.Bank_name,
                                  tbl.ID,
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Add(new BankListVM
                        {
                            BankId = list.ID,
                            BankName = list.Bank_name,
                        });
                    }
                    resp = new BankListResponse { BankList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new BankListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new BankListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SaveBankDetail")]
        public JsonResponse SaveBankDetail(int id, BankDetailsVM obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {
                if (id > 0)
                {
                    tblCustomerBankDetail tbl = db.tblCustomerBankDetails.FirstOrDefault(p => p.CustomerID == id);
                    if (tbl != null)
                    {
                        tbl.CustomerID = id;
                        tbl.FullName = obj.FullName;
                        tbl.BankID = obj.BankId;
                        tbl.AccountNumber = obj.AccountNumber;
                        tbl.IFSCcode = obj.IFSCCode;
                        tbl.BranchName = obj.BranchName;
                        tbl.Status = "Pending";
                        db.Entry(tbl).State = EntityState.Modified;
                        db.SaveChanges();
                        resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Bank Detail Update Successfully" };

                    }
                    else
                    {
                        tblCustomerBankDetail tbl1 = new tblCustomerBankDetail();
                        tbl1.CustomerID = id;
                        tbl1.FullName = obj.FullName;
                        tbl1.BankID = obj.BankId;
                        tbl1.AccountNumber = obj.AccountNumber;
                        tbl1.IFSCcode = obj.IFSCCode;
                        tbl1.BranchName = obj.BranchName;
                        tbl1.Status = "Pending";
                        var data = db.tblCustomerBankDetails.Add(tbl1);
                        db.SaveChanges();
                        resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Bank Detail Update Successfully" };

                    }
                }
            }
            catch (Exception ex)
            {
                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }



        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/getBankDetails")]
        public GetBankDetailResponse getBankDetails(long ID)
        {
            GetBankDetailResponse resp = new GetBankDetailResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetBankDetailVM> model = new List<GetBankDetailVM>();

                //var result = db.tblCustomerBankDetails.FirstOrDefault(p => p.CustomerID == ID);


                var result = (from tbl in db.tblCustomerBankDetails
                              join tbla in db.tblBankMasters on tbl.BankID equals tbla.ID into a
                              from tbla in a.DefaultIfEmpty()
                              join tblb in db.tblCustomers on tbl.CustomerID equals tblb.CustomerID into b
                              from tblb in b.DefaultIfEmpty()
                              where tbl.CustomerID == ID   // &&  tbl.Status == "Active" 

                              select new
                              {
                                  tbl.Id,
                                  tbla.Bank_name,
                                  tblb.CustomerName,
                                  tbl.FullName,
                                  tbl.AccountNumber,
                                  tbl.IFSCcode,                                 
                                  tbl.BranchName,
                                  tbl.Status   
                                  
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {

                        model.Add(new GetBankDetailVM
                        {
                            Id = list.Id,
                            FullName = list.FullName,
                            BankName = list.Bank_name,
                            UserName = list.CustomerName,
                            AccountNumber = list.AccountNumber,
                            IFSCCode = list.IFSCcode,
                            BranchName = list.BranchName,
                            Status = list.Status,                         
                        });
                    }
                    resp = new GetBankDetailResponse { BankList = model };
                    return resp;
                }
                else
                {
                    resp = new GetBankDetailResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetBankDetailResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


    }
}