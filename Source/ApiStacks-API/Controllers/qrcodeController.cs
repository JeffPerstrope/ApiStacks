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
    public class qrcodeController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage value)
        {
            var sampleResponse = new { key1 = "value1", key2 = "value2" };
            var responseValue = JsonConvert.SerializeObject(sampleResponse);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, responseValue);

            return response;

            //return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

    }
}