using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack)
            {
                if (Request.QueryString["plan"] != null)
                    Session["selectedPlan"] = Request.QueryString["plan"];

                //Validate data
                if (firstName.Value == "")
                    return;
                if (lastName.Value == "")
                    return;
                if (emailAddress.Value == "")
                    return;
                if (password.Value == "")
                    return;

                Session["userID"] = Guid.NewGuid();
                Response.Redirect(@"\Index.aspx");
            }
        }
    }
}