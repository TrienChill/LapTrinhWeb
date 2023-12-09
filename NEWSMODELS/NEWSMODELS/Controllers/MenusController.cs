using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEWSMODELS.Models;
using PagedList;

namespace NEWSMODELS.Controllers
{
    public class MenusController : Controller
    {
        // GET: Menus
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MenuList()
        {
            if (Session["username"].ToString() == "Logins") return RedirectToAction("Logins", "Login");
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var patents = from p in context.Menus where (p.Parent == 0) select p;
            ViewBag.parents = patents;
            ViewBag.parent = patents.First().ID_MN;
            long id = ViewBag.parent;
            var menus = from m in context.Menus.OrderBy(m => m.ID_MN) where (m.Parent == id) select m;
            ViewBag.menus = menus;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MenuList(FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var parents = from p in context.Menus where (p.Parent == 0) select p;
            ViewBag.parents = parents;
            ViewBag.parent = collection.Get("ID_MN");
            long id = Convert.ToInt64(ViewBag.parent);
            var menus = from m in context.Menus.OrderBy(m => m.ID_MN) where (m.Parent == id) select m;
            ViewBag.menus = menus;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MenuListParent()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var patents = from p in context.Menus where (p.Parent == 0) select p;
            ViewBag.parents = patents;
            ViewBag.parent = patents.First().ID_MN;
            long id = ViewBag.parent;
            var menus = from m in context.Menus.OrderBy(m => m.ID_MN) where (m.Parent == 0) select m;
            ViewBag.menus = menus;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult MenuListParent(FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var parents = from p in context.Menus where (p.Parent == 0) select p;
            ViewBag.parents = parents;
            ViewBag.parent = collection.Get("ID_MN");
            long id = Convert.ToInt64(ViewBag.parent);
            var menus = from m in context.Menus.OrderBy(m => m.ID_MN) where (m.Parent == 0) select m;
            ViewBag.menus = menus;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ViewMenu(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menu = context.Menus.Single(m => m.ID_MN == id);
            return View(menu);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditMenu(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menu = context.Menus.Single(m => m.ID_MN == id);
            ViewBag.parent = menu.Parent;
            var parents = from m in context.Menus where (m.Parent == 0) select m;
            ViewBag.parents = parents;
            return View(menu);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditMenu(FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            int id = Convert.ToInt32(collection.Get("ID_MN"));
            string lablel = collection.Get("Lablel");
            byte pos = Convert.ToByte(collection.Get("pos"));
            long parent = Convert.ToInt64(collection.Get("ID_Parent"));
            string url = collection.Get("UrlLink");
            Menus menu = context.Menus.Single(m => m.ID_MN == id);
            menu.Lablel = lablel;
            menu.Pos = pos;
            menu.Parent = parent;
            menu.UrlLink = url;
            context.SubmitChanges();
            return RedirectToAction("MenuList");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditMenuParent(int id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menu = context.Menus.Single(m => m.ID_MN == id);
            ViewBag.parent = menu.Parent;
            var parents = from m in context.Menus where (m.Parent == 0) select m;
            ViewBag.parents = parents;
            return View(menu);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditMenuParent(FormCollection collection)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            int id = Convert.ToInt32(collection.Get("ID_MN"));
            string lablel = collection.Get("Lablel");
            byte pos = Convert.ToByte(collection.Get("pos"));
            long parent = Convert.ToInt64(collection.Get("ID_Parent"));
            string url = collection.Get("UrlLink");
            Menus menu = context.Menus.Single(m => m.ID_MN == id);
            menu.Lablel = lablel;
            menu.Pos = pos;
            menu.Parent = parent;
            menu.UrlLink = url;
            context.SubmitChanges();
            return RedirectToAction("MenuListParent");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult DeleteMenu(int id)
        {
                NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
                Menus menus = context.Menus.Single(n => n.ID_MN == id);
                context.Menus.DeleteOnSubmit(menus);
                context.SubmitChanges();
                return RedirectToAction("MenuList");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddMenuParent()
        {
            if (Session["username"].ToString() == "Logins") return RedirectToAction("Logins", "Login");
            ViewBag.Message = "Nhập thông tin";
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var parents = from m in context.Menus where (m.Parent == 0) select m;
            ViewBag.parent = 0;
            ViewBag.idnew = (context.Menus.Max(n => n.ID_MN) + 1).ToString();
            ViewBag.parents = parents;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddMenuParent(FormCollection collection)
        {
            string error = "";
            long id = 0;
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            if (!String.IsNullOrEmpty(collection.Get("ID_MN")))
            {
                id = Convert.ToInt64(collection.Get("ID_MN"));
                if (context.Menus.SingleOrDefault(m => m.ID_MN == id) != null) error += "ID_MN đã tồn tại<br/>";
            }
            else
                error += "Chưa nhap ID_MN<br/>";
            string lablel = "";
            if (!string.IsNullOrEmpty(collection.Get("Lablel")))
                lablel = collection.Get("Lablel");
            else
                error += "Chưa nhập Label<br/>";
            byte pos = 0;
            if (!string.IsNullOrEmpty(collection.Get("Pos")))
            {
                if (!byte.TryParse(collection.Get("Pos"), out pos)) error += "Pos phải là số<br/>";
            }
            else
                error += "Chưa nhập Pos<br/>";
            string url = "";
            if (!string.IsNullOrEmpty(collection.Get("UrlLink")))
                url = collection.Get("UrlLink");
            else
                error += "Chưa nhập Url<br/>";
            long parent = 0;
            if (!string.IsNullOrEmpty(error))
                ViewBag.Message = error;
            else
            {
                //Lưu
                Menus menu = new Menus();
                menu.ID_MN = id;
                menu.Lablel = lablel;
                menu.Pos = pos;
                menu.Parent = parent;
                menu.UrlLink = url;
                context.Menus.InsertOnSubmit(menu);
                context.SubmitChanges();
                ViewBag.Message = "Lưu thành công. Bạn có thể thêm nữa";
            }
            ViewBag.idnew = (context.Menus.Max(n => n.ID_MN) + 1).ToString();
            var parents = from m in context.Menus where (m.Parent == 0) select m;
            ViewBag.parent = parent;
            ViewBag.parents = parents;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddMenu()
        {
                ViewBag.Message = "Nhập thông tin";
                NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
                var parents = from m in context.Menus where (m.Parent == 0) select m;
                ViewBag.parent = 0;
                ViewBag.idnew = (context.Menus.Max(n => n.ID_MN) + 1).ToString();
                ViewBag.parents = parents;
                return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddMenu(FormCollection collection)
        {
            string error = "";
            long id = 0;
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            if (!String.IsNullOrEmpty(collection.Get("ID_MN")))
            {
                id = Convert.ToInt64(collection.Get("ID_MN"));
                if (context.Menus.SingleOrDefault(m => m.ID_MN == id) != null) error += "ID_MN đã tồn tại<br/>";
            }
            else
                error += "Chưa nhap ID_MN<br/>";
            string lablel = "";
            if (!string.IsNullOrEmpty(collection.Get("Lablel")))
                lablel = collection.Get("Lablel");
            else
                error += "Chưa nhập Label<br/>";
            byte pos = 0;
            if (!string.IsNullOrEmpty(collection.Get("Pos")))
            {
                if (!byte.TryParse(collection.Get("Pos"), out pos)) error += "Pos phải là số<br/>";
            }
            else
                error += "Chưa nhập Pos<br/>";
            string url = "";
            if (!string.IsNullOrEmpty(collection.Get("UrlLink")))
                url = collection.Get("UrlLink");
            else
                url = "NULL";
            long parent = 0;
            if (!string.IsNullOrEmpty(collection.Get("ID_Parent")))
                parent = Convert.ToInt64(collection.Get("ID_Parent"));
            else
                error += "Chưa chọn menu cha<br/>";
            if (!string.IsNullOrEmpty(error))
                ViewBag.Message = error;
            else
            {
                //Lưu
                Menus menu = new Menus();
                menu.ID_MN = id;
                menu.Lablel = lablel;
                menu.Pos = pos;
                menu.Parent = parent;
                menu.UrlLink = url;
                context.Menus.InsertOnSubmit(menu);
                context.SubmitChanges();
                ViewBag.Message = "Lưu thành công. Bạn có thể thêm nữa";
            }
            ViewBag.idnew = (context.Menus.Max(n => n.ID_MN) + 1).ToString();
            var parents = from m in context.Menus where (m.Parent == 0) select m;
            ViewBag.parent = parent;
            ViewBag.parents = parents;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddMenuAjax()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var parents = from m in context.Menus where (m.Parent == 0) select m;
            ViewBag.parent = 0;
            ViewBag.idnew = (context.Menus.Max(n => n.ID_MN) + 1).ToString();
            ViewBag.parents = parents;
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public string AddMenuAjax(FormCollection collection)
        {
            string error = "";
            string result = "";
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            long id = 0;
            if (!string.IsNullOrEmpty(collection.Get("ID_MN")))
            {
                id = Convert.ToInt64(collection.Get("ID_MN"));
                if (context.Menus.SingleOrDefault(m => m.ID_MN == id) != null) error += "ID_MN đã tồn tại <br/>";
            }
            else
                error += "chưa nhập ID_MN <br/>";
            string lablel = "";
            if (!string.IsNullOrEmpty(collection.Get("Lablel")))
                lablel = collection.Get("Lablel");
            else
                error += "Chưa nhập Lable<br/>";
            byte pos = 0;
            if (!string.IsNullOrEmpty(collection.Get("Pos")))
            {
                if (!byte.TryParse(collection.Get("Pos"), out pos)) error += "Pos phải là số<br/>";
            }
            else
                error += "Chưa nhập Pos<br/>";
            string url = "";
            if (!string.IsNullOrEmpty(collection.Get("UrlLink")))
                url = collection.Get("UrlLink");
            else
                error += "Chưa nhập Url<br/>";
            long parent = 0;
            if (!string.IsNullOrEmpty(collection.Get("ID_Parent")))
                parent = Convert.ToInt64(collection.Get("ID_Parent"));
            else
                error += "Chưa chọn menu cha<br/>";
            if (!string.IsNullOrEmpty(error))
                result = error;
            else
            {
                Menus menu = new Menus();
                menu.ID_MN = id;
                menu.Lablel = lablel;
                menu.Pos = pos;
                menu.Parent = parent;
                menu.UrlLink = url;
                context.Menus.InsertOnSubmit(menu);
                context.SubmitChanges();
                result = "Lưu thành công. Bạn có thể thêm nữa";
            }
            ViewBag.idnew = (context.Menus.Max(n => n.ID_MN) + 1).ToString();
            var parents = from m in context.Menus where (m.Parent == 0) select m;
            ViewBag.parent = parent;
            ViewBag.parents = parents;
            return result;
        }
    }
}