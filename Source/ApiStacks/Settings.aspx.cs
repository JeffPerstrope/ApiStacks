using SharpFireStarter;
using System;
using System.Collections.Generic;
using System.Linq;
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

                if (Global.db.WriteToDB("Main/Users/" + Session["userID"], payload))
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