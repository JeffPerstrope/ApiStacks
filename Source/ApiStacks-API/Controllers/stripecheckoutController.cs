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

namespace ApiStacks_API.Controllers
{
    public class stripecheckoutController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage value)
        {
            //Determine plan
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var queryString = value.GetQueryNameValuePairs();
            foreach (var parameter in queryString)
            {
                var key = parameter.Key.ToLower();
                var val = parameter.Value.ToLower();
                parameters.Add(key, val);
            }

            if (!parameters.ContainsKey("plan") || (!parameters.ContainsKey("email")))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest); ;
            }

            var priceItem = "";
            var customerEmail = parameters["email"];
            if (parameters["plan"] == "starter")
                priceItem = "price_1HcZm0F6EVrg0l22lVOfn9c8";
            else if (parameters["plan"] == "professional")
                priceItem = "price_1HfvS8F6EVrg0l22KoQlyZp8";
            else if(parameters["plan"] == "ultimate")
                priceItem = "price_1HfwJ3F6EVrg0l22NYrfJDO6";

            StripeConfiguration.ApiKey = "sk_test_51HcZiyF6EVrg0l22KUHmhtN6fxfGQvV1yG2vk2My3Dnq6N4zTg3CASy3OKzArWvWij8CL7BwnqGDPY8xke0Hsmq100FLmHAkYc";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Replace `price_...` with the actual price ID for your subscription
                    // you created in step 2 of this guide.
                    Price = priceItem,
                    Quantity = 1
                  },
                },
                Mode = "subscription",
                CustomerEmail = customerEmail,
                SuccessUrl = "https://apistacks.com/Plan?subscription=success",
                CancelUrl = "https://apistacks.com/Plan?subscription=cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            var payload = new
            {
                id = session.Id
            };

            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, "application/json")
            };
        }
    }
}