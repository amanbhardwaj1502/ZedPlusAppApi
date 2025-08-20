using Microsoft.Ajax.Utilities;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ZedPlusAppApi.Models;
using System.Reflection;

namespace ZedPlusAppApi.Controllers
{
    public class MemberController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AddGuestMember")]
        public JsonResponse AddGuestMember(AddGuestMemberVM obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {
                var mno = Convert.ToInt64(obj.MobileNumber);
                //var mobile = db.tblMembers.Where(x => x.Member_Mobile == obj.MobileNumber).FirstOrDefault();
                var mobile1 = db.tblCustomers.Where(x => x.CustomerPhone == mno).FirstOrDefault();
                //if (mobile != null || mobile1 != null)
                if (mobile1 != null)
                {
                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Mobile Number Already Exist" };
                }
                else
                {
                    string MaxNo;
                    using (var context = new db_zedPlusShopEntities())
                    {
                        var customerCodes = context.tblCustomers
                            .Select(c => c.CustomerCode)
                            .ToList();

                        var maxCustomerNumber = customerCodes
                            .Where(code => !string.IsNullOrEmpty(code))
                            .Select(code =>
                            {
                                var index = code.IndexOf('-');
                                if (index >= 0 && index + 1 < code.Length)
                                {
                                    var part = code.Substring(index + 1);

                                    return int.TryParse(part, out int number) ? number : 0;
                                }
                                return 0;
                            })
                            .DefaultIfEmpty(0)
                            .Max();

                        int nextCustomerNumber = maxCustomerNumber + 1;
                        MaxNo = "0000-" + nextCustomerNumber.ToString("D4");
                    }

                    tblCustomer tbl1 = new tblCustomer();
                    tbl1.CustomerName = obj.Name;
                    tbl1.CustomerCode = MaxNo;
                    tbl1.CustomerPhone = Convert.ToInt64(obj.MobileNumber);
                    tbl1.Position = "Guest";
                    tbl1.Status = "Active";
                    tbl1.CustomerEmail = obj.Email;
                    tbl1.UserID = obj.UserId;
                    var data1 = db.tblCustomers.Add(tbl1);
                    db.SaveChanges();

                    //tblMember tbl = new tblMember();
                    //tbl.Member_Name = obj.Name;
                    //tbl.Member_Mobile = obj.MobileNumber;
                    //tbl.CustomerID = data1.CustomerID;
                    //tbl.Member_Email = obj.Email;
                    //tbl.UserID = obj.UserId;
                    //tbl.Date = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                    //tbl.Status = "Active";
                    //var data = db.tblMembers.Add(tbl);
                    //db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AddIntroducer")]
        public JsonResponse AddIntroducer(AddIntroducerVM obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {
                var mobile = db.tblIntroducers.Where(x => x.Introducer_Mobile == obj.MobileNumber).FirstOrDefault();
                if (mobile != null)
                {
                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Mobile Number Already Exist" };
                }
                else
                {

                    var userdetails = db.tblCustomers.Where(x => x.CustomerID == obj.UserId).FirstOrDefault();
                    if (userdetails.Status == "Introducer")
                    {
                        var existentry = db.tblIntroducers.Where(x => x.SponsorID == obj.UserId).ToList();
                        if (existentry.Count() < 5)
                        {
                            string MaxNo;
                            using (var context = new db_zedPlusShopEntities())
                            {
                                var customerCodes = context.tblCustomers
                                    .Select(c => c.CustomerCode)
                                    .ToList();
                                var maxCustomerNumber = customerCodes
                                    .Where(code => !string.IsNullOrEmpty(code))
                                    .Select(code =>
                                    {
                                        var index = code.IndexOf('-');
                                        if (index >= 0 && index + 1 < code.Length)
                                        {
                                            var part = code.Substring(index + 1);
                                            return int.TryParse(part, out int number) ? number : 0;
                                        }
                                        return 0;
                                    })
                                    .DefaultIfEmpty(0)
                                    .Max();
                                int nextCustomerNumber = maxCustomerNumber + 1;
                                MaxNo = "0000-" + nextCustomerNumber.ToString("D4");
                            }

                            tblCustomer tbl1 = new tblCustomer();
                            tbl1.CustomerName = obj.Name;
                            tbl1.CustomerCode = MaxNo;
                            tbl1.CustomerPhone = Convert.ToInt64(obj.MobileNumber);
                            tbl1.CustomerEmail = obj.Email;
                            tbl1.UserID = obj.UserId;
                            tbl1.CityID = obj.City;
                            tbl1.StateID = obj.State;
                            tbl1.Status = "Active";
                            tbl1.Position = "Introducer";
                            tbl1.CustomerZIP = Convert.ToInt64(obj.PinCode);
                            var data1 = db.tblCustomers.Add(tbl1);
                            db.SaveChanges();


                            var sponsor = db.tblCustomers.Where(x => x.CustomerCode == obj.SponsorId).FirstOrDefault();
                            var praposer = db.tblCustomers.Where(x => x.CustomerCode == obj.PraposerID).FirstOrDefault();

                            tblIntroducer tbl = new tblIntroducer();
                            tbl.Introducer_Name = obj.Name;
                            tbl.Introducer_Mobile = obj.MobileNumber;
                            tbl.Introducer_Address = obj.Address;
                            tbl.CustomerID = tbl1.CustomerID;
                            tbl.Introducer_Email = obj.Email;
                            tbl.FatherName = obj.FatherName;
                            tbl.PinCode = obj.PinCode;
                            tbl.CountryId = obj.Country;
                            tbl.StateID = obj.State;
                            tbl.District = obj.District;
                            tbl.CityID = obj.City;
                            tbl.Date = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                            tbl.Status = "Active";
                            tbl.SponsorID = sponsor.CustomerID;
                            tbl.PraposerID = praposer.CustomerID;
                            var data = db.tblIntroducers.Add(tbl);
                            db.SaveChanges();
                        }
                        else
                        {
                            resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Maximum limit reached" };
                        }
                    }
                    else if (userdetails.Status == "Member")
                    {
                        var existentry = db.tblIntroducers.Where(x => x.SponsorID == obj.UserId).ToList();
                        if (existentry.Count() < 10)
                        {
                            string MaxNo;
                            using (var context = new db_zedPlusShopEntities())
                            {
                                var customerCodes = context.tblCustomers
                                    .Select(c => c.CustomerCode)
                                    .ToList();
                                var maxCustomerNumber = customerCodes
                                    .Where(code => !string.IsNullOrEmpty(code))
                                    .Select(code =>
                                    {
                                        var index = code.IndexOf('-');
                                        if (index >= 0 && index + 1 < code.Length)
                                        {
                                            var part = code.Substring(index + 1);
                                            return int.TryParse(part, out int number) ? number : 0;
                                        }
                                        return 0;
                                    })
                                    .DefaultIfEmpty(0)
                                    .Max();
                                int nextCustomerNumber = maxCustomerNumber + 1;
                                MaxNo = "0000-" + nextCustomerNumber.ToString("D4");
                            }

                            tblCustomer tbl1 = new tblCustomer();
                            tbl1.CustomerName = obj.Name;
                            tbl1.CustomerCode = MaxNo;
                            tbl1.CustomerPhone = Convert.ToInt64(obj.MobileNumber);
                            tbl1.CustomerEmail = obj.Email;
                            tbl1.UserID = obj.UserId;
                            tbl1.CityID = obj.City;
                            tbl1.StateID = obj.State;
                            tbl1.Status = "Active";
                            tbl1.Position = "Introducer";
                            tbl1.CustomerZIP = Convert.ToInt64(obj.PinCode);
                            var data1 = db.tblCustomers.Add(tbl1);
                            db.SaveChanges();


                            var sponsor = db.tblCustomers.Where(x => x.CustomerCode == obj.SponsorId).FirstOrDefault();
                            var praposer = db.tblCustomers.Where(x => x.CustomerCode == obj.PraposerID).FirstOrDefault();


                            tblIntroducer tbl = new tblIntroducer();
                            tbl.Introducer_Name = obj.Name;
                            tbl.Introducer_Mobile = obj.MobileNumber;
                            tbl.Introducer_Address = obj.Address;
                            tbl.CustomerID = tbl1.CustomerID;
                            tbl.Introducer_Email = obj.Email;
                            tbl.FatherName = obj.FatherName;
                            tbl.PinCode = obj.PinCode;
                            tbl.CountryId = obj.Country;
                            tbl.StateID = obj.State;
                            tbl.District = obj.District;
                            tbl.CityID = obj.City;
                            tbl.Date = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                            tbl.Status = "Active";
                            tbl.SponsorID = sponsor.CustomerID;
                            tbl.PraposerID = praposer.CustomerID;
                            var data = db.tblIntroducers.Add(tbl);
                            db.SaveChanges();
                        }
                        else
                        {
                            resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Maximum limit reached" };
                        }
                    }
                    else if (userdetails.Status == "Permanent Member")
                    {
                        var existentry = db.tblIntroducers.Where(x => x.SponsorID == obj.UserId).ToList();
                        if (existentry.Count() < 15)
                        {
                            string MaxNo;
                            using (var context = new db_zedPlusShopEntities())
                            {
                                var customerCodes = context.tblCustomers
                                    .Select(c => c.CustomerCode)
                                    .ToList();
                                var maxCustomerNumber = customerCodes
                                    .Where(code => !string.IsNullOrEmpty(code))
                                    .Select(code =>
                                    {
                                        var index = code.IndexOf('-');
                                        if (index >= 0 && index + 1 < code.Length)
                                        {
                                            var part = code.Substring(index + 1);
                                            return int.TryParse(part, out int number) ? number : 0;
                                        }
                                        return 0;
                                    })
                                    .DefaultIfEmpty(0)
                                    .Max();
                                int nextCustomerNumber = maxCustomerNumber + 1;
                                MaxNo = "0000-" + nextCustomerNumber.ToString("D4");
                            }

                            tblCustomer tbl1 = new tblCustomer();
                            tbl1.CustomerName = obj.Name;
                            tbl1.CustomerCode = MaxNo;
                            tbl1.CustomerPhone = Convert.ToInt64(obj.MobileNumber);
                            tbl1.CustomerEmail = obj.Email;
                            tbl1.UserID = obj.UserId;
                            tbl1.CityID = obj.City;
                            tbl1.StateID = obj.State;
                            tbl1.Status = "Active";
                            tbl1.Position = "Introducer";
                            tbl1.CustomerZIP = Convert.ToInt64(obj.PinCode);
                            var data1 = db.tblCustomers.Add(tbl1);
                            db.SaveChanges();


                            var sponsor = db.tblCustomers.Where(x => x.CustomerCode == obj.SponsorId).FirstOrDefault();
                            var praposer = db.tblCustomers.Where(x => x.CustomerCode == obj.PraposerID).FirstOrDefault();


                            tblIntroducer tbl = new tblIntroducer();
                            tbl.Introducer_Name = obj.Name;
                            tbl.Introducer_Mobile = obj.MobileNumber;
                            tbl.Introducer_Address = obj.Address;
                            tbl.CustomerID = tbl1.CustomerID;
                            tbl.Introducer_Email = obj.Email;
                            tbl.FatherName = obj.FatherName;
                            tbl.PinCode = obj.PinCode;
                            tbl.CountryId = obj.Country;
                            tbl.StateID = obj.State;
                            tbl.District = obj.District;
                            tbl.CityID = obj.City;
                            tbl.Date = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                            tbl.Status = "Active";
                            tbl.SponsorID = sponsor.CustomerID;
                            tbl.PraposerID = praposer.CustomerID;
                            var data = db.tblIntroducers.Add(tbl);
                            db.SaveChanges();
                        }
                        else
                        {
                            resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Maximum limit reached" };
                        }
                    }
                    else if (userdetails.Status == "Executive Member")
                    {
                        var existentry = db.tblIntroducers.Where(x => x.SponsorID == obj.UserId).ToList();
                        if (existentry.Count() < 20)
                        {
                            string MaxNo;
                            using (var context = new db_zedPlusShopEntities())
                            {
                                var customerCodes = context.tblCustomers
                                    .Select(c => c.CustomerCode)
                                    .ToList();
                                var maxCustomerNumber = customerCodes
                                    .Where(code => !string.IsNullOrEmpty(code))
                                    .Select(code =>
                                    {
                                        var index = code.IndexOf('-');
                                        if (index >= 0 && index + 1 < code.Length)
                                        {
                                            var part = code.Substring(index + 1);
                                            return int.TryParse(part, out int number) ? number : 0;
                                        }
                                        return 0;
                                    })
                                    .DefaultIfEmpty(0)
                                    .Max();
                                int nextCustomerNumber = maxCustomerNumber + 1;
                                MaxNo = "0000-" + nextCustomerNumber.ToString("D4");
                            }

                            tblCustomer tbl1 = new tblCustomer();
                            tbl1.CustomerName = obj.Name;
                            tbl1.CustomerCode = MaxNo;
                            tbl1.CustomerPhone = Convert.ToInt64(obj.MobileNumber);
                            tbl1.CustomerEmail = obj.Email;
                            tbl1.UserID = obj.UserId;
                            tbl1.CityID = obj.City;
                            tbl1.StateID = obj.State;
                            tbl1.Status = "Active";
                            tbl1.Position = "Introducer";
                            tbl1.CustomerZIP = Convert.ToInt64(obj.PinCode);
                            var data1 = db.tblCustomers.Add(tbl1);
                            db.SaveChanges();


                            var sponsor = db.tblCustomers.Where(x => x.CustomerCode == obj.SponsorId).FirstOrDefault();
                            var praposer = db.tblCustomers.Where(x => x.CustomerCode == obj.PraposerID).FirstOrDefault();


                            tblIntroducer tbl = new tblIntroducer();
                            tbl.Introducer_Name = obj.Name;
                            tbl.Introducer_Mobile = obj.MobileNumber;
                            tbl.Introducer_Address = obj.Address;
                            tbl.CustomerID = tbl1.CustomerID;
                            tbl.Introducer_Email = obj.Email;
                            tbl.FatherName = obj.FatherName;
                            tbl.PinCode = obj.PinCode;
                            tbl.CountryId = obj.Country;
                            tbl.StateID = obj.State;
                            tbl.District = obj.District;
                            tbl.CityID = obj.City;
                            tbl.Date = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                            tbl.Status = "Active";
                            tbl.SponsorID = sponsor.CustomerID;
                            tbl.PraposerID = praposer.CustomerID;
                            var data = db.tblIntroducers.Add(tbl);
                            db.SaveChanges();
                        }
                        else
                        {
                            resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Maximum limit reached" };
                        }
                    }
                    else if (userdetails.Status == "Royal Member")
                    {
                        var existentry = db.tblIntroducers.Where(x => x.SponsorID == obj.UserId).ToList();
                        if (existentry.Count() < 25)
                        {
                            string MaxNo;
                            using (var context = new db_zedPlusShopEntities())
                            {
                                var customerCodes = context.tblCustomers
                                    .Select(c => c.CustomerCode)
                                    .ToList();
                                var maxCustomerNumber = customerCodes
                                    .Where(code => !string.IsNullOrEmpty(code))
                                    .Select(code =>
                                    {
                                        var index = code.IndexOf('-');
                                        if (index >= 0 && index + 1 < code.Length)
                                        {
                                            var part = code.Substring(index + 1);
                                            return int.TryParse(part, out int number) ? number : 0;
                                        }
                                        return 0;
                                    })
                                    .DefaultIfEmpty(0)
                                    .Max();
                                int nextCustomerNumber = maxCustomerNumber + 1;
                                MaxNo = "0000-" + nextCustomerNumber.ToString("D4");
                            }

                            tblCustomer tbl1 = new tblCustomer();
                            tbl1.CustomerName = obj.Name;
                            tbl1.CustomerCode = MaxNo;
                            tbl1.CustomerPhone = Convert.ToInt64(obj.MobileNumber);
                            tbl1.CustomerEmail = obj.Email;
                            tbl1.UserID = obj.UserId;
                            tbl1.CityID = obj.City;
                            tbl1.StateID = obj.State;
                            tbl1.Status = "Active";
                            tbl1.Position = "Introducer";
                            tbl1.CustomerZIP = Convert.ToInt64(obj.PinCode);
                            var data1 = db.tblCustomers.Add(tbl1);
                            db.SaveChanges();


                            var sponsor = db.tblCustomers.Where(x => x.CustomerCode == obj.SponsorId).FirstOrDefault();
                            var praposer = db.tblCustomers.Where(x => x.CustomerCode == obj.PraposerID).FirstOrDefault();


                            tblIntroducer tbl = new tblIntroducer();
                            tbl.Introducer_Name = obj.Name;
                            tbl.Introducer_Mobile = obj.MobileNumber;
                            tbl.Introducer_Address = obj.Address;
                            tbl.CustomerID = tbl1.CustomerID;
                            tbl.Introducer_Email = obj.Email;
                            tbl.FatherName = obj.FatherName;
                            tbl.PinCode = obj.PinCode;
                            tbl.CountryId = obj.Country;
                            tbl.StateID = obj.State;
                            tbl.District = obj.District;
                            tbl.CityID = obj.City;
                            tbl.Date = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                            tbl.Status = "Active";
                            tbl.SponsorID = sponsor.CustomerID;
                            tbl.PraposerID = praposer.CustomerID;
                            var data = db.tblIntroducers.Add(tbl);
                            db.SaveChanges();
                        }
                        else
                        {
                            resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Maximum limit reached" };
                        }
                    }
                    else
                    {
                        resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Minimum 1200 PV Requried" };
                    }
                }
            }
            catch (Exception ex)
            {
                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("api/AddMember")]
        //public JsonResponse AddMember(AddMemberVM obj)
        //{
        //    db_ZedPlusShopEntities db = new db_ZedPlusShopEntities();
        //    JsonResponse resp = new JsonResponse();
        //    try
        //    {
        //
        //        var mobile = db.tblMembers.Where(x => x.Member_Mobile == obj.MobileNumber).FirstOrDefault();
        //        if (mobile != null)
        //        {
        //            resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Mobile Number Already Exit" };
        //        }
        //        else
        //        {
        //            // var mobil = db.tblOrders.Where(x => x.CustomerID == (Userid)).ToList();
        //            // double sum = 0;
        //            //foreach (var x in mobil) 
        //            // {
        //            //     try
        //            //     {
        //            //         sum += Convert.ToDouble(x.TotalPV);
        //            //     }catch { }
        //            // }
        //            // if (sum >= 3000)
        //            // {
        //            tblCustomer tbl1 = new tblCustomer();
        //            tbl1.CustomerName = obj.Name;
        //            tbl1.CustomerPhone = Convert.ToInt64(obj.MobileNumber);
        //            tbl1.CustomerEmail = obj.Email;
        //            tbl1.UserID = obj.Userid;
        //            tbl1.CityID = obj.City;
        //            tbl1.StateID = obj.State;
        //            tbl1.Status = "Member";
        //            tbl1.CustomerZIP = Convert.ToInt64(obj.PinCode);
        //            var data1 = db.tblCustomers.Add(tbl1);
        //            db.SaveChanges();
        //            tblMember tbl = new tblMember();
        //            tbl.Member_Name = obj.Name;
        //            tbl.Member_Mobile = obj.MobileNumber;
        //            tbl.Member_Email = obj.Email;
        //            tbl.FatherName = obj.FatherName;
        //            tbl.PinCode = obj.PinCode;
        //            tbl.CountryId = obj.Country;
        //            tbl.StateID = obj.State;
        //            tbl.District = obj.District;
        //            tbl.CityID = obj.City;
        //            tbl.UserID = obj.Userid;
        //            tbl.Member_Address = obj.Address;
        //            tbl.Date = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
        //            tbl.Status = "Member";
        //            var data = db.tblMembers.Add(tbl);
        //            db.SaveChanges();
        //            //}
        //            //else { resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Your Point Value LessThan 3000 please Increase Your Point " }; }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
        //    }
        //    return resp;
        //}


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetLevel")]
        public GetLevelResponse GetLevel(int CustomerId)
        {
            GetLevelResponse resp = new GetLevelResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetLevelVM> mdl1 = new List<GetLevelVM>();
                var result = (from tbl in db.tblCustomers
                              join tbla in db.tblCustomers on tbl.UserID equals tbla.CustomerID into a
                              from tbla in a.DefaultIfEmpty()
                              where tbl.CustomerID == CustomerId
                              select new
                              {
                                  tbl.CustomerID,
                                  tbl.CustomerName,
                              }).ToList();
                if (result.Count() > 0)
                {

                    foreach (var list in result)
                    {
                        int memberCount = 1;
                        mdl1.Add(new GetLevelVM
                        {
                            MemberCount = memberCount,
                            LevelName = "Level - 1",
                        });
                        memberCount = CountMembers(list.CustomerID, 1, mdl1);
                    }
                    resp = new GetLevelResponse { LevelList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetLevelResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetLevelResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


        private int CountMembers(long? userId, int currentLevel, List<GetLevelVM> levelList)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            int count = 0;

            var members = from tbl in db.tblCustomers
                          where tbl.UserID == userId
                          select tbl;
            foreach (var member in members)
            {
                count++;

                var levelName = $"Level - {currentLevel + 1}";
                var existingLevel = levelList.FirstOrDefault(x => x.LevelName == levelName);

                if (existingLevel != null)
                {
                    existingLevel.MemberCount++;
                }
                else
                {
                    levelList.Add(new GetLevelVM
                    {
                        MemberCount = 1,
                        LevelName = levelName
                    });
                }

                count += CountMembers(member.CustomerID, currentLevel + 1, levelList);
            }

            return count;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetLevelMembers")]
        public GetLevelMembersResponse GetLevelMembers(int CustomerId, int Level)
        {
            GetLevelMembersResponse resp = new GetLevelMembersResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetLevelMembersVM> mdl1 = new List<GetLevelMembersVM>();

                var result = (from tbl in db.tblCustomers
                              join tblA in db.tblStateMasters on tbl.StateID equals tblA.StateID into tbla
                              from tblA in tbla.DefaultIfEmpty()
                              join tblB in db.tblCityMasters on tbl.CityID equals tblB.CityID into tblb
                              from tblB in tblb.DefaultIfEmpty()
                              where tbl.CustomerID == CustomerId
                              select new
                              {
                                  tbl.CustomerID,
                                  tbl.CustomerName,
                                  tbl.CustomerCode,
                                  tbl.CustomerZIP,
                                  tbl.Position,
                                  tblA.State_Name,
                                  tblB.City_Name
                              }).ToList();
                if (result.Count() > 0)
                {

                    foreach (var list in result)
                    {
                        int totalpv = 0;
                        int newlevel = 1;
                        if (Level == 1)
                        {
                            var memberCount = db.tblOrders.Where(x => x.CustomerID == list.CustomerID).ToList();
                            foreach (var member in memberCount)
                            {
                                totalpv += Convert.ToInt32(member.TotalPV);
                            }
                            mdl1.Add(new GetLevelMembersVM
                            {
                                MemeberName = list.CustomerName,
                                TotalPV = totalpv,
                                CustomerID=list.CustomerID,
                                CustomerCode=list.CustomerCode,
                                Pincode = list.CustomerZIP != null ? (long)list.CustomerZIP : 0,
                                Position = list.Position,
                                State=list.State_Name,
                                City=list.City_Name
                            });
                        }
                        else
                        {
                            //for (int i = 0; i < Level; i++)
                            //{
                            string cstid = Convert.ToString(list.CustomerID);
                            //    newlevel++;
                            var GetMember = GetMembers(cstid, Level, mdl1, newlevel);
                            //}
                        }
                    }
                    resp = new GetLevelMembersResponse { LevelMemebersList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetLevelMembersResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetLevelMembersResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


        private int GetMembers(string userId, int currentLevel, List<GetLevelMembersVM> levelList, int newlevel)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();

            int j = 0;
            int count = 0;
            string mems = "";
            levelList.Clear();
            if (userId.Contains(','))
            {
                List<string> userIds = userId.Split(',')
                           .Select(id => id.Trim())
                           .ToList();
                foreach (var id in userIds)
                {
                    int i = 0;
                    j++;
                    if (id != "")
                    {
                        long? usid = Convert.ToInt64(id);
                        var members = from tbl in db.tblCustomers
                                      where tbl.UserID == usid
                                      select new { tbl.CustomerID, tbl.CustomerName };
                        if (members.Count() > 0)
                        {
                            foreach (var member in members)
                            {
                                i++;
                                int totalpv = 0;
                                if (i <= members.Count())
                                {
                                    mems += member.CustomerID + ",";
                                }

                                if (newlevel == currentLevel)
                                {
                                    List<string> custIds = mems.Split(',')
                               .Select(memid => memid.Trim())
                               .ToList();
                                    foreach (var memid in custIds)
                                    {
                                        if (memid != "")
                                        {
                                            long? memsid = Convert.ToInt64(memid);
                                            var memberCount = db.tblOrders.Where(x => x.CustomerID == memsid).ToList();
                                            var membername = db.tblCustomers.FirstOrDefault(x => x.CustomerID == memsid);
                                            foreach (var member1 in memberCount)
                                            {
                                                totalpv += Convert.ToInt32(member1.TotalPV);
                                            }
                                            levelList.Add(new GetLevelMembersVM
                                            {
                                                MemeberName = membername.CustomerName,
                                                CustomerID = membername.CustomerID,
                                                CustomerCode = membername.CustomerCode,
                                                State = db.tblStateMasters
                                                           .Where(x => x.StateID == membername.StateID)
                                                           .Select(x => x.State_Name)
                                                           .FirstOrDefault() ?? "",
                                                City = db.tblCityMasters
                                                           .Where(x => x.CityID == membername.CityID)
                                                           .Select(x => x.City_Name)
                                                           .FirstOrDefault() ?? "",


                                                Pincode = membername.CustomerZIP != null ? (long)membername.CustomerZIP : 0,
                                                Position = membername.Position,
                                                TotalPV = totalpv,
                                            });
                                        }
                                    }
                                }
                                else
                                {
                                    if (j == userIds.Count())
                                    {
                                        if (i == members.Count())
                                        {
                                            newlevel++;
                                        }
                                        if (newlevel == currentLevel)
                                        {
                                            if (i == members.Count())
                                            {
                                                List<string> custIds = mems.Split(',')
                                   .Select(memid => memid.Trim())
                                   .ToList();
                                                foreach (var memid in custIds)
                                                {
                                                    if (memid != "")
                                                    {
                                                        long? memsid = Convert.ToInt64(memid);
                                                        var memberCount = db.tblOrders.Where(x => x.CustomerID == memsid).ToList();
                                                        var membername = db.tblCustomers.FirstOrDefault(x => x.CustomerID == memsid);
                                                        foreach (var member1 in memberCount)
                                                        {
                                                            totalpv += Convert.ToInt32(member1.TotalPV);
                                                        }
                                                        levelList.Add(new GetLevelMembersVM
                                                        {
                                                            MemeberName = membername.CustomerName,
                                                            CustomerID = membername.CustomerID,
                                                            CustomerCode = membername.CustomerCode,
                                                            Pincode = membername.CustomerZIP != null ? (long)membername.CustomerZIP : 0,
                                                            State = db.tblStateMasters
                                                           .Where(x => x.StateID == membername.StateID)
                                                           .Select(x => x.State_Name)
                                                           .FirstOrDefault() ?? "",
                                                            City = db.tblCityMasters
                                                           .Where(x => x.CityID == membername.CityID)
                                                           .Select(x => x.City_Name)
                                                           .FirstOrDefault() ?? "",
                                                            Position = membername.Position,
                                                            TotalPV = totalpv,
                                                        });
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (i == members.Count())
                                            {
                                                count += GetMembers(mems, currentLevel, levelList, newlevel);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (mems != "")
                            {
                                if (j == userIds.Count())
                                {
                                    if (i <= members.Count())
                                    {
                                        if (i == members.Count())
                                        {
                                            newlevel++;
                                        }
                                        if (newlevel == currentLevel)
                                        {
                                            if (i == members.Count())
                                            {
                                                List<string> custIds = mems.Split(',')
                                   .Select(memid => memid.Trim())
                                   .ToList();
                                                foreach (var memid in custIds)
                                                {
                                                    int totalpv = 0;
                                                    if (memid != "")
                                                    {
                                                        long? memsid = Convert.ToInt64(memid);
                                                        var memberCount = db.tblOrders.Where(x => x.CustomerID == memsid).ToList();
                                                        var membername = db.tblCustomers.FirstOrDefault(x => x.CustomerID == memsid);
                                                        foreach (var member1 in memberCount)
                                                        {
                                                            totalpv += Convert.ToInt32(member1.TotalPV);
                                                        }
                                                        levelList.Add(new GetLevelMembersVM
                                                        {
                                                            MemeberName = membername.CustomerName,
                                                            CustomerID = membername.CustomerID,
                                                            CustomerCode = membername.CustomerCode,
                                                            Pincode = membername.CustomerZIP != null ? (long)membername.CustomerZIP : 0,
                                                            State = db.tblStateMasters
                                                           .Where(x => x.StateID == membername.StateID)
                                                           .Select(x => x.State_Name)
                                                           .FirstOrDefault() ?? "",
                                                            City = db.tblCityMasters
                                                           .Where(x => x.CityID == membername.CityID)
                                                           .Select(x => x.City_Name)
                                                           .FirstOrDefault() ?? "",
                                                            Position = membername.Position,

                                                            TotalPV = totalpv,
                                                        });
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (i == members.Count())
                                            {
                                                count += GetMembers(mems, currentLevel, levelList, newlevel);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (mems != "")
                        {
                            if (j == userIds.Count())
                            {

                                newlevel++;
                                if (newlevel == currentLevel)
                                {

                                    List<string> custIds = mems.Split(',')
                       .Select(memid => memid.Trim())
                       .ToList();
                                    foreach (var memid in custIds)
                                    {
                                        int totalpv = 0;
                                        if (memid != "")
                                        {
                                            long? memsid = Convert.ToInt64(memid);
                                            var memberCount = db.tblOrders.Where(x => x.CustomerID == memsid).ToList();
                                            var membername = db.tblCustomers.FirstOrDefault(x => x.CustomerID == memsid);
                                            foreach (var member1 in memberCount)
                                            {
                                                totalpv += Convert.ToInt32(member1.TotalPV);
                                            }
                                            levelList.Add(new GetLevelMembersVM
                                            {
                                                MemeberName = membername.CustomerName,
                                                CustomerID = membername.CustomerID,
                                                CustomerCode = membername.CustomerCode,
                                                Pincode = membername.CustomerZIP != null ? (long)membername.CustomerZIP : 0,
                                                State = db.tblStateMasters
                                                           .Where(x => x.StateID == membername.StateID)
                                                           .Select(x => x.State_Name)
                                                           .FirstOrDefault() ?? "",
                                                City = db.tblCityMasters
                                                           .Where(x => x.CityID == membername.CityID)
                                                           .Select(x => x.City_Name)
                                                           .FirstOrDefault() ?? "",
                                                Position = membername.Position,
                                                TotalPV = totalpv,
                                            });
                                        }
                                    }

                                }
                                else
                                {

                                    count += GetMembers(mems, currentLevel, levelList, newlevel);

                                }

                            }
                        }
                    }
                }
            }
            else
            {
                int i = 0;
                int k = 0;
                long? uid = Convert.ToInt64(userId);
                var members = from tbl in db.tblCustomers
                              where tbl.UserID == uid
                              select tbl;
                if (members.Count() > 0)
                {
                    k++;
                    foreach (var member in members)
                    {
                        i++;
                        int totalpv = 0;
                        if (i < members.Count())
                        {
                            mems += member.CustomerID + ",";
                        }
                        else { mems += member.CustomerID; }
                        if (newlevel == currentLevel)
                        {
                            var memberCount = db.tblOrders.Where(x => x.CustomerID == member.CustomerID).ToList();
                            foreach (var member1 in memberCount)
                            {
                                totalpv += Convert.ToInt32(member1.TotalPV);
                            }
                            levelList.Add(new GetLevelMembersVM
                            {
                                MemeberName = member.CustomerName,
                                CustomerID = member.CustomerID,
                                CustomerCode = member.CustomerCode,
                                Pincode = member.CustomerZIP != null ? (long)member.CustomerZIP : 0,
                                State = db.tblStateMasters
                                                           .Where(x => x.StateID == member.StateID)
                                                           .Select(x => x.State_Name)
                                                           .FirstOrDefault() ?? "",
                                City = db.tblCityMasters
                                                           .Where(x => x.CityID == member.CityID)
                                                           .Select(x => x.City_Name)
                                                           .FirstOrDefault() ?? "",
                                Position = member.Position,
                                TotalPV = totalpv,
                            });
                        }
                        else
                        {

                            if (i == members.Count())
                            {
                                newlevel++;
                                if (newlevel == currentLevel)
                                {
                                    List<string> custIds = mems.Split(',')
                                   .Select(memid => memid.Trim())
                                   .ToList();
                                    foreach (var memid in custIds)
                                    {

                                        if (memid != "")
                                        {
                                            long? memsid = Convert.ToInt64(memid);
                                            var memberCount = db.tblOrders.Where(x => x.CustomerID == memsid).ToList();
                                            var membername = db.tblCustomers.FirstOrDefault(x => x.CustomerID == memsid);
                                            foreach (var member1 in memberCount)
                                            {
                                                totalpv += Convert.ToInt32(member1.TotalPV);
                                            }
                                            levelList.Add(new GetLevelMembersVM
                                            {
                                                MemeberName = membername.CustomerName,
                                                CustomerID = membername.CustomerID,
                                                CustomerCode = membername.CustomerCode,
                                                Pincode = membername.CustomerZIP != null ? (long)membername.CustomerZIP : 0,
                                                State = db.tblStateMasters
                                                           .Where(x => x.StateID == membername.StateID)
                                                           .Select(x => x.State_Name)
                                                           .FirstOrDefault() ?? "",
                                                City = db.tblCityMasters
                                                           .Where(x => x.CityID == membername.CityID)
                                                           .Select(x => x.City_Name)
                                                           .FirstOrDefault() ?? "",
                                                Position = membername.Position,
                                                TotalPV = totalpv,
                                            });
                                        }
                                    }
                                }
                                else
                                {
                                    count += GetMembers(mems, currentLevel, levelList, newlevel);
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/UpdateAnIntroducer")]
        public JsonResponse UpdateAnIntroducer(UpdateAnIntroducerVM obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {
                var dataIntro = db.tblIntroducers.Where(x => x.CustomerID == obj.CustomerId).FirstOrDefault();
                var dataCust  = db.tblCustomers.Where(x => x.CustomerID   == obj.CustomerId).FirstOrDefault();
                var sponsor   = db.tblCustomers.Where(x => x.CustomerCode == obj.SponsorID).FirstOrDefault();
                var praposer  = db.tblCustomers.Where(x => x.CustomerCode == obj.PraposerID).FirstOrDefault();
                if (dataIntro != null)
                {
                    var tbl = db.tblIntroducers.Where(x => x.CustomerID == obj.CustomerId).FirstOrDefault();
                    tbl.Introducer_Name = obj.Name;
                    tbl.Introducer_Mobile = obj.MobileNumber;
                    tbl.Introducer_Address = obj.Address;
                    tbl.PinCode = obj.Pincode;
                    tbl.FatherName = obj.FatherName;
                    tbl.CountryId = obj.Country;
                    tbl.StateID = obj.State;
                    tbl.District = obj.District;
                    tbl.CityID = obj.City;
                    tbl.CustomerID = obj.CustomerId;
                    tbl.Status = "Active";
                    if (dataIntro.SponsorID == null)
                    {
                        tbl.SponsorID = sponsor.CustomerID;
                    }
                    if (dataIntro.PraposerID == null)
                    {
                        tbl.PraposerID = praposer.CustomerID;
                    }
                    db.SaveChanges();
                }
                else
                {
                    try
                    {
                        var tbl2 = db.tblCustomers.Where(x => x.CustomerID == obj.CustomerId).FirstOrDefault();
                        if (tbl2 != null)
                        {
                            tbl2.UserID = sponsor.CustomerID;
                            tbl2.Position = "Introducer";
                            db.SaveChanges();
                        }
                    }
                    catch { }

                    tblIntroducer tbl = new tblIntroducer();
                    tbl.Introducer_Name = obj.Name;
                    tbl.Introducer_Mobile = obj.MobileNumber;
                    tbl.Introducer_Address = obj.Address;
                    tbl.PinCode = obj.Pincode;
                    tbl.FatherName = obj.FatherName;
                    tbl.CountryId = obj.Country;
                    tbl.StateID = obj.State;
                    tbl.District = obj.District;
                    tbl.CityID = obj.City;
                    tbl.CustomerID = obj.CustomerId;
                    tbl.Status = "Active";
                    tbl.SponsorID = sponsor.CustomerID;
                    tbl.PraposerID = praposer.CustomerID;
                    tbl.Date = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                    var data = db.tblIntroducers.Add(tbl);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetIntroducerList")]
        public GetIntroducerListResponse GetIntroducerList(int CustomerId)
        {
            GetIntroducerListResponse resp = new GetIntroducerListResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<GetIntroducerListVM> mdl1 = new List<GetIntroducerListVM>();
                var result = (from tbl in db.tblIntroducers
                              join tbla in db.tblCustomers on tbl.CustomerID equals tbla.CustomerID into a
                              from tbla in a.DefaultIfEmpty()
                              join tblb in db.tblCustomers on tbl.SponsorID equals tblb.CustomerID into b
                              from tblb in b.DefaultIfEmpty()
                              join tblc in db.tblCustomers on tbl.PraposerID equals tblc.CustomerID into c
                              from tblc in c.DefaultIfEmpty()
                              join tbld in db.tblCountryMasters on tbl.CountryId equals tbld.ID into d
                              from tbld in d.DefaultIfEmpty()
                              join tble in db.tblStateMasters on tbl.StateID equals tble.StateID into e
                              from tble in e.DefaultIfEmpty()
                              join tblf in db.tblDistrictMasters on tbl.District equals tblf.ID into f
                              from tblf in f.DefaultIfEmpty()
                              join tblg in db.tblCityMasters on tbl.CityID equals tblg.CityID into g
                              from tblg in g.DefaultIfEmpty()
                              where tbl.CustomerID == CustomerId
                              select new
                              {
                                  tbl.ID,
                                  tbl.Introducer_Name,
                                  tbl.Introducer_Email,
                                  tbl.Introducer_Mobile,
                                  tbl.Date,
                                  tbl.Status,
                                  tbl.FatherName,
                                  tbl.PinCode,
                                  tbld.Country_Name,
                                  tble.State_Name,
                                  tblf.DistrictName,
                                  tblg.City_Name,
                                  tbl.Introducer_Address,
                                  CustomerCode=tbla.CustomerCode,
                                  PraposerName=tblc.CustomerName,
                                  PraposerCode=tblc.CustomerCode,
                                  SponserName=tblb.CustomerName,
                                  SponserCode=tblb.CustomerCode
                              }).ToList();
                if (result.Count() > 0)
                {

                    foreach (var list in result)
                    {                        
                        mdl1.Add(new GetIntroducerListVM
                        {
                            IntroducerID = list.ID,
                            IntroducerName =list.Introducer_Name ,
                            IntroducerEmail = list.Introducer_Email,
                            IntroducerMobile = list.Introducer_Mobile,
                            IntroducerAddress = list.Introducer_Address,
                            FatherName = list.FatherName,                                                                                   
                            CustomerCode = list.CustomerCode,     
                            SponserName=list.SponserName,
                            SponserCode=list.SponserCode,
                            PraposerName=list.PraposerName,
                            PraposerCode=list.PraposerCode,
                            Pincode = list.PinCode,
                            Country=list.Country_Name,
                            State=list.State_Name,
                            District=list.DistrictName,
                            City=list.City_Name
                        });                        
                    }
                    resp = new GetIntroducerListResponse { GetIntroducerListVM = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetIntroducerListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetIntroducerListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetCustomerDetails")]
        public GetCustomerDetailsResponse GetCustomerDetails(string CustomerCode)
        {
            GetCustomerDetailsResponse resp = new GetCustomerDetailsResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();               
                GetCutomerDetailsVM model = new GetCutomerDetailsVM();
                var result = db.tblCustomers.Where(x => x.CustomerCode == CustomerCode).FirstOrDefault();
                if (result != null)
                {
                  
                    model.CustomerID = result.CustomerID;
                    model.CustomerCode = result.CustomerCode;
                    model.CustomerName = result.CustomerName;
                    model.CustomerMobileNumber = Convert.ToString(result.CustomerPhone);

                    resp = new GetCustomerDetailsResponse { getcustomerdetails = model };
                    return resp;
                }
                else
                {
                    resp = new GetCustomerDetailsResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetCustomerDetailsResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }


    }
}