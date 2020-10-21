using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ApiStacks_API.Controllers
{
    public class webdpfController
    {
        [SessionState(SessionStateBehavior.ReadOnly)]
        public class webpdfController : ApiController
        {

            [System.Web.Http.HttpGet]
            public HttpResponseMessage Get(HttpRequestMessage value)
            {
                var sampleResponse = new { key1 = "value1", key2 = "value2" };
                var responseValue = JsonConvert.SerializeObject(sampleResponse);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, responseValue);
                Thread.Sleep(3000);
                return response;

                //return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

        }
    }
}