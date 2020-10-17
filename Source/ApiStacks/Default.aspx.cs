using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userID"] = "814f52b2-0b74-43cc-9848-eaeec97da3e0";
            Session["privateKey"] = "814f52b2-0b74-43cc-9848-eaeec97da3e0";
        }
    }
}