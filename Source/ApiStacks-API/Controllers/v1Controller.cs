using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ApiStacks_API.Controllers
{
    public class v1Controller : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage value)
        {
            //var key = value.GetQueryNameValuePairs().Where(m => m.Key == "apiKey").SingleOrDefault().Value;
            //if (key == apiKey)
            //{
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            //}

            return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }

    }
}