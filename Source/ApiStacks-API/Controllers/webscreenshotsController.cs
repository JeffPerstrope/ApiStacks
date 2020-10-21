using Newtonsoft.Json;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace ApiStacks_API.Controllers
{
    public class webscreenshotsController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage value)
        {
            //Take screenshot
            var result = TakeScreenshot().Result;

            var sampleResponse = new { key1 = "value1", key2 = "value2" };
            var responseValue = JsonConvert.SerializeObject(sampleResponse);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, responseValue);

            return response;

            //return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        private async Task<string> TakeScreenshot()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory + @"chrome";
            //var browserFetcherOptions = new BrowserFetcherOptions { Path = dir + "/chrome/" };
            //var browserFetcher = new BrowserFetcher(browserFetcherOptions);
            //await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
            //await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = dir 
            });
            var page = await browser.NewPageAsync();
            await page.GoToAsync("http://www.google.com");
            await page.ScreenshotAsync("/capture.png");
            return "";
        }

    }
}