using Newtonsoft.Json;
using SharpFireStarter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStacks
{
    public class SessionManager: System.Web.UI.Page
    {
        public void LoadUserInfo(User user)
        {
            var userInfo = Global.db.GetFromDB("Main/Users/" + user.userID);
            var userInfoDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(userInfo);

            if (userInfoDictionary != null)
            {
                Session["user"] = user;
                Session["userID"] = user.userID;
                Session["userFirstName"] = userInfoDictionary["firstName"];
                Session["userLastName"] = userInfoDictionary["lastName"];
                Session["userEmail"] = userInfoDictionary["email"];
                Session["userKey"] = userInfoDictionary["key"];

                if (userInfoDictionary.ContainsKey("business"))
                    Session["userBusiness"] = userInfoDictionary["business"];
                else
                    Session["userBusiness"] = "";

                if (userInfoDictionary.ContainsKey("phone"))
                    Session["userPhone"] = userInfoDictionary["phone"];
                else
                    Session["userPhone"] = "";

                if (userInfoDictionary.ContainsKey("website"))
                    Session["userWebsite"] = userInfoDictionary["website"];
                else
                    Session["userWebsite"] = "";
            }
        }
    }
}