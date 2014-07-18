using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestKnockout.Controllers
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

        public ActionResult BookList()
        {
            ViewBag.Message = "Your contact page.";
            ServiceReference1.Service1Client objServiceClient = new ServiceReference1.Service1Client();
            var bookList = objServiceClient.GetBooksList();
            return View();
        }
    }
}
