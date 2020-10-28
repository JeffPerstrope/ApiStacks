using SharpFireStarter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiStacks
{
    public static class UserCreateNew
    {
        public static bool CreateCustomer_FirebaseUsage(User user)
        {
            //Authenticate with admin user to access firebase DB
            var firebaseInstance = new FireBaseDB(Global.appID, Global.databaseURL, Global.appKey);
            var adminUser = firebaseInstance.Authenticate("admin@apistacks.com", "W_e7&c':Nc`scc(S");

            var APIKey = Guid.NewGuid().ToString().ToLower();

            var keyPayload = new
            {
                key = APIKey
            };

            firebaseInstance.WriteToDB("Users/" + user.userID, keyPayload);

            var usagePayload = new
            {
                current = 0,
                max = 1500,
                plan = "free",
                renewal = DateTime.Now.ToShortDateString(),
                userID = user.userID
            };

            firebaseInstance.WriteToDB("Usage/" + APIKey, usagePayload);

            return true;
        }
    }
}