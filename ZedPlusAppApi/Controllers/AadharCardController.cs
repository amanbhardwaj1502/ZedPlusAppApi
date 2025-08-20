using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class AadharCardController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AddCustomerAadharCard")]

        public JsonResponse AddCustomerAadharCard(tblCustomerAadharCard objAadhar)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();

            try
            {
                var res = db.tblCustomerAadharCards.FirstOrDefault(x => x.CustomerId == objAadhar.CustomerId);
                if (res == null) 
                {
                    tblCustomerAadharCard tblAadharCard = new tblCustomerAadharCard();

                    tblAadharCard.AadharNumder = objAadhar.AadharNumder;
                    tblAadharCard.CustomerId = objAadhar.CustomerId;
                    tblAadharCard.AadharFrontImage = objAadhar.AadharFrontImage;
                    tblAadharCard.AadharBankImage = objAadhar.AadharBankImage;
                    tblAadharCard.Status = objAadhar.Status;
                    tblAadharCard.DateTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                    var data = db.tblCustomerAadharCards.Add(tblAadharCard);
                    db.SaveChanges();

                    if (tblAadharCard.ID > 0)
                    {
                        resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Successfully Add Aadhaar Card Detail" };
                    }
                    else
                    {
                        resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Something went wrong.Please try again." };
                    }
                }
                else
                {
                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = " Aadhar Already Exist" };
                }

            }
            catch(Exception ex)
            {
                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            
            return resp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetAadharDetails")]
        public GetAadhaarDetailResponse GetAadharDetails(int CustomerId)
        {
            GetAadhaarDetailResponse resp = new GetAadhaarDetailResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                GetAadhaarDetailVM mdl1 = new GetAadhaarDetailVM();

                var result = (from tbl in db.tblCustomerAadharCards
                              join tbla in db.tblCustomers on tbl.CustomerId equals tbla.CustomerID into a
                              from tbla in a.DefaultIfEmpty()
                              where tbl.CustomerId == CustomerId

                              select new
                              {
                                  tbl.ID,
                                  tbla.CustomerName,
                                  tbl.AadharNumder,
                                  tbl.AadharFrontImage,
                                  tbl.AadharBankImage,
                                  tbl.Status,
                                  tbl.DateTime
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Id = list.ID;
                        mdl1.CustomersName = list.CustomerName;
                        mdl1.AadharNumder = list.AadharNumder;
                        mdl1.AadharFrontImage = list.AadharFrontImage;
                        mdl1.AadharBankImage = list.AadharBankImage; 
                        mdl1.Status = list.Status;
                        mdl1.DateTime = list.DateTime;
                    }
                    resp = new GetAadhaarDetailResponse { AadhaarCard = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetAadhaarDetailResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetAadhaarDetailResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }

    
}