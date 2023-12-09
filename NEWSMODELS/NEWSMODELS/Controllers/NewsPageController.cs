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
    public class NewsPageController : Controller
    {
        // GET: NewsPage

        public ActionResult IndexNew(int? id, int? mn)
        {
            if (mn != null) Session.Add("mn", mn);
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var pages = from p in context.PageItems.OrderBy(p => p.ID_P)
                        where (p.ID_MN.ToString() == Session["mn"].ToString())
                        select p;
            int pagesize = 2;
            int pageindex = id ?? 1;
            return View(pages.ToPagedList(pageindex, pagesize));
        }
        public ActionResult News_Blog(int ? id)
        {
            MenuProviderNewController menu = new MenuProviderNewController();
            MenuFooterController menu_Footer = new MenuFooterController();
            Session["menus"] = menu.menuMainHoziontals();
            Session["menufooter"] = menu_Footer.menuMainHoziontalFooter();
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var pages = from p in context.PageItems.OrderBy(p => p.ID_P) select p;
            int pagesize = 3;
            int pageindex = id ?? 1;
            return View(pages.ToPagedList(pageindex, pagesize));
        }
        public ActionResult FindNew_User(int? id, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                ViewBag.key = key;
                NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
                var pages = from p in context.PageItems
                            .Where(p => p.Title.Contains(key) || p.Contents.Contains(key))
                            .OrderBy(p => p.ID_P)
                            select p;
                int pagesize = 3;
                int pageindex = id ?? 1;
                return View(pages.ToPagedList(pageindex, pagesize));
            }
            else
            {
                return View("News_Blog");
            }
        }
        public ActionResult ViewPageNews(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var page = context.PageItems.Single(p => p.ID_P == id);
            var links = from p in context.PageItems.OrderBy(p => p.ID_P)
                        .Where(p => p.ID_P > id).Take(10)
                        select p;
            var menushort = from a in context.PageItems.OrderBy(a => a.ID_P) select a;
            ViewBag.acc = menushort;
            ViewBag.links = links;
            return View(page);
        }


        public ActionResult ViewFooter(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var page = context.PageFooters.Single(p => p.ID_F == id - 3);
            var links = from p in context.PageFooters.OrderBy(p => p.ID_F)
                        .Where(p => p.ID_F > id)
                        select p;
            var menushort = from a in context.PageItems.OrderBy(a => a.ID_P) select a;
            ViewBag.acc = menushort;
            ViewBag.links = links;
            return View(page);
        }
    }
}