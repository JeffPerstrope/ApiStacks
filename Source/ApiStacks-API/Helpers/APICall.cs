﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace ApiStacks_API
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




        //public static string API_Screenshot = "http://api.apistacks.com/v1/getscreenshot?url=";
        //public static string API_PDF = "http://api.apistacks.com/v1//getpdf?pages=1&url=";
        //public static string API_LinkScraper = "http://api.apistacks.com/v1/scrapelinks?url=";
        //public static string API_Whois = "http://api.apistacks.com/v1/getwhois?url=";
        //public static string API_WebScraper = "http://api.apistacks.com/v1/scrapewebsite?url=";
        //public static string API_OpenGraph = "http://api.apistacks.com/v1/getmeta?url=";
        //public static string API_EmailValidator = "http://api.apistacks.com/v1/checkemail?url=";
        //public static string API_QRCode = "http://api.apistacks.com/v1/generateqr?url=";
        //public static string API_LanguageDetect = "http://api.apistacks.com/v1/detectlanguage?url=";

        public static string API_Screenshot = "https://us-central1-apistacks-webscreenshot.cloudfunctions.net/getscreenshot?url=";
        public static string API_PDF = "https://us-central1-apistacks-webpdf.cloudfunctions.net/getpdf?pages=1&url=";
        public static string API_LinkScraper = "https://us-central1-apistacks-basicapps.cloudfunctions.net/scrapelinks?url=";
        public static string API_Whois = "https://us-central1-apistacks-basicapps.cloudfunctions.net/getwhois?url=";
        public static string API_WebScraper = "https://us-central1-apistacks-basicapps.cloudfunctions.net/scrapewebsite?url=";
        public static string API_OpenGraph = "https://us-central1-apistacks-basicapps.cloudfunctions.net/getmeta?url=";
        public static string API_EmailValidator = "https://us-central1-apistacks-basicapps.cloudfunctions.net/checkemail?url=";
        public static string API_QRCode = "https://us-central1-apistacks-basicapps.cloudfunctions.net/generateqr?url=";
        public static string API_LanguageDetect = "https://us-central1-apistacks-basicapps.cloudfunctions.net/detectlanguage?url=";

        public static string call(string endpoint, string queryString)
        {

            var longEndpoint = endpoint + queryString;

            var request = (HttpWebRequest)WebRequest.Create(longEndpoint);
            request.Method = "GET";
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
                            //string jsonString = JsonConvert.SerializeObject(content, Formatting.Indented);
                            //content = content.Replace(@"\", "").Replace("\"", "");
                            return content;
                        }
                    }
                }
            }

            return null;
        }

        public static HttpResponseMessage ReturnFormattingError()
        {
            var failureResponse = new
            {
                status = "failed",
                timestamp = DateTime.Now,
                reason = "request was not formatted properly"
            };
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(failureResponse), System.Text.Encoding.UTF8, "application/json")
            };
        }

        public static HttpResponseMessage ReturnAPIAuthorizationError(string reason)
        {
            var failureResponse = new
            {
                status = "failed",
                timestamp = DateTime.Now,
                reason = reason
            };
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(failureResponse), System.Text.Encoding.UTF8, "application/json")
            };
        }
    }
}