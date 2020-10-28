
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
                            var screenshotResponse = APICall.call(APICall.API_OpenGraph, url);
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
    }
}