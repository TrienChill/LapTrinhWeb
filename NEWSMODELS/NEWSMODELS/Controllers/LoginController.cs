using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NEWSMODELS.Models;

namespace NEWSMODELS.Controllers
{
    public class LoginController : Controller

    {

        
        // GET: Login
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Logins()
        {
            return View();
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logins(FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            string us = collection.Get("Username");
            string p = collection.Get("Password");
            Accout ac = context.Accouts.SingleOrDefault(a => a.UserName == us && a.Password == p);
            if (ac != null)
            {
                Session["username"] = us;
                Session["permission"] = ac.Permission;
                if (ac.Permission == 1)
                {
                    Session["permission"] = "Toàn quyền";
                }
                else
                    Session["permission"] = "Không toàn quyền";
                Response.Redirect("~/PageItems/News");
            }
            else
                ViewBag.Message = "Sai tài khoản hoặc mật khẩu !!!";
            return View();
        }
        
        

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Logins");
        }
    }
}