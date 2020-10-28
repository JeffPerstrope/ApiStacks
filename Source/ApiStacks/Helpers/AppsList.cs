using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStacks
{
    public class AppsList
    {
        public static List<Model_ApiApp> InstalledApps = new List<Model_ApiApp>();

        public static void PopulateApps()
        {
            #region ALL APPS

            if (InstalledApps.Count <= 0)
            {
                var app1 = new Model_ApiApp
                {
                    id = "getscreenshot",
                    name = "Web Screenshots",
                    endpoint = "getscreenshot",
                    icon = "/images/app/webscreenshots.png",
                    enabled = false,
                    version = "0.1.3 BETA",
                    customization = new List<string>()
                    {
                        "quality :\t [0 ... 9]",
                        "scaleFactor :\t [1 ... 3]",
                        "fullscreen :\t [boolean]",
                        "emulateDevice :\t [string]"
                    }
                };

                var app2 = new Model_ApiApp
                {
                    id = "getpdf",
                    name = "Web PDF",
                    endpoint = "getpdf",
                    icon = "/images/app/webpdf.png",
                    enabled = false,
                    version = "0.1.3 BETA",
                    customization = new List<string>()
                    {
                        "quality :\t [0 ... 9]",
                        "width :\t [pixels]",
                        "heigt :\t [pixels]"
                    }
                };

                var app3 = new Model_ApiApp
                {
                    id = "scrapelinks",
                    name = "Link Scraper",
                    endpoint = "scrapelinks",
                    icon = "/images/app/linkscraper.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app4 = new Model_ApiApp
                {
                    id = "checkemail",
                    name = "Email Checker",
                    endpoint = "checkemail",
                    icon = "/images/app/emailscraper.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app5 = new Model_ApiApp
                {
                    id = "getmeta",
                    name = "Open Graph",
                    endpoint = "getmeta",
                    icon = "/images/app/opengraph.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app6 = new Model_ApiApp
                {
                    id = "getwhois",
                    name = "WHOIS Lookup",
                    endpoint = "getwhois",
                    icon = "/images/app/whois.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app7 = new Model_ApiApp
                {
                    id = "scrapewebsite",
                    name = "Web Scraper",
                    endpoint = "scrapewebsite",
                    icon = "/images/app/webscraper.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app8 = new Model_ApiApp
                {
                    id = "detectlanguage",
                    name = "Language Detect",
                    endpoint = "detectlanguage",
                    icon = "/images/app/languagedetect.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app9 = new Model_ApiApp
                {
                    id = "generateqr",
                    name = "QR Code Generator",
                    endpoint = "generateqr",
                    icon = "/images/app/qrcode.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                InstalledApps.Add(app1);
                InstalledApps.Add(app2);
                InstalledApps.Add(app3);
                InstalledApps.Add(app4);
                InstalledApps.Add(app5);
                InstalledApps.Add(app6);
                InstalledApps.Add(app7);
                InstalledApps.Add(app8);
                InstalledApps.Add(app9);
            }
            #endregion
        }

        private static void PopulateAppSettings()
        {
            var app1 = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("High Definition", true),
                new KeyValuePair<string, object>("High Definition", true),
            };
               
            lock (InstalledApps)
            {
                foreach(var app in InstalledApps.ToArray())
                {

                }
            }
        }


        public static void activateApp(string id)
        {
            lock (InstalledApps)
            {
                foreach (var app in InstalledApps.ToArray())
                {
                    if (app.id == id)
                    {
                        app.enabled = true;
                        break;
                    }
                }
            }
        }

        public static void deactivateApp(string id)
        {
            lock (InstalledApps)
            {
                foreach (var app in InstalledApps.ToArray())
                {
                    if (app.id == id)
                    {
                        app.enabled = false;
                        break;
                    }
                }
            }
        }
    }
}