using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ToernooiBaseFaker.Models;

namespace ToernooiBaseFaker.Controllers.Business
{
    public class ToernooiBaseReader
    {
        public string GetToernooibaseHtml(string relativeUrl)
        {
            HttpWebRequest toernooibaseRequest = (HttpWebRequest)WebRequest.Create("http://toernooibase.kndb.nl" + relativeUrl);
            using (HttpWebResponse response = (HttpWebResponse)toernooibaseRequest.GetResponse())
            {
                var encoding = ASCIIEncoding.ASCII;
                using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}