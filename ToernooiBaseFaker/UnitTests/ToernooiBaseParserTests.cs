using HtmlAgilityPack;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToernooiBaseFaker.Controllers.Business;
using ToernooiBaseFaker.Models;

namespace ToernooiBaseFaker.UnitTests
{
    public class ToernooiBaseParserTests
    {
        [Test]
        public void TestRandomPage()
        {
            ToernooiBaseReader reader = new ToernooiBaseReader();
            ToernooiBaseParser parser = new ToernooiBaseParser();

            string relativeUrl = "/opvraag/standen.php?taal=&kl=18&Id=3094&r=8&jr=13&afko=18&rondeweergave=8";

            ToernooiBaseHtml html = parser.ParseHtml(reader.GetToernooibaseHtml(relativeUrl), relativeUrl, "{0}");

            Assert.IsFalse(html.Html.Contains("<body"));

        }

        [Test]
        public void TestRecursiveGetNode()
        {
            ToernooiBaseReader reader = new ToernooiBaseReader();
            ToernooiBaseParser parser = new ToernooiBaseParser();
            string relativeUrl = "/opvraag/standen.php?taal=&kl=18&Id=3094&r=8&jr=13&afko=18&rondeweergave=8";

            string html = reader.GetToernooibaseHtml(relativeUrl);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            parser.ParseHtml(html, relativeUrl, "0");
        }

       
    }
}