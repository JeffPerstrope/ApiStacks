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
                            var screenshotResponse = getScreenshot(url);
                            if (screenshotResponse != null)
                            {
                                Session["screenshot"] = screenshotResponse;
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

        private string getScreenshot(string URL)
        {
            var endpoint = "https://apistacks-webscreenshot.glitch.me/getScreenshot?url=" + URL;

            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                        if (content != null)
                        {
                            content = content.Replace(@"\", "").Replace("\"", "");
                            return content;
                        }
                    }
                }
            }

            return null;
        }

    }
}