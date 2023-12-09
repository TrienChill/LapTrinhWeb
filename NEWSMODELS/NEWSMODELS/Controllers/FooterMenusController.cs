using NEWSMODELS.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEWSMODELS.Controllers
{
    public class FooterMenusController : Controller
    {
        // GET: FooterMenus

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MenuFooterList()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var patents = from p in context.Menu_Footers where (p.ParentID == 0) select p;
            ViewBag.parents = patents;
            ViewBag.parent = patents.First().ID_Footer;
            long id = ViewBag.parent;
            var menusfooter = from m in context.Menu_Footers.OrderBy(m => m.ID_Footer) where (m.ParentID == id) select m;
            ViewBag.menus = menusfooter;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MenuFooterList(FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var parents = from p in context.Menu_Footers where (p.ParentID == 0) select p;
            ViewBag.parents = parents;
            ViewBag.parent = collection.Get("ID_Footer");
            long id = Convert.ToInt64(ViewBag.parent);
            var menusfooter = from m in context.Menu_Footers.OrderBy(m => m.ID_Footer) where (m.ParentID == id) select m;
            ViewBag.menus = menusfooter;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditMenuFooter(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menu = context.Menu_Footers.Single(m => m.ID_Footer == id);
            ViewBag.parent = menu.ParentID;
            var parents = from m in context.Menu_Footers where (m.ParentID == 0) select m;
            ViewBag.parents = parents;
            return View(menu);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditMenuFooter(FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            int id = Convert.ToInt32(collection.Get("ID_Footer"));
            string title = collection.Get("TitleFooter");
            Menu_Footer menu = context.Menu_Footers.Single(m => m.ID_Footer == id);
            menu.TitleFooter = title;
            context.SubmitChanges();
            return RedirectToAction("MenuFooterList");
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddMenuFooter()
        {
            ViewBag.Message = "Nhập thông tin";
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var parents = from m in context.Menu_Footers where (m.ParentID == 0) select m;
            ViewBag.parent = 0;
            ViewBag.idnew = (context.Menu_Footers.Max(n => n.ID_Footer) + 1).ToString();
            ViewBag.parents = parents;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddMenuFooter(FormCollection collection)
        {
            string error = "";
            long id = 0;
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            if (!String.IsNullOrEmpty(collection.Get("ID_Footer")))
            {
                id = Convert.ToInt64(collection.Get("ID_Footer"));
                if (context.Menu_Footers.SingleOrDefault(m => m.ID_Footer == id) != null) error += "ID_MN đã tồn tại<br/>";
            }
            else
                error += "Chưa nhap ID_Footer<br/>";
            string title = "";
            if (!string.IsNullOrEmpty(collection.Get("TitleFooter")))
                title = collection.Get("TitleFooter");
            else
                error += "Chưa nhập TitleFooter<br/>";
            string link = "";
            if (!string.IsNullOrEmpty(collection.Get("Link")))
                link = collection.Get("Link");
            else
                error += "Chưa nhập Link<br/>";
            long parent = 0;
            if (!string.IsNullOrEmpty(collection.Get("ParentID")))
                parent = Convert.ToInt64(collection.Get("ParentID"));
            else
                error += "Chưa chọn menu cha<br/>";
            if (!string.IsNullOrEmpty(error))
                ViewBag.Message = error;
            else
            {
                //Lưu
                Menu_Footer menu = new Menu_Footer();
                menu.ID_Footer = id;
                menu.TitleFooter = title;
                menu.Link = link;
                menu.ParentID = parent;
                context.Menu_Footers.InsertOnSubmit(menu);
                context.SubmitChanges();
                ViewBag.Message = "Lưu thành công. Bạn có thể thêm nữa";
            }
            ViewBag.idnew = (context.Menu_Footers.Max(n => n.ID_Footer) + 1).ToString();
            var parents = from m in context.Menu_Footers where (m.ParentID == 0) select m;
            ViewBag.parent = parent;
            ViewBag.parents = parents;
            return View();
        }

        public ActionResult DeleteMenuFooter(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            Menu_Footer menus = context.Menu_Footers.Single(n => n.ID_Footer == id);
            context.Menu_Footers.DeleteOnSubmit(menus);
            context.SubmitChanges();
            return RedirectToAction("MenuFooterList");
        }
    }
}