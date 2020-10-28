
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class webscreenshots : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.AsyncMode = true;
            if (Page.IsPostBack)
            {
                try
                {
                    if (txtAddress.Value != null && txtAddress.Value.Trim() != "")
                    {
                        //Validate URL
                        var url = txtAddress.Value.ToLower().Trim();
                        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                            url = "http://" + url;
                        string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
                        Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        if (Rgx.IsMatch(url))
                        {
                            Session["screenshot"] = null;

                            //Get Screenshot
                            var screenshotResponse = APICall.call(APICall.API_Screenshot, url);
                            if (screenshotResponse != null)
                            {
                                var returnedData = JsonConvert.DeserializeObject < Dictionary<string, object>>(screenshotResponse);
                                Session["screenshot"] = returnedData["data"];
                                var fileName = Session["screenshot"].ToString().Split('/').Last();
                                Response.Redirect("webscreenshots?capture=" + fileName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if(Session["screenshot"] == null)
            {
                screenshotImage.Src = "images/webscreenshots_Browser.jpeg";
            } else
            {
                screenshotImage.Src = Session["screenshot"].ToString();
            }
        }
    }
}