using NEWSMODELS.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEWSMODELS.Controllers
{
    public class PageFooterController : Controller
    {
        // GET: PageFooter
        public ActionResult Index(int? id, int? mn)
        {
            if (mn != null) Session.Add("mn", mn);
            else
                return RedirectToAction("News");
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var pages = from p in context.PageItems.OrderBy(p => p.ID_P)
                        where (p.ID_MN.ToString() == Session["mn"].ToString())
                        select p;
            int pagesize = 2;
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
            int pagesize = 3;
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
        public ActionResult PageItemListF(int? id, FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var pages = from p in context.PageFooters.OrderBy(p => p.ID_F) select p;
            ViewBag.pages = pages;
            ViewBag.page = collection.Get("ID_F");
            int pagesize = 5;
            int pageindex = id ?? 1;
            return View(pages.ToPagedList(pageindex, pagesize));
        }
        [HttpGet]
        public ActionResult EditPageF(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            /* var page = context.PageItems.Single(p => p.ID_P == id);
             var parents = from m in context.Menus where (m.Parent == 0) select m;
             ViewBag.parent = page.ID_MN;
             ViewBag.parents = parents;*/
            var parents = from m in context.Menu_Footers select m;
            ViewBag.parents = parents;
            var page = context.PageFooters.Single(m => m.ID_F == id);
            ViewBag.parent = page.ID_Footer;
            ViewBag.page = page;
            return View(page);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPageF(FormCollection collection, HttpPostedFileBase file)
        {
            string error = "";
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            int id = Convert.ToInt32(collection.Get("ID_F"));

            string title = collection.Get("TitleF");
            string content = collection.Get("ContentF");
            long parent = 0;
            if (!string.IsNullOrEmpty(collection.Get("ParentID")))
                parent = Convert.ToInt64(collection.Get("ParentID"));
            else
                error += "Chưa chọn menu cha<br/>";
            PageFooter page = context.PageFooters.Single(p => p.ID_F == id);
            page.TitleF = title;
            page.ContentF = content;
            page.ID_Footer = parent;
            context.SubmitChanges();
            return RedirectToAction("PageItemListF");
        }
        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public ActionResult AddPageF()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            /* ViewBag.idnew = (context.PageItems.Max(n => n.ID_P) + 1).ToString();
             var parents = from m in context.Menus where (m.Parent == 0) select m;
             ViewBag.parent = 0;
             ViewBag.parents = parents;*/
            ViewBag.idpnew = (context.PageFooters.Max(n => n.ID_F) + 1).ToString();
            var parents = from p in context.Menu_Footers where (p.ParentID != 0) select p;
            ViewBag.parent = parents.First().ID_Footer;
            ViewBag.parent = 0;
            ViewBag.parents = parents;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult AddPageF(FormCollection collection, HttpPostedFileBase file)
        {
            string error = "";
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            int id = Convert.ToInt32(collection.Get("ID_F"));
            string title = "";
            if (!string.IsNullOrEmpty(collection.Get("TitleF")))
                title = collection.Get("Title");
            else
                error += "Chưa nhập Title<br/>";
            string content = "";
            if (!string.IsNullOrEmpty(collection.Get("ContentF")))
                content = collection.Get("ContentF");
            else
                error += "Chưa nhập Title<br/>";
            long parent = 0;
            if (!string.IsNullOrEmpty(collection.Get("ParentID")))
                parent = Convert.ToInt64(collection.Get("ParentID"));
            else
                error += "Chưa chọn menu cha<br/>";
            if (!string.IsNullOrEmpty(error))
                ViewBag.Message = error;
            else
            {
                PageFooter page = new PageFooter();
                page.ID_F = id;
                page.TitleF = title;
                page.ContentF = content;
                page.ID_Footer = parent;
                context.PageFooters.InsertOnSubmit(page);
                context.SubmitChanges();
                long id_p = context.PageFooters.Max(n => n.ID_F) + 1;
            }
            ViewBag.idpnew = (context.PageFooters.Max(n => n.ID_F) + 1).ToString();
            var parents = from m in context.Menu_Footers where (m.ParentID == 0) select m;
            ViewBag.parent = parent;
            ViewBag.parents = parents;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeletePageItemF(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            PageFooter page = context.PageFooters.Single(n => n.ID_F == id);
            context.PageFooters.DeleteOnSubmit(page);
            context.SubmitChanges();
            return RedirectToAction("PageItemListF");
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult FindNews(int? id, string key)
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
            if (!string.IsNullOrEmpty(key) && key != null)
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
                        .Where(p => p.ID_P > id).Take(10)
                        select p;
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