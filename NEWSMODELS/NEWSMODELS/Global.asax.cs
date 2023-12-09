using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace NEWSMODELS
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
            Application.Add("online", 0);
            Application.Add("Visit", 0);
        }
        protected void Session_Start()
        {
            Session.Add("username", "Logins");
            Session.Add("fullname", "Logins");
            Session.Add("permission", "0");
            Session.Add("menu", "");
            Session.Add("menus", "");
            Session.Add("menufooter", "");

        }
    }
}