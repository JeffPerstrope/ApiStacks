
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
            AppsList.PopulateApps();
            
            if(Page.Request.Params["activate"] != null)
            {
                AppsList.activateApp(Page.Request.Params["activate"]);
                Response.Redirect("/Dashboard");
            }
            else if (Page.Request.Params["deactivate"] != null)
            {
                AppsList.deactivateApp(Page.Request.Params["deactivate"]);
                Response.Redirect("/Dashboard");
            }
        }
    }
}