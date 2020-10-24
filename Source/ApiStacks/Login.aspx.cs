using Newtonsoft.Json;
using SharpFireStarter;
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
            //if(Session["userID"] != null)
            //{
            //    Response.Redirect("/Dashboard");
            //}

            //Hide login error
            txtLoginError.Visible = false;

            //Check if logging out
            if(Request.Params.Count > 0 && Request.Params["logout"] == "true")
            {
                Session.Abandon();
                Global.db.SignOut();
                Global.initializeFirebase();
                Response.Redirect("/Login");
            }


            //If user is signing in
            if (Page.IsPostBack)
            {
                var userName = txtEmail.Value;
                var password = txtPassword.Value;

                var signedInUser = validateLogin(userName, password);
                if (signedInUser != null)
                {
                    Session["user"] = signedInUser;
                    var userInfo = Global.db.GetFromDB("Main/Users/" + signedInUser.userID);
                    var userInfoDictionary = JsonConvert.DeserializeObject < Dictionary<string, string>>(userInfo);

                    Session["userFirstName"] = userInfoDictionary["firstName"];
                    Session["userLastName"] = userInfoDictionary["lastName"];
                    Session["userKey"] = userInfoDictionary["key"];
                    Response.Redirect("/Dashboard");
                } else
                {
                    txtLoginError.Visible = true;
                }
            }
        }

        public User validateLogin(string username, string password)
        {
            //Authenticate with Firebase DB
            var signedInUser = Global.db.Authenticate(username, password);
            if(signedInUser != null)
            {
                return signedInUser;
            }
            else
            {
                Console.WriteLine("falied");
                return null;
            }
        }
    }
}