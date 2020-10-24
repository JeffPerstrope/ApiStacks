using ApiStacks_API.Helpers;
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
            var authorizeAPI = APIValidate.AuthorizeRequest(value);
            if (authorizeAPI == "success")
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                var queryString = value.GetQueryNameValuePairs();
                foreach (var parameter in queryString)
                {
                    var key = parameter.Key.ToLower();
                    var val = parameter.Value.ToLower();
                    parameters.Add(key, val);
                }

                if (!parameters.ContainsKey("url"))
                {
                    return APICall.ReturnFormattingError();
                }

                //Validate URL Structure
                if ((!parameters["url"].StartsWith("http://")) && (!parameters["url"].StartsWith("https://")))
                    parameters["url"] = "http://" + parameters["url"];

                var screenshotResponse = APICall.call(APICall.API_Screenshot, parameters["url"]);
                if (screenshotResponse != null)
                {
                    var incrementUsage = APIValidate.IncrementUsageRequest(value);
                    if (incrementUsage == "success")
                    {
                        return new HttpResponseMessage()
                        {
                            Content = new StringContent(screenshotResponse, System.Text.Encoding.UTF8, "application/json")
                        };
                    }
                    else
                    {
                        return APICall.ReturnAPIAuthorizationError(incrementUsage);
                    }
                }
                else
                {
                    return APICall.ReturnFormattingError();
                }
            }
            else
            {
                return APICall.ReturnAPIAuthorizationError(authorizeAPI);
            }

        }

    }
}