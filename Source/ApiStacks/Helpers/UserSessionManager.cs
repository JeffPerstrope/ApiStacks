using Newtonsoft.Json;
using SharpFireStarter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStacks
{
    public class UserSessionManager: System.Web.UI.Page
    {
        public  void LoadUserInfo(User user)
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
                Session["userKey"] = userInfoDictionary.ContainsKey("key") ? userInfoDictionary["key"] : "";

                Session["userBusiness"] = userInfoDictionary.ContainsKey("business") ? userInfoDictionary["business"] : "";
                Session["userPhone"] = userInfoDictionary.ContainsKey("phone") ? userInfoDictionary["phone"] : "";
                Session["userWebsite"] = userInfoDictionary.ContainsKey("website") ? userInfoDictionary["website"] : "";

               
                //if (userInfoDictionary.ContainsKey("business"))
                //    Session["userBusiness"] = userInfoDictionary["business"];
                //else
                //    Session["userBusiness"] = "";

                //if (userInfoDictionary.ContainsKey("phone"))
                //    Session["userPhone"] = userInfoDictionary["phone"];
                //else
                //    Session["userPhone"] = "";

                //if (userInfoDictionary.ContainsKey("website"))
                //    Session["userWebsite"] = userInfoDictionary["website"];
                //else
                //    Session["userWebsite"] = "";

                LoadUserUsage(user);
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
    }
}