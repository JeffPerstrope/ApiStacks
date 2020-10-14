using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class opengraph : System.Web.UI.Page
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
                            Session["graphData"] = null;
                            var screenshotResponse = getOpenGraph(url);
                            if (screenshotResponse != null)
                            {
                                Session["graphData"] = screenshotResponse;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (Session["graphData"] == null)
            {
                //Load sample graph data
                string sampleFilePathpath = Server.MapPath("/samples/sampleGraph.txt");
                using (var sr = new StreamReader(sampleFilePathpath))
                {
                    var content = sr.ReadToEnd();
                    txtData.Value = content;
                }
            }
            else
            {
                txtData.Value = Session["graphData"].ToString();
            }
        }

        private string getOpenGraph(string URL)
        {
            var endpoint = "https://apistack-opengrah.glitch.me/openGraph?url=" + URL;

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
                            //string jsonString = JsonConvert.SerializeObject(content, Formatting.Indented);
                            //content = content.Replace(@"\", "").Replace("\"", "");
                            return content;
                        }
                    }
                }
            }

            return null;
        }
    }
}