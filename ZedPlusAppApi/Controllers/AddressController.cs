using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class AddressController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AddAddress")]
        public JsonResponse AddAddress(AddAddressVM obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {
                tblAddress tbl = new tblAddress();
                tbl.CustomerId = obj.CustomerId;
                tbl.CountryId = obj.CountryId;
                tbl.StateId = obj.StateId;
                tbl.DistrictId = obj.DistrictId;
                tbl.AddressType = obj.AddressType;
                tbl.Address = obj.Address;
                tbl.PinCode = obj.PinCode;
                tbl.Name = obj.Name;
                tbl.Phone = obj.MobileNumber;
                tbl.Status = "Active";
                var data = db.tblAddresses.Add(tbl);
                db.SaveChanges();
                if (tbl.Id > 0)
                {
                    resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Address Saved Successfully" };
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


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetAddressList")]
        public GetAddressResponse GetAddressList(int CustomerId)
        {
            GetAddressResponse resp = new GetAddressResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetAddressVM> mdl1 = new List<GetAddressVM>();

                var result = (from tbl in db.tblAddresses
                              join tbla in db.tblCustomers on tbl.CustomerId equals tbla.CustomerID into a
                              from tbla in a.DefaultIfEmpty()
                              join tblb in db.tblCountryMasters on tbl.CountryId equals tblb.ID into b
                              from tblb in b.DefaultIfEmpty()
                              join tblc in db.tblStateMasters on tbl.StateId equals tblc.StateID into c
                              from tblc in c.DefaultIfEmpty()
                              join tbld in db.tblDistrictMasters on tbl.DistrictId equals tbld.ID into d
                              from tbld in d.DefaultIfEmpty()
                              where tbl.Status == "Active" && tbl.CustomerId == CustomerId

                              select new
                              {
                                  tbl.Id,
                                  tbla.CustomerName,
                                  tblb.Country_Name,
                                  tblc.State_Name,
                                  tbld.DistrictName,
                                  tbl.PinCode,
                                  tbl.AddressType,
                                  tbl.Address,
                                  tbl.Status,
                                  tbl.Name,
                                  tbl.Phone
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        
                        mdl1.Add(new GetAddressVM
                        {
                            Id = list.Id,
                            CustomerName = list.CustomerName,
                            CountryName = list.Country_Name,
                            StateName = list.State_Name,
                            DistrictName = list.DistrictName,
                            PinCode = list.PinCode,
                            Address = list.Address,
                            AddressType = list.AddressType,
                            Name = list.Name,
                            MobileNumber = list.Phone,
                        });
                    }
                    resp = new GetAddressResponse { AddressList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetAddressResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetAddressResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/UpdateAddress")]
        public JsonResponse UpdateAddress(UpdateAddressVM obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {

                tblAddress  tbl = db.tblAddresses.FirstOrDefault(p => p.Id == obj.Id);
                if (tbl != null)
                {
                    tbl.CustomerId = obj.CustomerId;
                    tbl.CountryId = obj.CountryId;
                    tbl.StateId = obj.StateId;
                    tbl.DistrictId = obj.DistrictId;
                    tbl.AddressType = obj.AddressType;
                    tbl.Address = obj.Address;
                    tbl.PinCode = obj.PinCode;
                    tbl.Name = obj.Name;
                    tbl.Phone = obj.MobileNumber;
                    db.Entry(tbl).State = EntityState.Modified;
                    db.SaveChanges();
                    resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Address Updated Successfully" };
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
        [System.Web.Http.Route("api/RemoveAddress")]
        public JsonResponse RemoveAddress(int AddressId)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {

                tblAddress tbl = db.tblAddresses.FirstOrDefault(p => p.Id == AddressId);
                if (tbl != null)
                {
                    tbl.Status = "DeActive";
                    db.Entry(tbl).State = EntityState.Modified;
                    db.SaveChanges();
                    resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Address Removed Successfully" };
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