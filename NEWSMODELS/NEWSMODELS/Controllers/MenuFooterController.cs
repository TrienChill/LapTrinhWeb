using NEWSMODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NEWSMODELS.Controllers
{
    public class MenuFooterController : Controller
    {
        // GET: MenuFooter
        public string menuMainHoziontalFooter()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menusfooter = from m in context.Menu_Footers.Where(m => m.ParentID == 0) select m;
            if (menusfooter != null)
            {
                string listMenu = " <ul class='menufooter'>";
                foreach (Menu_Footer m in (menusfooter as IEnumerable<Menu_Footer>))
                {
                    listMenu = listMenu + "<li><a href= #"
                                        + ">" + m.TitleFooter + "</a>";
                    listMenu = listMenu + submenuMain1HoziontalFooter(Convert.ToInt64(m.ID_Footer)) + "</li>";
                }
                return listMenu + "</ul";
            }
            else
            {
                return "";
            }
        }
        protected string submenuMain1HoziontalFooter(long id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menusfooter = from m in context.Menu_Footers.Where(m => m.ParentID == id) select m;
            if (menusfooter != null)
            {
                string listMenu = "";
                foreach (Menu_Footer m in (menusfooter as IEnumerable<Menu_Footer>))
                {
                    listMenu = listMenu + "<li><a href= https://localhost:44390/NewsPage/ViewFooter/"
                                        + m.ID_Footer + ">" + m.TitleFooter + "</a>";
                }
                return (listMenu.Length == 0) ? listMenu : "<ul>" + listMenu + "</ul>";
            }
            else
            {
                return "";
            }
        }
    }
}