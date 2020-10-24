﻿using System;
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
                    id = "1",
                    name = "Web Screenshots",
                    endpoint = "webscreenshots",
                    icon = "/images/app/webscreenshots.png",
                    enabled = true,
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
                    id = "2",
                    name = "Web PDF",
                    endpoint = "webpdf",
                    icon = "/images/app/webpdf.png",
                    enabled = true,
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
                    id = "3",
                    name = "Link Scraper",
                    endpoint = "linkscraper",
                    icon = "/images/app/linkscraper.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app4 = new Model_ApiApp
                {
                    id = "4",
                    name = "Email Scraper",
                    endpoint = "emailscraper",
                    icon = "/images/app/emailscraper.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app5 = new Model_ApiApp
                {
                    id = "5",
                    name = "Open Graph",
                    endpoint = "opengraph",
                    icon = "/images/app/opengraph.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app6 = new Model_ApiApp
                {
                    id = "6",
                    name = "WHOIS Lookup",
                    endpoint = "whois",
                    icon = "/images/app/whois.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app7 = new Model_ApiApp
                {
                    id = "7",
                    name = "Web Scraper",
                    endpoint = "webscraper",
                    icon = "/images/app/webscraper.png",
                    enabled = false,
                    version = "0.1.3 BETA"
                };

                var app8 = new Model_ApiApp
                {
                    id = "8",
                    name = "Language Detect",
                    endpoint = "languagedetect",
                    icon = "/images/app/languagedetect.png",
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