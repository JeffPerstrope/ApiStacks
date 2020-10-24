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
                var usage = Global.db.GetFromDB("Main/Usage/" + Session["userKey"].ToString());
                var usageDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(usage);

                var userUsageCurrent = Convert.ToDouble(usageDictionary["current"]);
                var userUsageMax = Convert.ToDouble(usageDictionary["max"]);
                var userUsagePercentage = ((userUsageCurrent / userUsageMax) * 100);
                Session["userUsagePercentage"] = Convert.ToInt64(userUsagePercentage);


                Session["userUsageCurrent"] = userUsageCurrent.ToString("N0");
                Session["userUsageMax"] = userUsageMax.ToString("N0");
                Session["userUsageRenewal"] = usageDictionary["renewal"];
            }
            else
            {
                Response.Redirect("/Login");
            }
        }
    }
}