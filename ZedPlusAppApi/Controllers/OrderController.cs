//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Http;
//using System.Web.Mvc;
//using ZedPlusAppApi.Models;

//namespace ZedPlusAppApi.Controllers
//{
//    public class OrderController : ApiController
//    {
//        [System.Web.Http.HttpPost]
//        [System.Web.Http.Route("api/AddOrder")]
//        public JsonResponse AddOrder(int CustomerId,int AddressId)
//        {
//            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
//            JsonResponse resp = new JsonResponse();
//            try
//            {
//                var res = (from tbl in db.tblCarts
//                          where tbl.CustomerID == CustomerId && tbl.Status == "Active"
//                          select new 
//                          { 
//                              tbl.CartID,
//                              tbl.ItemID,
//                              tbl.TotalPice,
//                              tbl.Quantity,
//                              tbl.SizeID
//                          }).ToList();
//                if (res.Count() > 0)
//                {
//                    string newOrderNo="";
//                    var lastOrder = db.tblOrders.OrderByDescending(o => o.OrderNumber).FirstOrDefault();
//                    if (lastOrder != null)
//                    {
//                        // Extract numeric part and increment
//                        int lastNumber = int.Parse(lastOrder.OrderNumber.Substring(2));
//                        newOrderNo = "OD" + (lastNumber + 1).ToString("D10");
//                    }
//                    else
//                    {
//                        newOrderNo = "OD0000000001"; // First entry
//                    }

//                    foreach (var item in res.ToList())
//                    {
//                        var pv = db.tblProducts.FirstOrDefault(x=>x.ID == item.ItemID);
//                        tblOrder tbl1 = new tblOrder();
//                        tbl1.CustomerID = CustomerId;
//                        tbl1.Itemid = item.ItemID;
//                      //  tbl1.TotalPrice = item.TotalPice;
//                        tbl1.Quantity = item.Quantity;
//                        tbl1.OrderStatus = "Pending";
//                        tbl1.AddressId = AddressId;
//                        tbl1.TotalPV = Convert.ToString(Convert.ToInt32(pv.PV) * Convert.ToInt32(item.Quantity));
//                        tbl1.Date = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
//                        tbl1.OrderNumber = newOrderNo;
//                        var data = db.tblOrders.Add(tbl1);
//                        db.SaveChanges();

                        
//                        if (tbl1.OrderID > 0)
//                        {
//                            tblCart tbl2 = db.tblCarts.FirstOrDefault(x => x.CartID == item.CartID);
//                            tbl2.Status = "DeActive";
//                            db.Entry(tbl2).State = EntityState.Modified;
//                            db.SaveChanges();
//                            resp = new JsonResponse { Status_Code = "200", Status = "Success", Message = "Order Added Successfully" };
//                        }
//                        else
//                        {
//                            resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Something went wrong.Please try again." };
//                        }
//                    }
//                }
//                else
//                {
//                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Data Not Found !" };
//                }
//            }
//            catch (Exception ex)
//            {
//                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
//            }
//            return resp;
//        }

//        [System.Web.Http.HttpGet]
//        [System.Web.Http.Route("api/GetOrderSummary")]
//        public OrderSummaryResponse GetOrderSummary(int CustomerId)
//        {
//            OrderSummaryResponse resp = new OrderSummaryResponse();
//            List<OrderSummaryVM> mdl = new List<OrderSummaryVM>();
//            try
//            {
//                db_zedPlusShopEntities db = new db_zedPlusShopEntities();

//                var lastOrder = db.tblOrders.OrderByDescending(o => o.OrderNumber).FirstOrDefault();
//                var result = (from tbl in db.tblOrders
//                              where tbl.CustomerID == CustomerId && tbl.OrderStatus == "Pending" && tbl.OrderNumber == lastOrder.OrderNumber
//                              select new
//                              {
//                                  tbl.Quantity,
//                                  tbl.TotalPrice,
//                                  tbl.TotalPV,
//                                  tbl.Date,
//                                  tbl.OrderNumber
//                              }).ToList();
//                if (result.Count() > 0)
//                {
//                    double sumquantity = 0;
//                    double totalprice = 0;
//                    double totalpv = 0;
//                    foreach (var list in result)
//                    {
//                        sumquantity += Convert.ToDouble(list.Quantity);
//                        totalprice += Convert.ToDouble(list.TotalPrice);
//                        totalpv += Convert.ToDouble(list.TotalPV);
//                    }
//                    mdl.Add(new OrderSummaryVM
//                    {
//                        OrderNumber = result.FirstOrDefault().OrderNumber,
//                        ProductQuantity = result.Count().ToString(),
//                        ItemQuantity = sumquantity.ToString(),
//                        TotalPrice = totalprice.ToString(),
//                        TotalPV = totalpv.ToString(),
//                        Discount = "0",
//                        Date = result.FirstOrDefault().Date.ToString()
//                    }); ;

//                    resp = new OrderSummaryResponse { OrderList = mdl };
//                    return resp;
//                }
//                else
//                {
//                    resp = new OrderSummaryResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
//                }

//            }
//            catch (Exception ex)
//            {
//                resp = new OrderSummaryResponse { Status_Code = "0", Status = "error", Message = ex.Message };
//            }
//            return resp;
//        }

//        [System.Web.Http.HttpPost]
//        [System.Web.Http.Route("api/OrderReceive")]
//        public JsonResponse OrderReceive(int CustomerId, string OrderNumber)
//        {
//            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
//            JsonResponse resp = new JsonResponse();
//            try
//            {
//                var res = db.tblOrders.Where(x => x.CustomerID == CustomerId && x.OrderNumber == OrderNumber && x.OrderStatus == "Pending").ToList();
//                if (res != null)
//                {
//                    foreach (var item in res)
//                    {
//                        item.OrderStatus = "Order Received";
//                        db.SaveChanges();
//                    }
//                }
//                else
//                {
//                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
//                }
//            }
//            catch (Exception ex)
//            {
//                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
//            }
//            return resp;
//        }


//        [System.Web.Http.HttpGet]
//        [System.Web.Http.Route("api/GetMyOrderList")]
//        public GetMyOrderListResponse GetMyOrderList(int CustomerId)
//        {
//            GetMyOrderListResponse resp = new GetMyOrderListResponse();
//            List<GetMyOrderListVM> mdl = new List<GetMyOrderListVM>();
//            try
//            {
//                db_zedPlusShopEntities db = new db_zedPlusShopEntities();                
//                var result = (from tbl in db.tblOrders
//                              join tbla in db.tblProducts on tbl.Itemid equals tbla.ID into a
//                              from tbla in a.DefaultIfEmpty()
//                              //join tblb in db.tblColorMasters on tbla.ColorID equals tblb.ID into b
//                              //from tblb in b.DefaultIfEmpty()
//                              where tbl.CustomerID == CustomerId
//                              select new
//                              {
//                                  tbl.OrderID,
//                                  tbla.Item_Name,
//                                  ProductID = tbla.ID,
//                                  //tbld.ImageURL,
//                                  //tblb.Color_Name,
//                                  tbl.OrderStatus,
//                                  tbl.Date,
//                                  tbl.OrderNumber
//                              }).ToList();
//                if (result.Count() > 0)
//                {
//                    foreach (var list in result)
//                    {
//                        string firstImagePath = "";
//                        var primaryVariant = db.ProductVariants
//                                               .FirstOrDefault(x => x.ProductID == list.ProductID && x.IsPrimary == true);

//                        if (primaryVariant != null)
//                        {
//                            var variantImage = db.tblVaraintsimages
//                                                 .FirstOrDefault(x => x.VariantID == primaryVariant.VariantID);

//                            if (variantImage != null && !string.IsNullOrEmpty(variantImage.ImageUrl))
//                            {
//                                firstImagePath = variantImage.ImageUrl.Split(',')[0];
//                            }
//                        }
//                        mdl.Add(new GetMyOrderListVM
//                        {
//                            OrderID = list.OrderID,
//                            OrderNumber=list.OrderNumber,
//                            ProductName = list.Item_Name,
//                            ProductID=list.ProductID,
//                            ProductImage = firstImagePath,
//                           //ProductColor = list.Color_Name,
//                            OrderStatus = list.OrderStatus,
//                            OrderDate = Convert.ToDateTime(list.Date),
//                        });
//                    }
//                    resp = new GetMyOrderListResponse { GetMyOrderList = mdl };
//                    return resp;
//                }
//                else
//                {
//                    resp = new GetMyOrderListResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
//                }                
//            }
//            catch (Exception ex)
//            {
//                resp = new GetMyOrderListResponse { Status_Code = "0", Status = "error", Message = ex.Message };
//            }
//            return resp;
//        }


//        [System.Web.Http.HttpGet]
//        [System.Web.Http.Route("api/GetMyOrderDetail")]
//        public GetMyOrderDetailResponse GetOrderDetail(int OrderId ,int ProductId, int CustomerId)
//        {
//            GetMyOrderDetailResponse resp = new GetMyOrderDetailResponse();
//            List<GetMyOrderDetailVM> mdl = new List<GetMyOrderDetailVM>();
//            try
//            {
//                db_zedPlusShopEntities db = new db_zedPlusShopEntities();

//                var result = (from tbl in db.tblOrders
//                              join tbla in db.tblProducts on tbl.Itemid equals tbla.ID into a
//                              from tbla in a.DefaultIfEmpty()
//                              join tblb in db.tblAddresses on tbl.AddressId equals tblb.Id into b
//                              from tblb in b.DefaultIfEmpty()
//                              join tblc in db.tblDistrictMasters on tblb.DistrictId equals tblc.ID into c
//                              from tblc in c.DefaultIfEmpty()
//                              join tbld in db.tblStateMasters on tblb.StateId equals tbld.StateID into d
//                              from tbld in d.DefaultIfEmpty()
//                              where tbl.OrderID == OrderId && tbl.Itemid == ProductId && tbl.CustomerID == CustomerId
//                              select new
//                              {
//                                  tbl.OrderID,
//                                  tbl.OrderNumber,
//                                  OrderDate=tbl.Date,
//                                  ProductID=tbla.ID,
//                                  ProductName=tbla.Item_Name,
//                                  ProductPrice=tbla.NRP,
//                                  OrderQuantity =tbl.Quantity,                                  
//                                  tbl.TotalPrice,
//                                  ReceiverName=tblb.Name,
//                                  MobileNo=tblb.Phone,
//                                  tbl.OrderStatus, 
//                                  tblb.Address,
//                                  tblc.DistrictName,
//                                  tbld.State_Name
//                              }).ToList();
//                if (result.Count() > 0)
//                {
//                    foreach (var list in result)
//                    {
//                        string firstImagePath = "";
//                        var primaryVariant = db.ProductVariants
//                                               .FirstOrDefault(x => x.ProductID == list.ProductID && x.IsPrimary == true);

//                        if (primaryVariant != null)
//                        {
//                            var variantImage = db.tblVaraintsimages
//                                                 .FirstOrDefault(x => x.VariantID == primaryVariant.VariantID);

//                            if (variantImage != null && !string.IsNullOrEmpty(variantImage.ImageUrl))
//                            {
//                                firstImagePath = variantImage.ImageUrl.Split(',')[0];
//                            }
//                        }
//                        string Address =list.Address+","+list.DistrictName+","+list.State_Name;
//                        mdl.Add(new GetMyOrderDetailVM
//                        {
//                            OrderID = list.OrderID,
//                            OrderNumber=list.OrderNumber,
//                            OrderDate=Convert.ToString(list.OrderDate),
//                         //   OrderQuantity=list.OrderQuantity,
//                            OrderStatus=list.OrderStatus,
//                            TotalPrice=list.TotalPrice,
//                            ProductID=list.ProductID,
//                            ProductName = list.ProductName,
//                            ProductPrice = list.ProductPrice.ToString(),
//                            ProductImage = firstImagePath,                            
//                            ReceiverName =list.ReceiverName,
//                            MobileNo=list.MobileNo,
//                            ShippingAddress= Address
//                        }); 
//                    }
//                    resp = new GetMyOrderDetailResponse { GetMyOrderDetail = mdl };
//                    return resp;
//                }
//                else
//                {
//                    resp = new GetMyOrderDetailResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
//                }
//            }
//            catch (Exception ex)
//            {
//                resp = new GetMyOrderDetailResponse { Status_Code = "0", Status = "error", Message = ex.Message };
//            }
//            return resp;
//        }

//        [System.Web.Http.HttpPost]
//        [System.Web.Http.Route("api/OrderCancelation")]
//        public JsonResponse OrderCancelation(int OrderID, int CustomerID, string OrderCancelationReason)
//        {
//            db_zedPlusShopEntities db = new db_zedPlusShopEntities();
//            JsonResponse resp = new JsonResponse();
//            try
//            {
//                var res = db.tblOrders.Where(x => x.OrderID == OrderID && x.CustomerID == CustomerID && x.OrderStatus != "Order Canceled" && x.OrderStatus != "Shipped" && x.OrderStatus != "Out For Delivery" && x.OrderStatus != "DeLivered").ToList();
//                if (res.Count() > 0)
//                {
//                    foreach (var item in res)
//                    {
//                        item.OrderStatus = "Order Canceled";
//                       // item.CancellationReason = OrderCancelationReason;
//                      //  item.CancelledDate = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
//                        db.SaveChanges();
//                    }
//                }
//                else
//                {
//                    resp = new JsonResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
//                }
//            }
//            catch (Exception ex)
//            {
//                resp = new JsonResponse { Status_Code = "0", Status = "error", Message = ex.Message };
//            }
//            return resp;
//        }

//    }
//}