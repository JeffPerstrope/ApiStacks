using ApiStacks;
using ApiStacks_API.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace ApiStacks_API.Controllers
{
    public class keepAliveController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage value)
        {
            //Check if app is enabled
            var completeURL = value.RequestUri.ToString();
            var trailingURL = completeURL.Split('?').Last();

            if(trailingURL == "4ab6f3b3-2bae-4715-a58e-53c1efd96efc")
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
    }
}