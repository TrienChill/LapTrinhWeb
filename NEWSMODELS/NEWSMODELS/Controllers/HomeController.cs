using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEWSMODELS.Models;
using PagedList;

namespace NEWSMODELS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //NewsDataContext context = new NewsDataContext();
        public ActionResult Index(int? id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var pages = from p in context.PageItems.OrderBy(p => p.ID_P) select p;
            int pagesize = 2;
            int pageindex = id ?? 1;
            return View(pages.ToPagedList(pageindex, pagesize));
        }
        //public ActionResult EditMenu()
        //{
        //    NewsDataContext context = new NewsDataContext();
        //    int id = 3;
        //    Menus menus = context.Menus.Single(n => n.ID_MN == id);
        //    menus.Lablel = "Đây là menu đã sửa url";
        //    menus.UrlLink = "http://dthu.edu.vn";/*Có thể sử thuộc tính khác*/
        //    context.SubmitChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult StronglyType()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menus = from m in context.Menus select m;
            return View(menus.ToList());
        }
        public ActionResult DynamicType()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menus = from m in context.Menus select m;
            return View(menus.ToList());
        }
        public ActionResult Viewbag()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menus = from m in context.Menus select m;
            ViewBag.Menus = menus;
            return View();
        }
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult MenuList()
        //{
        //    NewsDataContext context = new NewsDataContext();
        //    var patents = from p in context.Menus where (p.Parent == 0) select p;
        //    ViewBag.parents = patents;
        //    ViewBag.parent = patents.First().ID_MN;
        //    long id = ViewBag.parent;
        //    var menus = from m in context.Menus.OrderBy(m => m.ID_MN) where (m.Parent == id) select m;
        //    ViewBag.menus = menus;
        //    return View();

        //}
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult MenuList(FormCollection collection)
        //{
        //    NewsDataContext context = new NewsDataContext();
        //    var parents = from p in context.Menus where (p.Parent == 0) select p;
        //    ViewBag.parents = parents;
        //    ViewBag.parent = collection.Get("ID_MN");
        //    long id = Convert.ToInt64(ViewBag.parent);
        //    var menus = from m in context.Menus.OrderBy(m => m.ID_MN) where (m.Parent == id) select m;
        //    ViewBag.menus = menus;
        //    return View();
        //}
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult ViewMenu(int id)
        //{
        //    NewsDataContext context = new NewsDataContext();
        //    var menu = context.Menus.Single(m => m.ID_MN == id);
        //    return View(menu);
        //}
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult EditMenu(int id)
        //{
        //    NewsDataContext context = new NewsDataContext();
        //    var menu = context.Menus.Single(m => m.ID_MN == id);
        //    ViewBag.parent = menu.Parent;
        //    var parents = from m in context.Menus where (m.Parent == 0) select m;
        //    ViewBag.parents = parents;
        //    return View(menu);
        //}
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditMenu(FormCollection collection)
        //{
        //    NewsDataContext context = new NewsDataContext();
        //    int id = Convert.ToInt32(collection.Get("ID_MN"));
        //    string lablel = collection.Get("Lablel");
        //    byte pos = Convert.ToByte(collection.Get("pos"));
        //    long parent = Convert.ToInt64(collection.Get("ID_Parent"));
        //    string url = collection.Get("UrlLink");
        //    Menus menu = context.Menus.Single(m => m.ID_MN == id);
        //    menu.Lablel = lablel;
        //    menu.Pos = pos;
        //    menu.Parent = parent;
        //    menu.UrlLink = url;
        //    context.SubmitChanges();
        //    return RedirectToAction("MenuList");
        //}
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult DeleteMenu(int id)
        //{
        //    NewsDataContext context = new NewsDataContext();
        //    Menus menus = context.Menus.Single(n => n.ID_MN == id);
        //    context.Menus.DeleteOnSubmit(menus);
        //    context.SubmitChanges();
        //    return RedirectToAction("MenuList");
        //}
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult AddMenu()
        //{
        //    ViewBag.Message = "Nhập thông tin";
        //    NewsDataContext context = new NewsDataContext();
        //    var parents = from m in context.Menus where (m.Parent == 0) select m;
        //    /*ViewBag.parent = parents.First().ID_MN;*/
        //    ViewBag.parent = 0;
        //    ViewBag.idnew = (context.Menus.Max(n => n.ID_MN) + 1).ToString();
        //    ViewBag.parents = parents;
        //    return View();
        //}
        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult AddMenu(FormCollection collection)
        //{
        //    String error = "";
        //    long id = 0;
        //    NewsDataContext context = new NewsDataContext();
        //    if (!String.IsNullOrEmpty(collection.Get("ID_MN")))
        //    {
        //        id = Convert.ToInt64(collection.Get("ID_MN"));
        //        if (context.Menus.SingleOrDefault(m => m.ID_MN == id) != null) error += "ID_MN đã tồn tại<br/>";
        //    }
        //    else
        //        error += "Chưa nhap ID_MN<br/>";
        //    string lablel = "";
        //    if (!string.IsNullOrEmpty(collection.Get("Lablel")))
        //        lablel = collection.Get("Lablel");
        //    else
        //        error += "Chưa nhập Lable<br/>";
        //    byte pos = 0;
        //    if (!string.IsNullOrEmpty(collection.Get("Pos")))
        //    {
        //        if (!byte.TryParse(collection.Get("Pos"), out pos)) error += "Pos phải là số<br/>";
        //    }
        //    else
        //        error += "Chưa nhập Pos<br/>";
        //    string url = "";
        //    if (!string.IsNullOrEmpty(collection.Get("UrlLink")))
        //        url = collection.Get("UrlLink");
        //    else
        //        error += "Chưa nhập Url<br/>";
        //    long parent = 0;
        //    if (!string.IsNullOrEmpty(collection.Get("ID_Parent")))
        //        parent = Convert.ToInt64(collection.Get("ID_Parent"));
        //    else
        //        error += "Chưa chọn menu cha<br/>";
        //    if (!string.IsNullOrEmpty(error))
        //        ViewBag.Message = error;
        //    else
        //    {
        //        //Lưu
        //        Menus menu = new Menus();
        //        menu.ID_MN = id;
        //        menu.Lablel = lablel;
        //        menu.Pos = pos;
        //        menu.Parent = parent;
        //        menu.UrlLink = url;
        //        context.Menus.InsertOnSubmit(menu);
        //        context.SubmitChanges();
        //        ViewBag.Message = "Lưu thành công. Bạn có thể thêm nữa";
        //    }
        //    ViewBag.idnew = (context.Menus.Max(n => n.ID_MN) + 1).ToString();
        //    var parents = from m in context.Menus where (m.Parent == 0) select m;
        //    ViewBag.parent = parent;
        //    ViewBag.parents = parents;
        //    return View();

        //    //-----------------------------------------------------------
        //}
        //[AcceptVerbs(HttpVerbs.Get)]
    //    public ActionResult PageItemList()
    //    {
    //        NewsDataContext context = new NewsDataContext();
    //        var orderkeys = from p in context.PageItems where (p.OrderKey == 0) select p;
    //        ViewBag.orderkeys = orderkeys;
    //        ViewBag.orderkey = orderkeys.First().ID_P;
    //        long id = ViewBag.orderkey;
    //        var pageitems = from m in context.PageItems.OrderBy(m => m.ID_P) where (m.OrderKey == id) select m;
    //        ViewBag.pageitems = pageitems;
    //        return View();
    //    }
    //    [AcceptVerbs(HttpVerbs.Post)]
    //    public ActionResult PageItemList(FormCollection collection)
    //    {
    //        NewsDataContext context = new NewsDataContext();
    //        var oderkeys = from p in context.PageItems where (p.OrderKey == 0) select p;
    //        ViewBag.orderkeys = oderkeys;
    //        ViewBag.orderkey = collection.Get("ID_P");
    //        long id = Convert.ToInt64(ViewBag.orderkey);
    //        var pageitems = from m in context.PageItems.OrderBy(m => m.ID_P) where (m.OrderKey == id) select m;
    //        ViewBag.pageitems = pageitems;
    //        return View();
    //    }

    //    [AcceptVerbs(HttpVerbs.Get)]
    //    public ActionResult EditPageItem(int id)
    //    {
    //        NewsDataContext context = new NewsDataContext();
    //        var page = context.PageItems.Single(m => m.ID_P == id);
    //        ViewBag.orderkey = page.OrderKey;
    //        var oderkeys = from m in context.PageItems where (m.OrderKey == 1) select m;
    //        ViewBag.orderkeys = oderkeys;
    //        return View(page);
    //    }
    //    [AcceptVerbs(HttpVerbs.Post)]
    //    public ActionResult EditPageItem(FormCollection collection)
    //    {
    //        NewsDataContext context = new NewsDataContext();
    //        int id = Convert.ToInt32(collection.Get("ID_P"));
    //        string title = collection.Get("Title");
    //        string content = collection.Get("Contents");
    //        long orderkey = Convert.ToInt32(collection.Get("OrderKey"));
    //        string useradd = collection.Get("UserAdd");
    //        PageItem page = context.PageItems.Single(m => m.ID_P == id);
    //        page.Title = title;
    //        page.Contents = content;
    //        page.OrderKey = orderkey;
    //        page.UserAdd = useradd;
    //        context.SubmitChanges();
    //        return RedirectToAction("PageItemList");
    //    }

    //    [AcceptVerbs(HttpVerbs.Get)]
    //    public ActionResult ViewPageItem(int id)
    //    {
    //        NewsDataContext context = new NewsDataContext();
    //        var page = context.PageItems.Single(m => m.ID_P == id);
    //        return View(page);
    //    }


    //    [AcceptVerbs(HttpVerbs.Get)]
    //    public ActionResult AddPageItem()
    //    {
    //        //ViewBag.Message = "Nhập thông tin";
    //        //NewsDataContext context = new NewsDataContext();
    //        //var orderkeys = from m in context.PageItems where (m.OrderKey == 1) select m;
    //        ///*ViewBag.parent = parents.First().ID_MN;*/
    //        //ViewBag.orderkey = 0;
    //        //ViewBag.idnew = (context.PageItems.Max(n => n.ID_P) + 1).ToString();
    //        //ViewBag.orderkeys = orderkeys;
    //        //return View();
    //        NewsDataContext context = new NewsDataContext();
    //        var orderkeys = from m in context.PageItems where (m.OrderKey == 0) select m;
    //        ViewBag.orderkey = orderkeys.First().ID_P;
    //        ViewBag.idnew = (context.PageItems.Max(n => n.ID_P) + 1).ToString();
    //        ViewBag.orderkeys = orderkeys;
    //        return View();

    //    }
    //    [AcceptVerbs(HttpVerbs.Post)]
    //    public ActionResult AddPageItem(FormCollection collection)
    //    {
    //        //String error = "";
    //        //long id = 0;
    //        //NewsDataContext context = new NewsDataContext();
    //        //if (!String.IsNullOrEmpty(collection.Get("ID_P")))
    //        //{
    //        //    id = Convert.ToInt64(collection.Get("ID_P"));
    //        //    if (context.PageItems.SingleOrDefault(m => m.ID_P == id) != null) error += "ID_P đã tồn tại<br/>";
    //        //}
    //        //else
    //        //    error += "Chưa nhap ID_P<br/>";
    //        //string title = "";
    //        //if (!string.IsNullOrEmpty(collection.Get("Title")))
    //        //    title = collection.Get("Title");
    //        //else
    //        //    error += "Chưa nhập Title<br/>";
    //        //string content = "";
    //        //if (!string.IsNullOrEmpty(collection.Get("Contents")))
    //        //    content = collection.Get("Contents");
    //        //else
    //        //    error += "Chưa nhập Contents<br/>";
    //        //string useradd = "";
    //        //if (!string.IsNullOrEmpty(collection.Get("UserAdd")))
    //        //    useradd = collection.Get("UserAdd");
    //        //else
    //        //    error += "Chưa nhập UserAdd<br/>";
    //        //long orderkey = 0;
    //        //if (!string.IsNullOrEmpty(collection.Get("OrderKey")))
    //        //    orderkey = Convert.ToInt64(collection.Get("OrderKey"));
    //        //else
    //        //    error += "Chưa nhập OrderKey<br/>";
    //        //if (!string.IsNullOrEmpty(error))
    //        //    ViewBag.Message = error;
    //        //else
    //        //{
    //        //    //Lưu
    //        //    PageItem page = new PageItem();
    //        //    page.ID_P = id;
    //        //    page.Title = title;
    //        //    page.Contents = content;
    //        //    page.UserAdd = useradd;
    //        //    context.PageItems.InsertOnSubmit(page);
    //        //    context.SubmitChanges();
    //        //    ViewBag.Message = "Lưu thành công. Bạn có thể thêm nữa";
    //        //}
    //        //ViewBag.idnew = (context.PageItems.Max(n => n.ID_P) + 1).ToString();
    //        //var orderkeys = from m in context.PageItems where (m.OrderKey == 0) select m;
    //        //ViewBag.orderkey = orderkey;
    //        //ViewBag.orderkeys = orderkeys;
    //        //return View();

    //        ////-----------------------------------------------------------
    //        NewsDataContext context = new NewsDataContext();
    //        int id = Convert.ToInt32(collection.Get("ID_P"));
    //        string title = collection.Get("Title");
    //        string content = collection.Get("Contents");
    //        long orderkey = Convert.ToInt64(collection.Get("OrderKey"));
    //        string useradd = collection.Get("UserAdd");
    //        PageItem page = new PageItem();
    //        page.ID_P = id;
    //        page.Title = title;
    //        page.Contents = content;
    //        page.OrderKey = orderkey;
    //        page.UserAdd = useradd;
    //        context.PageItems.InsertOnSubmit(page);
    //        context.SubmitChanges();

    //        var orderkeys = from m in context.PageItems where (m.OrderKey == 0) select m;
    //        ViewBag.orderkey = orderkey;
    //        ViewBag.idnew = (context.PageItems.Max(n => n.ID_P) + 1).ToString();
    //        ViewBag.orderkeys = orderkeys;
    //        return View();

    //    }

    //    [AcceptVerbs(HttpVerbs.Get)]
    //    public ActionResult DeletePageItem(int id)
    //    {
    //        NewsDataContext context = new NewsDataContext();
    //        PageItem page = context.PageItems.Single(n => n.ID_P == id);
    //        context.PageItems.DeleteOnSubmit(page);
    //        context.SubmitChanges();
    //        return RedirectToAction("PageItemList");
    //    }
    }
}

