using Newtonsoft.Json;
using SharpFireStarter;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStacks
{
    public class UserSessionManager : System.Web.UI.Page
    {
        /// <summary>
        /// Load user info from firebase
        /// </summary>
        /// <param name="user"></param>
        /// <param name="forceRefresh"></param>
        public void LoadUserInfo(User user, bool forceRefresh = false)
        {
            //Get Basic user info
            var userInfo = Global.db.GetFromDB("Users/" + user.userID);
            var userInfoDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(userInfo);

            if (userInfoDictionary != null)
            {
                Session["user"] = user;
                Session["userID"] = user.userID;
                Session["userFirstName"] = userInfoDictionary["firstName"];
                Session["userLastName"] = userInfoDictionary["lastName"];
                Session["userEmail"] = userInfoDictionary["email"];
                Session["userKey"] = userInfoDictionary.ContainsKey("key") ? userInfoDictionary["key"] : "";

                Session["userBusiness"] = userInfoDictionary.ContainsKey("business") ? userInfoDictionary["business"] : "";
                Session["userPhone"] = userInfoDictionary.ContainsKey("phone") ? userInfoDictionary["phone"] : "";
                Session["userWebsite"] = userInfoDictionary.ContainsKey("website") ? userInfoDictionary["website"] : "";

                LoadUserUsage(user);
                LoadStripeSubscription(user);
                UpdateFIrebaseData(user);
            }
            else
            {
                //Something went wrong. Clear session
                Session.Clear();
                Session.Abandon();
            }
        }


        /// <summary>
        /// Load usage info from firebase
        /// </summary>
        /// <param name="user"></param>
        private void LoadUserUsage(User user)
        {
            var usage = Global.db.GetFromDB("Usage/" + Session["userKey"].ToString());
            var usageDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(usage);

            var userUsageCurrent = Convert.ToDouble(usageDictionary["current"]);
            var userUsageMax = Convert.ToDouble(usageDictionary["max"]);
            var userUsagePercentage = ((userUsageCurrent / userUsageMax) * 100);
            Session["userUsagePercentage"] = Convert.ToInt64(userUsagePercentage);

            Session["userStripeID"] = usageDictionary["stripeID"];
            Session["userPlan"] = usageDictionary["plan"];
            Session["userPlanRenewal"] = usageDictionary["renewal"];
            Session["userUsageCurrent"] = userUsageCurrent.ToString("N0");
            Session["userUsageMax"] = userUsageMax.ToString("N0");
            Session["userUsageRenewal"] = usageDictionary["renewal"];
        }


        /// <summary>
        /// Load subscription from stripe
        /// </summary>
        /// <param name="user"></param>
        /// <param name="forceRefresh"></param>
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
                if (subscriptions == null || subscriptions.Count() <= 0)
                {
                    Session["userPlan"] = "free";
                    Session["userPlanRenewal"] = DateTime.Now.AddDays(365).ToShortDateString().ToString();
                    return;
                }
                foreach (var sub in subscriptions)
                {
                    var subItem = sub.Items;
                    foreach (var subItem_item in subItem)
                    {
                        if (subItem_item.Price.Id == subscriptionStarter)
                        {
                            Session["userPlan"] = "starter";
                            Session["userPlanRenewal"] = Convert.ToDateTime(sub.CurrentPeriodEnd.ToString()).ToShortDateString();
                            break;
                        }

                        else if (subItem_item.Price.Id == subscriptionProfessional)
                        {
                            Session["userPlan"] = "professional";
                            Session["userPlanRenewal"] = Convert.ToDateTime(sub.CurrentPeriodEnd.ToString()).ToShortDateString();
                            break;
                        }
                        else if (subItem_item.Price.Id == subscriptionUltimate)
                        {
                            Session["userPlan"] = "ultimate";
                            Session["userPlanRenewal"] = Convert.ToDateTime(sub.CurrentPeriodEnd.ToString()).ToShortDateString();
                            break;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Update firebase user plan data
        /// </summary>
        /// <param name="user"></param>
        private void UpdateFIrebaseData(User user)
        {
            //Update Firebase Data
            var db = new FireBaseDB(Global.appID, Global.databaseURL, Global.appKey);
            db.Authenticate("admin@apistacks.com", "W_e7&c':Nc`scc(S");
            var currentPlan = Session["userPlan"].ToString();
            var maxUsage = 1500;


            if (currentPlan == "free")
                maxUsage = 1200;
            else if (currentPlan == "starter")
                maxUsage = 10000;
            else if (currentPlan == "professional")
                maxUsage = 35000;
            else if (currentPlan == "ultimate")
                maxUsage = 100000;

            var payload = new
            {
                plan = currentPlan,
                max = maxUsage,
                renewal = Session["userPlanRenewal"].ToString()
            };
            db.WriteToDB("Usage/" + Session["userKey"].ToString(), payload);

        }
    }
}