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
    public class CartController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/AddToCart")]
        public JsonResponse AddToCart(AddToCartVM obj)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {
                var res = db.tblCarts.FirstOrDefault(x => x.ItemID == obj.ProductId && x.CustomerID == obj.CustomerId && x.Status == "Active" && x.SizeID == obj.SizeId && x.VarientId==obj.VarientId);
                if (res != null)
                {
                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Already Exist In Your Cart !" };
                }
                else
                {
                    tblCart tbl = new tblCart();
                    tbl.CustomerID = obj.CustomerId;
                    tbl.ItemID = obj.ProductId;
                    tbl.SizeID = obj.SizeId;
                    tbl.VarientId = obj.VarientId;
                    tbl.Quantity = obj.Quantity;
                    tbl.TotalPice = Convert.ToInt64(obj.TotalAmount);
                    tbl.Date = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                    tbl.Status = "Active";
                    var data = db.tblCarts.Add(tbl);
                    db.SaveChanges();
                    if (tbl.CartID > 0)
                    {
                        resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Item Added to Cart Successfully" };
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
        [System.Web.Http.Route("api/GetCartList")]
        public CartResponse GetCartList(int CustomerId)
        {
            CartResponse resp = new CartResponse();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                List<CartVM> mdl1 = new List<CartVM>();



                var result = (from tbl in db.tblCarts
                              join tbla in db.tblCustomers on tbl.CustomerID equals tbla.CustomerID into a
                              from tbla in a.DefaultIfEmpty()
                              join tblb in db.tblProducts on tbl.ItemID equals tblb.ID into b
                              from tblb in b.DefaultIfEmpty()
                              join tblc in db.tblSizeMasters on tbl.SizeID equals tblc.ID into c
                              from tblc in c.DefaultIfEmpty()
                              //join tbld in db.tblColorMasters on tblb.ColorID equals tbld.ID into d
                              //from tbld in d.DefaultIfEmpty()
                              where tbl.Status == "Active" && tbl.CustomerID == CustomerId
                              
                              select new
                              {
                                  tbl.CartID,
                                  tbla.CustomerName,
                                  tbl.ItemID,
                                  tblb.Item_Name,
                                  ProductID=tblb.ID,
                                  //tbld.ImageURL,
                               //   tbld.Color_Name,
                                  tblb.NRP,
                                  tblb.PV,
                                  tbl.TotalPice,
                                  tbl.Quantity,
                                  tbl.Date,
                                  tblc.Size_Name,
                                  tbl.Status
                              }).ToList();
                if (result.Count() > 0)
                {
                    foreach (var list in result)
                    {
                        string firstImagePath = "";
                        var primaryVariant = db.ProductVariants
                                               .FirstOrDefault(x => x.ProductID == list.ProductID && x.IsPrimary == true);

                        if (primaryVariant != null)
                        {
                            var variantImage = db.tblVaraintsimages
                                                 .FirstOrDefault(x => x.VariantID == primaryVariant.VariantID);

                            if (variantImage != null && !string.IsNullOrEmpty(variantImage.ImageUrl))
                            {
                                firstImagePath = variantImage.ImageUrl.Split(',')[0];
                            }
                        }
                        mdl1.Add(new CartVM
                        {
                            Id = list.CartID,
                            CustomerName = list.CustomerName,
                            ProductName = list.Item_Name,
                            ProductImage = firstImagePath,
                         //   ProductColor = list.Color_Name,
                            ProductPrice = list.NRP.ToString(),
                            ProductSize = list.Size_Name,
                            TotalPrice  = Convert.ToString(list.TotalPice),
                       //     Quantity = list.Quantity,
                            Date = list.Date,
                            Status = list.Status,
                            ProductId = Convert.ToInt32(list.ItemID),
                            ItemPV = list.PV
                        });
                    }
                    resp = new CartResponse { CartList = mdl1 };
                    return resp;
                }
                else
                {
                    resp = new CartResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                }
            }
            catch (Exception ex)
            {
                resp = new CartResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CountAddToCart")]
        public JsonResponse CountAddToCart(int CartId , int Qty, int TotalAmount)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {

                tblCart tbl = db.tblCarts.FirstOrDefault(p => p.CartID == CartId);
                if (tbl != null)
                {
                  //  tbl.Quantity = Convert.ToString(Qty);
                    tbl.TotalPice = Convert.ToInt64(TotalAmount);
                    tbl.Date = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"); 
                    tbl.Status = "Active";
                    db.Entry(tbl).State = EntityState.Modified;
                    db.SaveChanges();
                    resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Purchase Count Updated Successfully" };
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
        [System.Web.Http.Route("api/RemoveFromCart")]
        public JsonResponse RemoveFromCart(int CartId)
        {
            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
            JsonResponse resp = new JsonResponse();
            try
            {

                tblCart tbl = db.tblCarts.FirstOrDefault(p => p.CartID == CartId);
                if (tbl != null)
                {
                  //  tbl.Quantity = "0";
                    tbl.TotalPice = 0;
                    tbl.Date = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
                    tbl.Status = "DeActive";
                    db.Entry(tbl).State = EntityState.Modified;
                    db.SaveChanges();
                    resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Product Removed From Cart " };
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