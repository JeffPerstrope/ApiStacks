using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //Redirect if already logged in
            if (Session["userID"] != null)
            {
                Response.Redirect("/Dashboard");
            }

            if (Page.IsPostBack)
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

                var userFirstName = firstName.Value;
                var userLastName = lastName.Value;
                var userEmail = emailAddress.Value;
                var userPassword = password.Value;

                //Proceed if debugging
                if (!Debugger.IsAttached)
                    Response.Redirect("/Login");

                var newUser = Global.db.SignUp(string.Format("{0} {1}", userFirstName, userLastName), userEmail, userPassword);
                if(newUser != null)
                {
                    var payload = new Dictionary<string, string>
                    {
                        { "firstName", userFirstName},
                        { "lastName", userLastName},
                        { "email", userEmail},

                    };

                    Global.db.WriteToDB("Main/Users/" + newUser.userID + "/", payload);

                    SessionManager sessionMan = new SessionManager();
                    sessionMan.LoadUserInfo(newUser);
                }
                
                

                Response.Redirect("/Dashboard");
            }
        }
    }
}