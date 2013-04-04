using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToernooiBaseFaker.Models
{
    public class ToernooiBaseHtml
    {
        public ToernooiBaseHtml(string html)
        {
            Html = html;
        }

        public string Html { get; private set; }
    }
}