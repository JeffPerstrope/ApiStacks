using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace ApiStacks_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();

            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "",
                defaults: new { controller = "Default" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "webscreenshots",
                routeTemplate: "api/v1/webscreenshots/{*catchall}",
                defaults: new { controller = "webscreenshots" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "webpdf",
                routeTemplate: "api/v1/webpdf/{*catchall}",
                defaults: new { controller = "webpdf" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "linkscraper",
                routeTemplate: "api/v1/linkscraper/{*catchall}",
                defaults: new { controller = "linkscraper" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "opengraph",
                routeTemplate: "api/v1/opengraph/{*catchall}",
                defaults: new { controller = "opengraph" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "whois",
                routeTemplate: "api/v1/whois/{*catchall}",
                defaults: new { controller = "whois" }
            );

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

        }

        protected void Application_PostAuthorizeRequest()
        {
            // WebApi SessionState
            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            if (routeData != null && routeData.RouteHandler is HttpControllerRouteHandler)
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
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
    }
}
