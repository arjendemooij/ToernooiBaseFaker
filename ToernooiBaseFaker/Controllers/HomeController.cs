using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ToernooiBaseFaker.Controllers.Business;
using ToernooiBaseFaker.Models;

namespace ToernooiBaseFaker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }

        public ActionResult ToernooiBaseHome()
        {
            return View();
        }

        public string ToernooiBasePage(string relativeUrl, string linkFormat)
        {
            ToernooiBaseReader reader = new ToernooiBaseReader();
            ToernooiBaseParser parser = new ToernooiBaseParser();

            ToernooiBaseHtml html = parser.ParseHtml(reader.GetToernooibaseHtml(relativeUrl), HttpUtility.UrlDecode(relativeUrl),  HttpUtility.UrlDecode(linkFormat));

            return html.Html;
        }

        
    }
}
