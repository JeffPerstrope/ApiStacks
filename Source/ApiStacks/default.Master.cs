using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri.ToLower().Replace(".aspx", "");
            Session["currentURL"] = url;
            if (url.EndsWith("terms") || url.EndsWith("privacy"))
            {
                sectionFAQ.Visible = false;
                sectionPricing.Visible = false;
            }
            else
            {
                sectionFAQ.Visible = true;
                sectionPricing.Visible = true;
            }
        }
    }
}