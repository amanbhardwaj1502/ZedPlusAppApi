using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class RegistrationController : ApiController
    {


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/UserRegistration")]
        public JsonResponse UserRegistration(tblCustomer obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {


                var mobile = db.tblCustomers.Where(x => x.CustomerPhone == obj.CustomerPhone).FirstOrDefault();
                if (mobile != null)
                {
                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Mobile Number Already Exit" };
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

                    tblCustomer tbl = new tblCustomer();
                    tbl.CustomerName = obj.CustomerName;
                    tbl.CustomerCode = MaxNo;
                    tbl.CustomerPhone = obj.CustomerPhone;
                    tbl.CustomerEmail = obj.CustomerEmail;
                    tbl.Status = "Active";
                    tbl.Position = "Guest";
                    var data = db.tblCustomers.Add(tbl);
                    db.SaveChanges();

                }
            }
            catch(Exception ex)
            {
                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}