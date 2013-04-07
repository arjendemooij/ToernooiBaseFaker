using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ToernooiBaseFaker.Models;
using HtmlAgilityPack;
using System.Security.Policy;

namespace ToernooiBaseFaker.Controllers.Business
{
    public class ToernooiBaseParser
    {
        public ToernooiBaseHtml ParseHtml(string html, string toernooiBaseRelativeUrl, string generatedToernooiBaseUrlFormat)
        {
            html = FixMissingRowTags(html);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            FixImageSources(document);
            FixLinkUrls(toernooiBaseRelativeUrl, generatedToernooiBaseUrlFormat, document);

            HtmlNode body = document.DocumentNode.SelectSingleNode("//body");
            return new ToernooiBaseHtml(body.InnerHtml);
        }


        private static string FixMissingRowTags(string html)
        {
            html = html.Replace("</tr>", "</tr><tr>");
            html = html.Replace("</TR>", "</TR><TR>");
            return html;
        }

        private void FixLinkUrls(string toernooiBaseRelativeUrl, string generatedToernooiBaseUrlFormat, HtmlDocument document)
        {
            foreach (HtmlNode link in document.DocumentNode.Descendants("a"))
            {
                string originalUrl = link.GetAttributeValue("href", string.Empty);

                bool useOriginalUrl = IsAppletLink(originalUrl) || IsNonToernooiBaseLink(originalUrl);

                if (useOriginalUrl)
                {
                    if (IsAppletLink(originalUrl))
                    {
                        link.SetAttributeValue("href", "http://toernooibase.kndb.nl/opvraag/" + originalUrl);
                        link.SetAttributeValue("target", "_blank");
                    }
                }
                else
                {
                    string strippedUrl = originalUrl.Replace("http://toernooibase.kndb.nl", string.Empty);
                    string relativeUrl;
                    if (IsRelativeToHome(strippedUrl))
                    {
                        relativeUrl = strippedUrl;
                    }
                    else
                    {
                        relativeUrl = GetCurrentFolder(toernooiBaseRelativeUrl) + strippedUrl;
                    }

                    string newUrl = string.Format(generatedToernooiBaseUrlFormat, HttpUtility.UrlEncode(relativeUrl));

                    link.SetAttributeValue("href", newUrl);
                }
            }
        }

        private static void FixImageSources(HtmlDocument document)
        {
            foreach (HtmlNode image in document.DocumentNode.Descendants("img"))
            {
                string source = image.GetAttributeValue("src", string.Empty);
                source = source.Replace("..", "http://toernooibase.kndb.nl");
                image.SetAttributeValue("src", source);
            }
        }

        private bool IsRelativeToHome(string strippedUrl)
        {
            return strippedUrl.StartsWith("/");
        }

        private bool IsAppletLink(string url)
        {
            return url.Contains("applet.php");
        }

        private bool IsNonToernooiBaseLink(string url)
        {
            return !url.StartsWith("http://toernooibase.kndb.nl") && url.StartsWith("http");
        }



        private string GetCurrentFolder(string currentUrl)
        {
            int lastSlashIndex = currentUrl.LastIndexOf(@"/");
            string currentFolder = currentUrl.Substring(0, lastSlashIndex + 1);

            if (string.IsNullOrEmpty(currentFolder))
            {
                currentFolder = "/";
            }

            return currentFolder;
        }

        public HtmlNode GetRecursiveNode(HtmlNode node, string name)
        {
            if (node.Name == name) return node;

            foreach (HtmlNode child in node.ChildNodes)
            {
                HtmlNode recursiveResult = GetRecursiveNode(child, name);
                if (recursiveResult != null)
                    return recursiveResult;
            }

            return null;

        }
    }
}