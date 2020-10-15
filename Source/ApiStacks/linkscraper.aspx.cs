using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class linkscraper : System.Web.UI.Page
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
                            Session["linkScraperData"] = null;
                            var screenshotResponse = APICall.call(APICall.API_LinkScraper, url);
                            if (screenshotResponse != null)
                            {
                                Session["linkScraperData"] = screenshotResponse;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (Session["linkScraperData"] == null)
            {
                //Load sample graph data
                string sampleFilePathpath = Server.MapPath("/samples/sampleWhois.txt");
                using (var sr = new StreamReader(sampleFilePathpath))
                {
                    var content = sr.ReadToEnd();
                    txtData.Value = content;
                }
            }
            else
            {
                txtData.Value = Session["linkScraperData"].ToString();
            }
        }
    }
}
