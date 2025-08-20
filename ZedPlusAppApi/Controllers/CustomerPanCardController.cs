using System;
using System.Linq;
using System.Web.Http;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class CustomerPanCardController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CustomerPanCard")]

        public JsonResponse AddCustomerPanCard(tblCustomerPanCard ObjCustomerPanCard)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();

            try
            {
                var res = db.tblCustomerPanCards.FirstOrDefault(x => x.CustomerID == ObjCustomerPanCard.CustomerID);
                if (res == null)
                {
                    tblCustomerPanCard tblCustomerPanCard = new tblCustomerPanCard();

                    tblCustomerPanCard.PanCardNumber = ObjCustomerPanCard.PanCardNumber;
                    tblCustomerPanCard.CustomerID = ObjCustomerPanCard.CustomerID;
                    tblCustomerPanCard.PanCardImage = ObjCustomerPanCard.PanCardImage;
                    tblCustomerPanCard.Status = ObjCustomerPanCard.Status;
                    tblCustomerPanCard.DateTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                    var data = db.tblCustomerPanCards.Add(tblCustomerPanCard);
                    db.SaveChanges();

                    if (tblCustomerPanCard.ID > 0)
                    {
                        resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Successfully Add PanCard Detail" };
                    }
                    else
                    {
                        resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Something went wrong.Please try again." };
                    }
                }
                else
                {
                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Already Exist" };
                }
            }

            catch (Exception ex)
            {
                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }

            return resp;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPancardDetails")]
        public GetPanCardResponse GetPancardDetails(int CustomerId)
        {
            GetPanCardResponse resp = new GetPanCardResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                GetPanCardVM mdl1 = new GetPanCardVM();

                var result = (from tbl in db.tblCustomerPanCards
                              join tbla in db.tblCustomers on tbl.CustomerID equals tbla.CustomerID into a
                              from tbla in a.DefaultIfEmpty()
                              where  tbl.CustomerID == CustomerId

                              select new
                              {
                                  tbl.ID,
                                  tbla.CustomerName,
                                  tbl.PanCardNumber,
                                  tbl.PanCardImage,
                                  tbl.Status,
                                  tbl.DateTime
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        mdl1.Id = list.ID;
                        mdl1.CustomerName  = list.CustomerName;
                        mdl1.PanCardNumber = list.PanCardNumber;
                        mdl1.PanCardImage  = list.PanCardImage;
                        mdl1.Status = list.Status;
                        mdl1.DateTime = list.DateTime;
                    }
                    resp = new GetPanCardResponse { PanCard = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new GetPanCardResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new GetPanCardResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }
    }
}