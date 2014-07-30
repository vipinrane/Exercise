using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;
using TestWeb1.Models;

namespace TestWeb1.Controllers
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
            var client = new WebClient();
            var data = client.DownloadData("http://localhost:5460/Service.svc/XmlService/Employees");
            var stream = new MemoryStream(data);
            var obj = new DataContractJsonSerializer(typeof(ServiceReference1.Employee[]));
            var result = (List<EmployeeModel>)obj.ReadObject(stream);
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            //ServiceReference1.ServiceClient objServiceClient = new ServiceReference1.ServiceClient();

            //var bookList = objServiceClient.GetEmployees();

            string ServiceUrl = "http://localhost:5460/Service.svc/XmlService/Employees";
            WebRequest theRequest = WebRequest.Create(ServiceUrl);
            WebResponse theResponse = theRequest.GetResponse();


            DataContractSerializer collectionData = new DataContractSerializer(typeof(ServiceReference1.Employee[]));

            var arrEmp = collectionData.ReadObject(theResponse.GetResponseStream());

            var collection = arrEmp as ServiceReference1.Employee[];
            return View();
        }
    }
}
