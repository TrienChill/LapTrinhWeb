using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NEWSMODELS.Models;

namespace NEWSMODELS.Controllers
{
    public class MenuProviderController : Controller
    {
        // GET: MenuProviderr
        public string menuMainHoziontal()
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menus = from m in context.Menus.Where(m => m.Parent == 0) select m;
            if (menus != null)
            {
                string listMenu = " <ul id='menu-doc'>";
                foreach (Menus m in (menus as IEnumerable<Menus>))
                {
                    listMenu = listMenu + "<li><a href= #"
                                        + ">" + m.Lablel + "</a>";
                    listMenu = listMenu + submenuMain1Hoziontal(Convert.ToInt64(m.ID_MN)) + "</li>";
                }
                return listMenu + "</ul";
            }
            else
            {
                return "";
            }
        }
        protected string submenuMain1Hoziontal(long id)
        {
            NewsDataContext context = new NewsDataContext("Data Source=DESKTOP-00I5VE3\\SQLEXPRESS;Initial Catalog=News;Integrated Security=True;Encrypt=False");
            var menus = from m in context.Menus.Where(m => m.Parent == id) select m;
            if (menus != null)
            {
                string listMenu = "";
                foreach(Menus m in (menus as IEnumerable<Menus>))
                {
                    listMenu = listMenu + "<li><a href= https://localhost:44390/PageItems/Index/1?mn="
                                        + m.ID_MN + ">" + m.Lablel + "</a>";
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