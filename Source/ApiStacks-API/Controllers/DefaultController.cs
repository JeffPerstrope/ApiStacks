using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ApiStacks_API.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage value)
        {
            return new HttpResponseMessage(HttpStatusCode.Forbidden);
        }
    }
}