﻿using Newtonsoft.Json;
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
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            var queryString = value.GetQueryNameValuePairs();
            foreach (var parameter in queryString)
            {
                var key = parameter.Key;
                var val = parameter.Value;
                parameters.Add(key, val);
            }

            if (!parameters.ContainsKey("url"))
            {
                return APICall.ReturnFormattingError();
            }

            var screenshotResponse = APICall.call(APICall.API_Screenshot, parameters["url"]);
            if (screenshotResponse != null)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent(screenshotResponse, System.Text.Encoding.UTF8, "application/json")
                };
            }
            else
            {
                return APICall.ReturnFormattingError();
            }
        }

    }
}