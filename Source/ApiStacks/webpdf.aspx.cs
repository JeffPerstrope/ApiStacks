using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class webpdf : System.Web.UI.Page
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
                            Session["webPDFData"] = null;

                            //Get Screenshot
                            var screenshotResponse = APICall.call(APICall.API_PDF, url);
                            if (screenshotResponse != null)
                            {
                                Session["webPDFData"] = screenshotResponse.Replace(@"\", "").Replace("\"", ""); ;
                                var fileName = Session["webPDFData"].ToString().Split('/').Last();
                                Response.Redirect("webpdf?capture=" + fileName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (Session["webPDFData"] == null)
            {
                screenshotImage.Src = "samples/samplePDF.pdf";
            }
            else
            {
                screenshotImage.Src = Session["webPDFData"].ToString();
            }
        }
    }
}