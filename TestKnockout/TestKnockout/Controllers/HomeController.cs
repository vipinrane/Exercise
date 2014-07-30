using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestKnockout.ServiceReference1;

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

        public ActionResult BookListUsingREST()
        {
            string ServiceUrl = "http://localhost:54883/Service1.svc/RESTService/GetBooksList";
            ///////call Web Method from RESTfull service 
            var webRequest = (HttpWebRequest)WebRequest.Create(ServiceUrl);
            webRequest.Method = "GET";
            List<BookModel> bookModelList = new List<BookModel>();
            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    var reader = new StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();
                    var arr = JsonConvert.DeserializeObject<JArray>(s);

                    foreach (JObject obj in arr)
                    {
                        BookModel objBookModel = new BookModel();
                        objBookModel.ID = (Int32)obj["ID"];
                        objBookModel.BookName = (string)obj["BookName"];
                        bookModelList.Add(objBookModel);
                    } // foreach
                } // if (webResponse.StatusCode == HttpStatusCode.OK)
            } // using HttpWebResponse
            var bookList = bookModelList;
            /////////////

            return Json(bookList, JsonRequestBehavior.AllowGet);
        }
       
    }
}
