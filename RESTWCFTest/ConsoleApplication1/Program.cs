using System;
using RESTService.Lib;
using System.ServiceModel.Web;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            RESTDemoServices DemoServices = new RESTDemoServices();
            WebServiceHost _serviceHost = new WebServiceHost(DemoServices, new Uri("http://localhost:8000/DEMOService/Client/"));
            _serviceHost.Open();
            Console.Write("Add length:");
            var length = Console.ReadLine();//Console.ReadKey();
            string result = DemoServices.GetClientNameById(length);
            Console.WriteLine("\nResult=" + result);
            Console.ReadLine();
            _serviceHost.Close();
        }
    }
}
