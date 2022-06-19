using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project.Models;
using System.Data;
using System.Data.SqlClient;
using PagedList;

namespace Project.Controllers
{

    [Authorize]
    public class MemberController : Controller
    {

        dbSCEntities db = new dbSCEntities();
        int pageSize = 3;

        public ActionResult Index()
        {

            var products = db.Product.OrderByDescending(m => m.fId).ToList();

            return View("../Home/Index", "_LayoutMember", products);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }


        public ActionResult ShoppingCar()
        {

            string mId = User.Identity.Name;

            var orderdetail = db.OrderDetail.Where
                (m => m.mId == mId && m.oIsApproved == "否")
                .ToList();

            return View(orderdetail);
        }



        public ActionResult AddCar(string pId)
        {

            string mId = User.Identity.Name;

            var currentCar = db.OrderDetail
                .Where(m => m.pId == pId && m.oIsApproved == "否" && m.mId == mId)
                .FirstOrDefault();

            if (currentCar == null)
            {

                var product = db.Product.Where(m => m.pId == pId).FirstOrDefault();

                OrderDetail orderdetail = new OrderDetail();
                orderdetail.mId = mId;
                orderdetail.pId = product.pId;
                orderdetail.pName = product.pName;
                orderdetail.pPrice = product.pPrice;
                orderdetail.oNum = 1;
                orderdetail.oIsApproved = "否";
                db.OrderDetail.Add(orderdetail);
            }
            else
            {

                currentCar.oNum += 1;
            }
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");


        }




        //Get:Member/DeleteCar
        public ActionResult DeleteCar(int fId)
        {

            var orderDetail = db.OrderDetail.Where
                (m => m.fId == fId).FirstOrDefault();

            db.OrderDetail.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("ShoppingCar");
        }


        [HttpPost]
        public ActionResult ShoppingCar(string mName, string mEmail, string oAddress)
        {
            string mId = User.Identity.Name;
            string oId = Guid.NewGuid().ToString();

            Order order = new Order();
            order.oId = oId;
            order.mId = mId;
            order.mName = mName;
            order.mEmail = mEmail;
            order.oAddress = oAddress;
            order.oDate = DateTime.Now;
            db.Order.Add(order);
            var carList = db.OrderDetail
                .Where(m => m.oIsApproved == "否" && m.mId == mId)
                .ToList();

            foreach (var item in carList)
            {
                item.oId = oId;
                item.oIsApproved = "是";
            }

            db.SaveChanges();




            return RedirectToAction("OrderList");


        }


        public ActionResult OrderList(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            string mId = User.Identity.Name;
            var orders = db.Order.Where(m => m.mId == mId)
                .OrderByDescending(m => m.oDate).ToList();
            var result = orders.ToPagedList(currentPage, pageSize);
            return View(result);
        }



        public ActionResult OrderDetail(string oId)
        {
            var orderDetails = db.OrderDetail
                .Where(m => m.oId == oId).ToList();
            return View(orderDetails);
        }
        public ActionResult Edit(string oId)
        {
            var orderlist = db.Order
               .Where(m => m.oId == oId).FirstOrDefault();
            return View(orderlist);
        }

        [HttpPost]
        public ActionResult Edit(Order oProduct)
        {

            // 修改資料
            var orderlist = db.Order
                .Where(m => m.oId == oProduct.oId).FirstOrDefault();
            orderlist.mName = oProduct.mName;
            orderlist.oAddress = oProduct.oAddress;
            orderlist.mEmail = oProduct.mEmail;
            db.SaveChanges();
            return RedirectToAction("OrderList");
        }

    }

}