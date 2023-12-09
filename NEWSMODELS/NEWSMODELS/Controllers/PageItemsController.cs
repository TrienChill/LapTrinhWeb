using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using NEWSMODELS.Models;
using PagedList;

namespace NEWSMODELS.Controllers
{
    public class PageItemsController : Controller
    {
        // GET: PageItem
        public ActionResult Index(int? id, int? mn)
        {
            if (mn != null) Session.Add("mn", mn);
            else
                return RedirectToAction("News");
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var pages = from p in context.PageItems.OrderBy(p => p.ID_P)
                        where (p.ID_MN.ToString() == Session["mn"].ToString()) select p;
            int pagesize = 4;
            int pageindex = id ?? 1;
            return View(pages.ToPagedList(pageindex, pagesize));
        }
        public ActionResult News(int? id)
        {
            if (Session["username"].ToString() == "Logins") return RedirectToAction("Logins", "Login");
            MenuProviderController menu = new MenuProviderController();
            Session["menu"] = menu.menuMainHoziontal();
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var pages = from p in context.PageItems.OrderBy(p => p.ID_P) select p;
            int pagesize = 4;
            int pageindex = id ?? 1;
            return View(pages.ToPagedList(pageindex, pagesize));
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ViewPageItem(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var page = context.PageItems.Single(m => m.ID_P == id);
            ViewBag.file = page.Image;
            return View(page);
        }
        public ActionResult PageItemList(int? id, FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var pages = from p in context.PageItems.OrderBy(p => p.ID_P) select p;
            ViewBag.pages = pages;
            ViewBag.page = collection.Get("ID_P");
            int pagesize = 3;
            int pageindex = id ?? 1;
            return View(pages.ToPagedList(pageindex, pagesize));
        }
        [HttpGet]
        public ActionResult EditPageItem(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            /* var page = context.PageItems.Single(p => p.ID_P == id);
             var parents = from m in context.Menus where (m.Parent == 0) select m;
             ViewBag.parent = page.ID_MN;
             ViewBag.parents = parents;*/
            var parents = from m in context.Menus select m;
            ViewBag.parents = parents;
            var page = context.PageItems.Single(m => m.ID_P == id);
            ViewBag.parent = page.ID_MN;
            ViewBag.page = page;
            return View(page);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPageItem(FormCollection collection, HttpPostedFileBase file )
        {
            string error = "";
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            int id = Convert.ToInt32(collection.Get("ID_P"));
            string title = collection.Get("Title");
            string sumary = collection.Get("Sumary");
            string content = collection.Get("Contents");
            string useradd = collection.Get("UserAdd");
            long parent = 0;
            if (!string.IsNullOrEmpty(collection.Get("ID_Parent")))
                parent = Convert.ToInt64(collection.Get("ID_Parent"));
            else
                error += "Chưa chọn menu cha<br/>";
            PageItem page = context.PageItems.Single(p => p.ID_P == id);
            page.Title = title;
            page.Sumary = sumary;
            page.Contents = content;
            page.UserAdd = useradd;
            page.ID_MN = parent;
            if (file != null && file.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                file.SaveAs(_path);
                page.Image = _FileName;
            }
            {//try
             //{
             //    if (Request.Files.Count > 0)
             //    {
             //        if (file != null && file.ContentLength > 0)
             //        {
             //            string _FileName = id.ToString() + " " + Path.GetFileName(file.FileName);
             //            string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
             //            file.SaveAs(_path);
             //            page.Image = _FileName;
             //            ViewBag.Message = "Upload thành công!";
             //        }
             //        context.SubmitChanges();
             //    }
             //}
             //catch
             //{
             //    ViewBag.Message = "Uploads thất bại";
             //}
            }
            context.SubmitChanges();
            return RedirectToAction("PageItemList");
        }
        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public ActionResult AddPageItem()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            /* ViewBag.idnew = (context.PageItems.Max(n => n.ID_P) + 1).ToString();
             var parents = from m in context.Menus where (m.Parent == 0) select m;
             ViewBag.parent = 0;
             ViewBag.parents = parents;*/
            ViewBag.idpnew = (context.PageItems.Max(n => n.ID_P) + 1).ToString();
            var parents = from p in context.Menus where (p.Parent != 0) select p;
            ViewBag.parent = parents.First().ID_MN;
            ViewBag.parent = 0;
            ViewBag.parents = parents;
            return View();

        }
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult AddPageItem(FormCollection collection, HttpPostedFileBase file)
        {
            string error = "";
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            int id = Convert.ToInt32(collection.Get("ID_P"));
            string title = "";
            if (!string.IsNullOrEmpty(collection.Get("Title")))
                title = collection.Get("Title");
            else
                error += "Chưa nhập Title<br/>";
            string sumary = collection.Get("Sumary");
            string useradd = collection.Get("UserAdd");
            string date = collection.Get("CreaDate");
            long parent = 0;
            if (!string.IsNullOrEmpty(collection.Get("ID_Parent")))
                parent = Convert.ToInt64(collection.Get("ID_Parent"));
            else
                error += "Chưa chọn menu cha<br/>";
            if (!string.IsNullOrEmpty(error))
                ViewBag.Message = error;
            else
            {
                PageItem page = new PageItem();
                page.ID_P = id;
                page.Title = title;
                page.Sumary = sumary;
                page.UserAdd = useradd;
                page.CreaDate = date;
                page.ID_MN = parent;
                context.PageItems.InsertOnSubmit(page);
                context.SubmitChanges();
                long id_p = context.PageItems.Max(n => n.ID_P) + 1;
                if (file.ContentLength > 0)
                {
                    string _Filename = id_p.ToString() + Path.GetExtension(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _Filename);
                    file.SaveAs(_path);
                    page.Image = _Filename;
                }
            }
            ViewBag.idpnew = (context.PageItems.Max(n => n.ID_P) + 1).ToString();
            var parents = from m in context.Menus where (m.Parent == 0) select m;
            ViewBag.parent = parent;
            ViewBag.parents = parents;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeletePageItem(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            PageItem page = context.PageItems.Single(n => n.ID_P == id);
            context.PageItems.DeleteOnSubmit(page);
            context.SubmitChanges();
            return RedirectToAction("PageItemList");
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult FindNews(int ? id, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                ViewBag.key = key;
                NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
                var pages = from p in context.PageItems
                            .Where(p => p.Title.Contains(key) || p.Contents.Contains(key))
                            .OrderBy(p => p.ID_P)
                            select p;
                int pagesize = 2;
                int pageindex = id ?? 1;
                return View(pages.ToPagedList(pageindex, pagesize));
            }
            else
            {
                return View("PageItemList");
            }
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult FindList(int? id, string key)
        {
            if (!string.IsNullOrEmpty(key)&& key != null)
            {
                ViewBag.key = key;
                NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
                var pages = from p in context.PageItems
                            .Where(p => p.Title.Contains(key) || p.Sumary.Contains(key))
                            .OrderBy(p => p.ID_P)
                            select p;
                int pagesize = 3;
                int pageindex = id ?? 1;
                return View(pages.ToPagedList(pageindex, pagesize));
            }
            else
                return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ViewPage(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var page = context.PageItems.Single(p => p.ID_P == id);
            var links = from p in context.PageItems.OrderBy(p => p.ID_P)
                        .Where(p => p.ID_P > id).Take(10) select p;
            ViewBag.links = links;
            return View(page);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditNews()
        {
            ViewBag.content = "";
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult EditNews(FormCollection collection)
        {
            ViewBag.content = collection.Get("editor1");
            return View();
        }
    }
}
