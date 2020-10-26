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
        public static string databaseURL = "https://apistacks-basicapps.firebaseio.com";
        public static string appID = "apistacks-basicapps";
        public static string appKey = "AIzaSyBkCHXwY87S0ZQxo6T1jNLbxYCaizgMnsU";

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