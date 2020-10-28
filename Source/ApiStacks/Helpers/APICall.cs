using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace ApiStacks
{
    public static class APICall
    {
        //public static string API_Screenshot = "https://apistacks-webscreenshot.glitch.me/getscreenshot?url=";
        //public static string API_OpenGraph = "https://apistacks-opengraph.glitch.me/getopengraph?url=";
        //public static string API_Whois = "https://apistacks-whois.glitch.me/getwhois?url=";
        //public static string API_IPLocation = "https://apistacks-whois.glitch.me/getiplocation?url=";
        //public static string API_LinkScraper = "https://apistacks-linkscraper.glitch.me/geturls?url=";
        //public static string API_WebScraper = "https://apistacks-webscraper.glitch.me/getwebsite?url=";
        //public static string API_EmailValidator = "https://apistacks-emailvalidate.glitch.me/validateemail?url=";
        //public static string API_QRCode = "https://apistacks-qrcodegen.glitch.me/generateqr?url=";
        //public static string API_LanguageDetect = "https://apistacks-languagedetect.glitch.me/detectlanguage?url=";

        public static string API_Screenshot = "https://us-central1-apistacks-webscreenshot.cloudfunctions.net/getscreenshot?url=";
        public static string API_PDF = "https://us-central1-apistacks-webpdf.cloudfunctions.net/getpdf?pages=1&url=";
        public static string API_LinkScraper = "https://api.apistacks.com/v1/scrapelinks?url=";
        public static string API_Whois = "https://api.apistacks.com/v1/getwhois?url=";
        public static string API_WebScraper = "https://api.apistacks.com/v1/scrapewebsite?url=";
        public static string API_OpenGraph = "https://api.apistacks.com/v1/getmeta?url=";
        public static string API_EmailValidator = "https://api.apistacks.com/v1/checkemail?url=";
        public static string API_QRCode = "https://api.apistacks.com/v1/generateqr?url=";
        public static string API_LanguageDetect = "https://api.apistacks.com/v1/detectlanguage?url=";

        public static string call(string endpoint, string queryString)
        {
            var longEndpoint = endpoint + queryString;

            var request = (HttpWebRequest)WebRequest.Create(longEndpoint);
            request.Method = "GET";
            request.Headers.Add("x-api-key", "9f61ed46-1214-4a95-b17f-eec1b0161994");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                        if (content != null)
                        {
                            return content;
                        }
                    }
                }
            }

            return null;
        }

    }
}