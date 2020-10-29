using Newtonsoft.Json;
using SharpFireStarter;
using Stripe;
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

                var newUser = CreateCustomer_Firebase(userFirstName, userLastName, userEmail, userPassword);
                if(newUser == null)
                    Response.Redirect("/Login");
                if (!CreateCustomer_Stripe(newUser))
                    Response.Redirect("/Login");
                

                //All went well, load session data
                UserSessionManager sessionMan = new UserSessionManager();
                sessionMan.LoadUserInfo(newUser);

                //Redirect to dashboard
                Response.Redirect("/Dashboard");
            }
        }


        /// <summary>
        /// Create a new user in Firebase
        /// </summary>
        /// <param name="userFirstName"></param>
        /// <param name="userLastName"></param>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        private User CreateCustomer_Firebase(string userFirstName, string userLastName, string userEmail, string userPassword)
        {
            var newUser = Global.db.SignUp(string.Format("{0} {1}", userFirstName, userLastName), userEmail, userPassword);
            if (newUser != null)
            {
                //Generate new API Key
                var APIKey = Guid.NewGuid().ToString().ToLower();
                Session["userKey"] = APIKey;

                var payload = new Dictionary<string, object>
                    {
                        { "firstName", userFirstName},
                        { "lastName", userLastName},
                        { "email", userEmail},
                        { "emailValid", false},
                        { "key", APIKey }
                    };

                Global.db.WriteToDB("Users/" + newUser.userID + "/", payload);

                //Authenticate with admin user to access firebase DB
                var firebaseInstance = new FireBaseDB(Global.appID, Global.databaseURL, Global.appKey);
                _ = firebaseInstance.Authenticate("admin@apistacks.com", "W_e7&c':Nc`scc(S");

                var usagePayload = new
                {
                    current = 0,
                    max = 1500,
                    plan = "free",
                    renewal = DateTime.Now.ToShortDateString(),
                    userID = newUser.userID
                };

                firebaseInstance.WriteToDB("Usage/" + APIKey, usagePayload);

                return newUser;
            }
            return null;
        }

        private bool CreateCustomer_Stripe(User newUser)
        {
            StripeConfiguration.ApiKey = "sk_test_51HcZiyF6EVrg0l22KUHmhtN6fxfGQvV1yG2vk2My3Dnq6N4zTg3CASy3OKzArWvWij8CL7BwnqGDPY8xke0Hsmq100FLmHAkYc";

            var firebaseIDData = new Dictionary<string, string>();
            firebaseIDData.Add("FirebaseID", newUser.userID);
            var options = new CustomerCreateOptions
            {
                Email = newUser.email,
                Description = "FirebaseID: " + newUser.userID,
                Metadata = firebaseIDData
            };
            var service = new CustomerService();
            Customer customer = service.Create(options);

            if(customer != null)
            {
                var payload = new Dictionary<string, string>
                    {
                        { "stripeID", customer.Id},
                    };

                Global.db.WriteToDB("Users/" + newUser.userID + "/", payload);
                Global.db.WriteToDB("Usage/" + Session["userKey"].ToString() + "/", payload);
                return true;
            }

            return false;
        }
    }
}