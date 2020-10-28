using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ApiStacks
{
    public class Global : System.Web.HttpApplication
    {
        public static string databaseURL = "https://apistacks-58c16.firebaseio.com";
        public static string appID = "apistacks-58c16";
        public static string appKey = "AIzaSyAPWF83ypScrGhUrQykEjfcWwV2xWW5LT8";

        public static SharpFireStarter.FireBaseDB db;

        protected void Application_Start(object sender, EventArgs e)
        {
            initializeFirebase();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        public static void initializeFirebase()
        {
            db = null;
            db = new SharpFireStarter.FireBaseDB(appID, databaseURL, appKey);
        }
    }

}