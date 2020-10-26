using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class dashboard : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Load user info
            var currentUser = Global.db.currentUser;
            if(currentUser != null)
            {
                var userSessionManager = new UserSessionManager();
                userSessionManager.LoadUserInfo(currentUser);
            }
            else
            {
                Response.Redirect("/Login");
            }

            //Get number of activated apps
            float ativatedApps = 0;
            foreach(var app in AppsList.InstalledApps)
            {
                if (app.enabled)
                    ativatedApps += 1;
            }

            if (Session["userPlan"].ToString() == "free")
            {
                float activatedAppsPercentage = ((ativatedApps / 3) * 100);
                enabledAppsCount.InnerText = ativatedApps.ToString() + " / 3";
                enabledAppsProgress.Attributes.Add("style", "width: " + activatedAppsPercentage + "%");
                if (activatedAppsPercentage >= 100)
                    Session["userMaxAppsReached"] = true;
                else
                    Session["userMaxAppsReached"] = false;
            } else
            {
                float activatedAppsPercentage = ((ativatedApps / AppsList.InstalledApps.Count) * 100);
                enabledAppsCount.InnerText = ativatedApps.ToString() + " / " + AppsList.InstalledApps.Count.ToString();
                enabledAppsProgress.Attributes.Add("style", "width: " + activatedAppsPercentage + "%");
                if (activatedAppsPercentage >= 100)
                    Session["userMaxAppsReached"] = true;
                else
                    Session["userMaxAppsReached"] = false;
            }
        }
    }
}