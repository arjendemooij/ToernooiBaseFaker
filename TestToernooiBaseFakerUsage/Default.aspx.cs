using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    private const string ToernooiBaseUrlQueryParameter = "toernooiBaseUrl";

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public string GetToernooiBaseContent()
    {
        string relativeUrl = GetToernooiBaseRelativeUrl();
        string localUrlFormat = GetLocalUrlFormat();

        string remoteUrl = string.Format("http://localhost:4625/Home/ToernooiBasePage?relativeUrl={0}&linkFormat={1}", HttpUtility.UrlEncode(relativeUrl), HttpUtility.UrlEncode(localUrlFormat)); 
        string html = new WebClient().DownloadString(remoteUrl);

        return html;
    }

    private string GetLocalUrlFormat()
    {
        string thisRequestUrl = Request.Url.AbsoluteUri;
        
        if(RequestHasToernooiBaseParameterValue())
        {
            string parameterValueString = string.Format("{0}={1}", ToernooiBaseUrlQueryParameter, HttpUtility.UrlEncode(GetToernooiBaseRelativeUrlFromGet()));
            return thisRequestUrl.Replace(parameterValueString, ToernooiBaseUrlQueryParameter + "={0}");
        }
        else
        {
            string seperator =  thisRequestUrl.Contains("?") ? "&": "?";
            return thisRequestUrl + seperator + ToernooiBaseUrlQueryParameter + "={0}";
        }
        
    }

    private bool RequestHasToernooiBaseParameterValue()
    {
        return !string.IsNullOrEmpty(GetToernooiBaseRelativeUrlFromGet()); 
    }

    private string GetToernooiBaseRelativeUrl()
    {
        string relativeUrlFromGet = GetToernooiBaseRelativeUrlFromGet();

        string defaultRelativeUrl = "/opvraag/standenke.php?taal=&Id=3094&jr=13";
        string relativeUrl = !string.IsNullOrEmpty(relativeUrlFromGet)
            ? relativeUrlFromGet
            : defaultRelativeUrl;

        return relativeUrl;
    }

    private string GetToernooiBaseRelativeUrlFromGet()
    {
        string relativeUrlFromGet = Request["toernooiBaseUrl"];
        return relativeUrlFromGet;
    }

}