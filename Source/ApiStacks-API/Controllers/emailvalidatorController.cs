using ApiStacks;
using ApiStacks_API.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace ApiStacks_API.Controllers
{
    public class emailvalidatorController : ApiController
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

                //Check if email is valid
                bool isEmail = Regex.IsMatch(parameters["url"].ToString(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isEmail == false)
                {
                    return APICall.ReturnFormattingError();
                }

                var screenshotResponse = APICall.call(APICall.API_EmailValidator, parameters["url"]);
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