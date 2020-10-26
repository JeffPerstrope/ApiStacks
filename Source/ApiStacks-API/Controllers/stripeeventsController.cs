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
using Stripe;
using Stripe.Checkout;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpFireStarter;

namespace ApiStacks_API.Controllers
{
    public class stripeeventsController : ApiController
    {
        private static FireBaseDB db = new FireBaseDB("apistacks-basicapps", "https://apistacks-basicapps.firebaseio.com", "AIzaSyBkCHXwY87S0ZQxo6T1jNLbxYCaizgMnsU");

        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage value)
        {
            //Get post values
            var content = value.Content;
            string jsonContent = content.ReadAsStringAsync().Result;

            var eventData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);

            if (eventData != null && eventData.Count > 0 && eventData.ContainsKey("type"))
            {
                var dataLayer = ((JObject)eventData["data"]).ToObject<Dictionary<string, JObject>>();
                var wantedData = ((JObject)dataLayer["object"]).ToObject<Dictionary<string, object>>();

                var customerID = wantedData["customer"].ToString();
                var customerEmail = wantedData["customer_email"].ToString();

                if (eventData["type"].ToString() == "invoice.paid")
                {

                }
                else
                {

                }
            }
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}