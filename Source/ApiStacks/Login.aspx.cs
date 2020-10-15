using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Redirect if already logged in
            if(Session["userID"] != null)
            {
                Response.Redirect("/Dashboard");
            }

            if (Page.IsPostBack)
            {
                var userName = txtEmail.Value;
                var password = txtPassword.Value;

                if (validateLogin(userName, password))
                {
                    Session["userID"] = userName;
                    Response.Redirect(@"\Dashboard");
                }
            }
        }

        public bool validateLogin(string username, string password)
        {
            return true;
        }
    }
}