using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEWSMODELS.Models;

namespace NEWSMODELS.Controllers
{
    public class AccoutsController : Controller
    {
        // GET: Accouts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccoutsList()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var acc = from a in context.Accouts.OrderBy(a => a.UserName) select a;
            ViewBag.acc = acc;
            return View();    
        }
        public ActionResult ViewAccouts(string user)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var acc = context.Accouts.Single(m => m.UserName == user);
            Session["permission"] = acc.Permission;
            if (acc.Permission == 1)
            {
                Session["permission"] = "Toàn quyền";
            }
            else
                Session["permission"] = "Không toàn quyền";
            return View(acc);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditAccouts(string user, FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var accs = context.Accouts.Single(m => m.UserName == user);
            return View(accs);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditAccouts(FormCollection collection, HttpPostedFileBase file)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            string username = collection.Get("UserName");
            //string pass = security.MD5(collection.Get("Password"));
            byte per = Convert.ToByte(collection.Get("Permission"));
            string fullname = collection.Get("Fullname");
            string email = collection.Get("Email");
            string info = collection.Get("Info");
            Accout acc = context.Accouts.Single(m => m.UserName == username);
            acc.UserName = username;
            //acc.Password = pass;
            acc.Permission = per;
            acc.Fullname = fullname;
            acc.Email = email;
            acc.Info = info;
            if (file != null && file.ContentLength > 0)
            {
                string _Filename = username + Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadAvata"), _Filename);
                file.SaveAs(_path);
                acc.Avata = _Filename;
            }
            context.SubmitChanges();
            return RedirectToAction("AccoutsList");
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddAccouts()
        {
            ViewBag.Message = "Nhập thông tin !";
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddAccouts(FormCollection collection, HttpPostedFileBase file)
        { 
            string error = "";
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            string username = "";
            if (!string.IsNullOrEmpty(collection.Get("UserName")))
                username = collection.Get("UserName");
            else
                error += "Chưa nhập UserName<br/>";
            string pass = "";
            if (!string.IsNullOrEmpty(collection.Get("Password")))
                pass = security.MD5(collection.Get("Password"));
            else
                error += "Chưa nhập Password<br/>";
            byte per = 0;
            if (!string.IsNullOrEmpty(collection.Get("Permission")))
                per = Convert.ToByte(collection.Get("Permission"));
            else
                error += "Chưa chọn quyền<br/>";
            string fullname = "";
            if (!string.IsNullOrEmpty(collection.Get("Fullname")))
                fullname = collection.Get("Fullname");
            else
                error += "Chưa nhập Fullname<br/>";
            string email = "";
            if (!string.IsNullOrEmpty(collection.Get("Email")))
                email = collection.Get("Email");
            else
                error += "Chưa nhập Email<br/>";
            string info = "";
            if (!string.IsNullOrEmpty(collection.Get("Info")))
                info = collection.Get("Info");
            else
                error += "Chưa nhập Info<br/>";
            if (!string.IsNullOrEmpty(error))
                ViewBag.Message = error;
            else
            {
                Accout acc = new Accout();
                acc.UserName = username;
                acc.Password = pass;
                acc.Permission = per;
                acc.Fullname = fullname;
                acc.Email = email;
                acc.Info = info;
                context.Accouts.InsertOnSubmit(acc);
                if (file != null && file.ContentLength > 0)
                {
                    string _Filename = username + Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadAvata"), _Filename);
                    file.SaveAs(_path);
                    acc.Avata = _Filename;
                }
                ViewBag.Message = "Thêm thành công !!!";
                
            }
            context.SubmitChanges();
            ViewBag.accnew = (context.Accouts.Max(a => a.UserName)).ToString();
            return View();
        }
        public ActionResult DeleteAccouts(string user)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            Accout acc = context.Accouts.Single(a => a.UserName == user);
            context.Accouts.DeleteOnSubmit(acc);
            context.SubmitChanges();
            return RedirectToAction("AccoutsList");
        }

        public ActionResult ResetPass (string user, FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            Accout acc = context.Accouts.Single(a => a.UserName == user);
            string pass = security.MD5("11111");
            acc.Password = pass;
            context.SubmitChanges();
            return RedirectToAction("AccoutsList");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditPassword(string user, FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var accs = context.Accouts.Single(m => m.UserName == user);
            return View(accs);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditPassword(FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            string username = collection.Get("UserName");
            //string pass = security.MD5(collection.Get("Password"));
            byte per = Convert.ToByte(collection.Get("Permission"));
            string fullname = collection.Get("Fullname");
            string email = collection.Get("Email");
            Accout acc = context.Accouts.Single(m => m.UserName == username);
            acc.UserName = username;
            //acc.Password = pass;
            acc.Permission = per;
            acc.Fullname = fullname;
            acc.Email = email;
            context.SubmitChanges();
            return RedirectToAction("AccoutsList");
        }
    }
}