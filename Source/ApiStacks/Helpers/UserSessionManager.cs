using Newtonsoft.Json;
using SharpFireStarter;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStacks
{
    public class UserSessionManager: System.Web.UI.Page
    {
        public  void LoadUserInfo(User user, bool forceRefresh = false)
        {
            //Get Basic user info
            var userInfo = Global.db.GetFromDB("Main/Users/" + user.userID);
            var userInfoDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(userInfo);

            if (userInfoDictionary != null)
            {
                Session["user"] = user;
                Session["userID"] = user.userID;
                Session["userFirstName"] = userInfoDictionary["firstName"];
                Session["userLastName"] = userInfoDictionary["lastName"];
                Session["userEmail"] = userInfoDictionary["email"];
                Session["userStripeID"] = userInfoDictionary["stripeID"];
                Session["userKey"] = userInfoDictionary.ContainsKey("key") ? userInfoDictionary["key"] : "";

                Session["userBusiness"] = userInfoDictionary.ContainsKey("business") ? userInfoDictionary["business"] : "";
                Session["userPhone"] = userInfoDictionary.ContainsKey("phone") ? userInfoDictionary["phone"] : "";
                Session["userWebsite"] = userInfoDictionary.ContainsKey("website") ? userInfoDictionary["website"] : "";

                LoadUserUsage(user);
                LoadStripeSubscription(user);
            } else
            {
                //Something went wrong. Clear session
                Session.Clear();
                Session.Abandon();
            }
        }

        private void LoadUserUsage(User user)
        {
            var usage = Global.db.GetFromDB("Main/Usage/" + Session["userKey"].ToString());
            var usageDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(usage);

            var userUsageCurrent = Convert.ToDouble(usageDictionary["current"]);
            var userUsageMax = Convert.ToDouble(usageDictionary["max"]);
            var userUsagePercentage = ((userUsageCurrent / userUsageMax) * 100);
            Session["userUsagePercentage"] = Convert.ToInt64(userUsagePercentage);

            Session["userPlan"] = usageDictionary["plan"];
            Session["userUsageCurrent"] = userUsageCurrent.ToString("N0");
            Session["userUsageMax"] = userUsageMax.ToString("N0");
            Session["userUsageRenewal"] = usageDictionary["renewal"];
        }

        private void LoadStripeSubscription(User user, bool forceRefresh = false)
        {
            var subscriptionStarter = "price_1HcZm0F6EVrg0l22lVOfn9c8";
            var subscriptionProfessional = "price_1HfvS8F6EVrg0l22KoQlyZp8";
            var subscriptionUltimate = "price_1HfwJ3F6EVrg0l22NYrfJDO6";

            StripeConfiguration.ApiKey = "sk_test_51HcZiyF6EVrg0l22KUHmhtN6fxfGQvV1yG2vk2My3Dnq6N4zTg3CASy3OKzArWvWij8CL7BwnqGDPY8xke0Hsmq100FLmHAkYc";
            var service = new CustomerService();
            var customerGetOptions = new CustomerGetOptions();
            customerGetOptions.AddExpand("subscriptions");
            Customer customer = service.Get(Session["userStripeID"].ToString(), customerGetOptions);

            if (customer != null)
            {
                var subscriptions = customer.Subscriptions;
                if(subscriptions == null || subscriptions.Count() <= 0)
                {
                    Session["userPlan"] = "free";
                    return;
                }
                foreach(var sub in subscriptions)
                {
                    var subItem = sub.Items;
                    foreach(var subItem_item in subItem)
                    {
                        if (subItem_item.Price.Id == subscriptionStarter)
                        {
                            Session["userPlan"] = "starter";
                            break;
                        }

                        else if (subItem_item.Price.Id == subscriptionProfessional)
                        {
                            Session["userPlan"] = "professional";
                            break;
                        }
                        else if (subItem_item.Price.Id == subscriptionUltimate)
                        {
                            Session["userPlan"] = "ultimate";
                            break;
                        }
                    }
                }
            }
        }
    }
}