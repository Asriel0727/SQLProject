using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project.Models;
using PagedList;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        dbSCEntities db = new dbSCEntities();
        // int pageSize = 2;

        // GET: Home
        public ActionResult Index()/*(int page=1)*/
        {
            //   int currentPage = page < 1 ? 1 : page;
            var products = db.Product
              .OrderByDescending(m => m.fId).ToList();
            //    var result = products.ToPagedList(currentPage, pageSize);
            return View(products);
        }

        //Get: Home/Login
        public ActionResult Login()
        {
            return View();
        }
        //Post: Home/Login
        [HttpPost]
        public ActionResult Login(string mId, string mPwd)
        {
            // 依帳密取得會員並指定給member
            var member = db.Member
                .Where(m => m.mId == mId && m.mPwd == mPwd)
                .FirstOrDefault();
            //若member為null，表示會員未註冊
            if (member == null)
            {
                ViewBag.Message = "帳密錯誤，登入失敗";
                return View();
            }
            //使用Session變數記錄歡迎詞
            Session["WelCome"] = member.mName + "歡迎光臨";
            FormsAuthentication.RedirectFromLoginPage(mId, true);
            return RedirectToAction("Index", "Member");
        }

        //Get:Home/Register
        public ActionResult Register()
        {
            return View();
        }
        //Post:Home/Register
        [HttpPost]
        public ActionResult Register(Member pMember)
        {
            //若模型沒有通過驗證則顯示目前的View
            if (ModelState.IsValid == false)
            {
                return View();
            }
            // 依帳號取得會員並指定給member
            var member = db.Member
                .Where(m => m.mId == pMember.mId)
                .FirstOrDefault();
            //若member為null，表示會員未註冊
            if (member == null)
            {
                //將會員記錄新增到tMember資料表
                db.Member.Add(pMember);
                db.SaveChanges();
                //執行Home控制器的Login動作方法
                return RedirectToAction("Login");
            }
            ViewBag.Message = "此帳號己有人使用，註冊失敗";
            return View();
        }

    }
}