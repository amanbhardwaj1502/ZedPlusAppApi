//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Http;
//using System.Web.Mvc;
//using ZedPlusAppApi.Models;

//namespace ZedPlusAppApi.Controllers
//{
//    public class WishListController : ApiController
//    {
       
//            [System.Web.Http.HttpPost]
//            [System.Web.Http.Route("api/AddToWishList")]
//            public JsonResponse AddToWishList(WishListVM obj)
//            {
//            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
//            JsonResponse resp = new JsonResponse();
//                try
//                {
//                    var res = db.tblwishlists.FirstOrDefault(x => x.product_id == obj.ProductId && x.user_id == obj.UserId );
//                    if (res != null)
//                    {
//                        resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Already Exist In Your Wish List !" };
//                    }
//                    else
//                    {
//                        tblwishlist tbl = new tblwishlist();
//                        tbl.user_id = obj.UserId;
//                        tbl.product_id = obj.ProductId;
//                        tbl.added_at = DateTime.Now;

                   

//                      var data = db.tblwishlists.Add(tbl);
//                        db.SaveChanges();
//                        if (tbl.id > 0)
//                        {
//                            resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Item Added to Wish List Successfully" };
//                        }
//                        else
//                        {
//                            resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Something went wrong.Please try again." };
//                        }
//                    }

//                }
//                catch (Exception ex)
//                {
//                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
//                }
//                return resp;
//            }


//        [System.Web.Http.HttpPost]
//        [System.Web.Http.Route("api/RemoveFromWishList")]
//        public JsonResponse RemoveFromWishList(WishListVM obj)
//        {
//            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
//            JsonResponse resp = new JsonResponse();
//            try
//            {
//                var res = db.tblwishlists.FirstOrDefault(x => x.product_id == obj.ProductId && x.user_id == obj.UserId);
//                if (res != null)
//                {
//                    db.tblwishlists.Remove(res);
//                    db.SaveChanges();
//                    resp = new JsonResponse
//                    {
//                        Status_Code = "200",
//                        Status = "Success",
//                        Message = "Item Removed from Wish List Successfully"
//                    };
//                }
//                else
//                {
//                    resp = new JsonResponse
//                    {
//                        Status_Code = "0",
//                        Status = "error",
//                        Message = "Item not found in your Wish List"
//                    };
//                }
//            }
//            catch (Exception ex)
//            {
//                resp = new JsonResponse
//                {
//                    Status_Code = "0",
//                    Status = "error",
//                    Message = ex.Message
//                };
//            }
//            return resp;
//        }


//        [System.Web.Http.HttpGet]
//        [System.Web.Http.Route("api/GetFromWishList")]
//        public GetWishListResponse GetFromWishList(int userid)
//        {
//            GetWishListResponse resp = new GetWishListResponse();
//            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
//            List<WishListGetVM> mdl = new List<WishListGetVM>();

//            try
//            {
//                var result = (from tbl in db.tblwishlists
//                              join tbla in db.tblUserMasters on tbl.user_id equals tbla.UserID into a
//                              from tbla in a.DefaultIfEmpty()
//                              join tblb in db.tblProducts on tbl.product_id equals tblb.ID into b
//                              from tblb in b.DefaultIfEmpty()
//                              where tbl.user_id == userid
//                              select new
//                              {
//                                  tbl.id,
//                                  UserID = (long?)tbla.UserID,
//                                  tbla.User_Name,
//                                //  tbl.product_id,
//                                  tblb.Item_Name,
//                                  ProductID = (int?)tblb.ID,
//                                  tbl.added_at,
//                              }).ToList();

//                if (result.Count() > 0)
//                {
//                    foreach (var list in result)
//                    {
//                        mdl.Add(new WishListGetVM
//                        {
//                            UserId = list.UserID,
//                            UserName = list.User_Name,
//                            ProductId = list.ProductID,
//                            ProductName = list.Item_Name,
//                            Date = list.added_at ?? DateTime.MinValue // Fix nullable error
//                        });
//                    }
//                    //resp = new GetWishListResponse { WIshList = mdl };
//                    //return resp;
//                    resp = new GetWishListResponse
//                    {
//                        Status_Code = "200",
//                        Status = "Success",
//                        WIshList = mdl
//                    };
//                }
//                else
//                {
//                    resp = new GetWishListResponse
//                    {
//                        Status_Code = "0",
//                        Status = "error",
//                        Message = "Data Not Found"
//                    };
//                }
//            }
//            catch (Exception ex)
//            {
//                resp = new GetWishListResponse
//                {
//                    Status_Code = "0",
//                    Status = "error",
//                    Message = ex.Message
//                };
//            }

//            return resp;
//        }


//    }

//}