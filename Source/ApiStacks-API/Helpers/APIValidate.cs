using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using SharpFireStarter;

namespace ApiStacks_API.Helpers
{
    public static class APIValidate
    {
        private static FireBaseDB db = new FireBaseDB("apistacks-basicapps", "https://apistacks-basicapps.firebaseio.com", "AIzaSyBkCHXwY87S0ZQxo6T1jNLbxYCaizgMnsU");

        /// <summary>
        /// Check if key is authorized to make request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string AuthorizeRequest(HttpRequestMessage request)
        {
            //OK if debugging
            if(System.Diagnostics.Debugger.IsAttached)
            {
                return "success";
            }

            //Extract API Key
            if(request.Headers.Contains("x-api-key"))
            {
                IEnumerable<string> headerValues = request.Headers.GetValues("x-api-key");
                var apiKey = headerValues.FirstOrDefault();

                var user = (db.Authenticate("admin@apistacks.com", "W_e7&c':Nc`scc(S"));
                if (user != null)
                {
                    //Check if user has enough requests left
                    var usageData = db.GetFromDB("Main/Usage/" + apiKey);
                    if(usageData == null)
                    {
                        return "this account is not set up correctly";
                    }

                    var usageDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(usageData);

                    var userID = usageDictionary["userID"];
                    var userUsageCurrent = Convert.ToDouble(usageDictionary["current"]);
                    var userUsageMax = Convert.ToDouble(usageDictionary["max"]);
                    var userUsagePercentage = ((userUsageCurrent / userUsageMax) * 100);

                    if(userUsagePercentage >= 100)
                    {
                        return "api limit of " + userUsageMax + " has been reached";
                    }

                    //Check if app is enabled
                    var completeURL = request.RequestUri.ToString();
                    var trailingURL = completeURL.Split('/').Last();
                    var segmentAppID = trailingURL.Split('?').First();

                    var appEnabled = false;
                    var enabledApps = db.GetFromDB("Main/Users/" + userID + "/apps");
                    if (enabledApps != null)
                    {
                        var enabledAppsDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(enabledApps);
                        foreach (var app in enabledAppsDic)
                        {
                            if (app.Key == segmentAppID)
                            {
                                appEnabled = true;
                                break;
                            }
                        }
                    }

                    if (!appEnabled)
                        return "api is disabled in the dashboard";


                    return "success";
                }
                else
                {
                    return "unknown error";
                }
            }
            else
            {
                return "api key not provided";
            }            
        }

        /// <summary>
        /// Increment usage count
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string IncrementUsageRequest(HttpRequestMessage request)
        {
            //OK if debugging
            if (System.Diagnostics.Debugger.IsAttached)
            {
                return "success";
            }

            //Extract API Key
            if (request.Headers.Contains("x-api-key"))
            {
                IEnumerable<string> headerValues = request.Headers.GetValues("x-api-key");
                var apiKey = headerValues.FirstOrDefault();

                var user = (db.Authenticate("admin@apistacks.com", "W_e7&c':Nc`scc(S"));
                if (user != null)
                {
                    var usageData = db.GetFromDB("Main/Usage/" + apiKey);
                    if (usageData == null)
                    {
                        return "this account is not set up correctly";
                    }

                    var usageDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(usageData);
                    var userUsageCurrent = Convert.ToDouble(usageDictionary["current"]);
                    var userUsageMax = Convert.ToDouble(usageDictionary["max"]);
                    var userUsagePercentage = ((userUsageCurrent / userUsageMax) * 100);

                    if (userUsagePercentage < 100)
                    {
                        userUsageCurrent += 1;
                        var payload = new
                        {
                            current = userUsageCurrent
                        };
                        db.WriteToDB("Main/Usage/" + apiKey, payload);

                        return "success";
                    }
                    return "unknown error occured.";
                }
                else
                {
                    return "unknown error";
                }
            }
            else
            {
                return "api key not provided";
            }
        }

        public static string GetUserSubscription(User user)
        {



            return "";
        }
    }
}