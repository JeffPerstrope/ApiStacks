
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
    public partial class emailvalidate : System.Web.UI.Page
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
                        //if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                        //    url = "http://" + url;
                        string Pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                        Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        if (Rgx.IsMatch(url) && !url.EndsWith("."))
                        {
                            Session["emailValidateData"] = null;
                            var screenshotResponse = APICall.call(APICall.API_EmailValidator, url);
                            if (screenshotResponse != null)
                            {
                                Session["emailValidateData"] = screenshotResponse;
                                Response.Redirect("emailvalidate?email=" + Guid.NewGuid().ToString().ToLower());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (Session["emailValidateData"] == null)
            {
                //Load sample graph data
                string sampleFilePathpath = Server.MapPath("/samples/sampleEmailValidate.txt");
                using (var sr = new StreamReader(sampleFilePathpath))
                {
                    var content = sr.ReadToEnd();
                    txtData.Value = content;
                }
            }
            else
            {
                txtData.Value = Session["emailValidateData"].ToString();
            }
        }
    }
}