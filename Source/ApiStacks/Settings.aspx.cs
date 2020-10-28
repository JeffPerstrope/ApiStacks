using SharpFireStarter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("/Login");
            }

            txtPasswordError.Visible = false;

            if (Page.IsPostBack)
            {
                var payload = new
                {
                    firstName = txtFirstName.Value,
                    lastName = txtLastName.Value,
                    business = txtBusiness.Value,
                    phone = txtPhone.Value,
                    website = txtWebsite.Value
                };

                if (Global.db.WriteToDB("Users/" + Session["userID"], payload))
                {
                    UserSessionManager sessionMan = new UserSessionManager();
                    sessionMan.LoadUserInfo((User)Session["user"]);
                }

                //Reset password
                if (txtPasswordOld.Value.Trim() != "" && txtPasswordNew.Value.Trim() != "" && txtPasswordNewVerify.Value.Trim() != "")
                {
                    var oldPasword = txtPasswordOld.Value.Trim();
                    var newPassword = txtPasswordNew.Value.Trim();
                    var newPasswordVerify = txtPasswordNewVerify.Value.Trim();

                    if (newPassword != newPasswordVerify)
                    {
                        txtPasswordError.Visible = true;
                        txtPasswordError.InnerText = "Passwords do not match";
                    }
                    else
                    {
                        var changedPassword = Global.db.ChangePassword(Session["userEmail"].ToString(), oldPasword, newPassword);
                        if (changedPassword == null)
                        {
                            txtPasswordError.Visible = true;
                        }
                    }
                }

                //Send Email
                var newEmail = new Model_Email
                {
                    email = "charifield@gmail.com",
                    name = "Patrick",
                    subject = "Settings have changed",
                    body = "Thanks for creating an Landrick account. To continue, please confirm your email address by clicking the button below :"

                };
                EmailSender.SendEmail(newEmail, Server);
            }
            else
            {
                //Set current Values
                txtFirstName.Value = Session["userFirstName"].ToString();
                txtLastName.Value = Session["userLastName"].ToString();
                txtEmail.Value = Session["userEmail"].ToString();
                txtBusiness.Value = Session["userBusiness"].ToString();
                txtPhone.Value = Session["userPhone"].ToString();
                txtWebsite.Value = Session["userWebsite"].ToString();
            }
        }
    }
}