
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("/Login");
            }

            //Load all apps and get their statuses
            AppsList.PopulateApps();
            loadAppStatuses();


            if (Request.QueryString["activate"] != null)
            {
                string appID = Request.QueryString["activate"];

                //Activate app in DB
                var payload = new Dictionary<object, object>();
                payload.Add(appID, true);

                if (Global.db.WriteToDB("Main/Users/" + Session["userID"].ToString() + "/apps/", payload))
                {
                    AppsList.activateApp(appID);
                }

                Response.Redirect("/Dashboard");
            }
            else if (Request.QueryString["deactivate"] != null)
            {
                string appID = Request.QueryString["deactivate"];

                //Activate app in DB
                var payload = new Dictionary<object, object>();
                payload.Add(appID, null);

                if (Global.db.WriteToDB("Main/Users/" + Session["userID"].ToString() + "/apps/", payload))
                {
                    AppsList.deactivateApp(appID);
                }
                Response.Redirect("/Dashboard");
            }
        }

        private void loadAppStatuses()
        {
            var appStatuses = Global.db.GetFromDB("Main/Users/" + Session["userID"].ToString() + "/apps/");
            if (appStatuses != null)
            {
                var appStatusesDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(appStatuses);
                foreach (var appStatus in appStatusesDic)
                {
                    foreach (var app in AppsList.InstalledApps)
                    {
                        if (appStatus.Key == app.id)
                        {
                            app.enabled = (bool)appStatus.Value;
                            break;
                        }
                    }
                }
            }
        }
    }
}