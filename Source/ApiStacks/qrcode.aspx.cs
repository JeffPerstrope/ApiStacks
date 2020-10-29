
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApiStacks
{
    public partial class qrcode : System.Web.UI.Page
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
                        //string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
                        //Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        //if (Rgx.IsMatch(url))
                        //{
                        Session["qrCodeData"] = null;

                        //Get Screenshot
                        var screenshotResponse = APICall.call(APICall.API_QRCode, url);
                        if (screenshotResponse != null)
                        {
                            var returnedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(screenshotResponse);
                            Session["qrCodeData"] = returnedData["data"];
                            var fileName = Session["qrCodeData"].ToString().Split('/').Last();
                            Response.Redirect("qrcode?code=" + fileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (Session["qrCodeData"] == null)
            {
                screenshotImage.Src = "samples/qrcode.png";
            }
            else
            {
                screenshotImage.Src = Session["qrCodeData"].ToString();
            }
        }
    }
}