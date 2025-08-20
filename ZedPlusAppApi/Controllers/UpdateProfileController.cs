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
    public class UpdateProfileController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/SaveProfileDetail")]
        public JsonResponse SaveProfileDetail(int id, ProfileDetailsVM obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {
                Random rand = new Random();
                int randomno = rand.Next(1000000, 9999999);
                string strphotpPath = "";
                if (obj.Image.Length > 0)
                {
                    try
                    {
                        var base64 = obj.Image;
                        var buffer = Convert.FromBase64String(base64);
                        var file = System.Web.HttpContext.Current.Server.MapPath("~/Image/" + "User-" + randomno + ".jpg");
                        System.IO.File.WriteAllBytes(file, buffer);
                        //strphotpPath = "http://43.224.1.62/ZedPlusAppApi/image/" + "User-" + randomno + ".jpg";
                        strphotpPath = "http://zedplusappapi.libitsolutions.com/image/" + "User-" + randomno + ".jpg";
                    }
                    catch (Exception ex)
                    {

                        resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.ToString() };
                    }
                }
                if (id > 0)
                {

                    tblCustomer tbl = db.tblCustomers.FirstOrDefault(p => p.CustomerID == id);
                    if (tbl != null)
                    {
                        tbl.CustomerName = obj.Name;
                        tbl.DOB = obj.DateOfBirth;
                        tbl.CustomerEmail = obj.EmailID;
                        tbl.CustomerPhone = Convert.ToInt64(obj.Mobilenumber);
                        if (tbl.CustomerImage == obj.Image)
                        {

                        }
                        else
                        {
                            tbl.CustomerImage = strphotpPath;
                        }

                        db.Entry(tbl).State = EntityState.Modified;
                        db.SaveChanges();
                        resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Profile Detail Update Successfully" };
                    }
                    else
                    {
                        resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Something went wrong.Please try again." };
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
        [System.Web.Http.Route("api/getProfileDetails")]
        public UserProfileDetailResponse getProfileDetails(long ID)
        {
            UserProfileDetailResponse resp = new UserProfileDetailResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                ProfileDetailsVM model = new ProfileDetailsVM();

                var result = db.tblCustomers.FirstOrDefault(p => p.CustomerID == ID);

                if (result != null)
                {
                    model.Id = result.CustomerID;
                    model.Name = result.CustomerName;
                    model.Mobilenumber = Convert.ToString(result.CustomerPhone);
                    model.EmailID = result.CustomerEmail;
                    model.DateOfBirth = result.DOB;
                    model.Image = result.CustomerImage;
                    resp = new UserProfileDetailResponse { ProfileDetails = model };
                }
                else
                {
                    resp = new UserProfileDetailResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new UserProfileDetailResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }




        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/getUserUplineDetails")]
        public UserUplineDetailResponse getUserUplineDetails(long ID)
        {
            UserUplineDetailResponse resp = new UserUplineDetailResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                UserUplinedetailsVM model = new UserUplinedetailsVM();

                var UserResult = db.tblCustomers.FirstOrDefault(p => p.CustomerID == ID);

                var sponsordetails = db.tblCustomers.FirstOrDefault(p => p.CustomerID == UserResult.UserID);

                if (UserResult != null)
                {
                    model.Id = UserResult.CustomerID;
                    model.Name = UserResult.CustomerName;
                    model.sponsorID = sponsordetails.CustomerID;
                    model.sponsorCode = sponsordetails.CustomerCode;
                    model.sponsorName = sponsordetails.CustomerName;
                    resp = new UserUplineDetailResponse { UserUplineDetails = model };
                }
                else
                {
                    resp = new UserUplineDetailResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new UserUplineDetailResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}