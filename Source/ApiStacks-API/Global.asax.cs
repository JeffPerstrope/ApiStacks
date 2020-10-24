using ApiStacks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                name: "getscreenshot",
                routeTemplate: "v1/getscreenshot/{*catchall}",
                defaults: new { controller = "webscreenshots" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "getpdf",
                routeTemplate: "v1/getpdf/{*catchall}",
                defaults: new { controller = "webpdf" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "scrapelinks",
                routeTemplate: "v1/scrapelinks/{*catchall}",
                defaults: new { controller = "linkscraper" }
            );

            RouteTable.Routes.MapHttpRoute(
               name: "scrapewebsite",
               routeTemplate: "v1/scrapewebsite/{*catchall}",
               defaults: new { controller = "webscraper" }
           );

            RouteTable.Routes.MapHttpRoute(
                name: "getmeta",
                routeTemplate: "v1/getmeta/{*catchall}",
                defaults: new { controller = "opengraph" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "getwhois",
                routeTemplate: "v1/getwhois/{*catchall}",
                defaults: new { controller = "whois" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "emailvalidate",
                routeTemplate: "v1/checkemail/{*catchall}",
                defaults: new { controller = "emailvalidator" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "generateqr",
                routeTemplate: "v1/generateqr/{*catchall}",
                defaults: new { controller = "qrcode" }
            );

            RouteTable.Routes.MapHttpRoute(
                name: "detectlanguage",
                routeTemplate: "v1/detectlanguage/{*catchall}",
                defaults: new { controller = "languagedetect" }
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
