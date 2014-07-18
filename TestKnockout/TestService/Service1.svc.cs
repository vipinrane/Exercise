using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using KnockModel;
using KnockoutData;

namespace TestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        BookManager objBookManager = new BookManager();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBooksList")]
        public List<KnockModel.BookModel> GetBooksList()
        {
            return objBookManager.GetBooksList();
        }

        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Book/{id}")]
        public BookModel GetBookById(string id)
        {
            return objBookManager.GetBookById(id);
        }

        //[WebInvoke(UriTemplate = "AddBook/{name}")]
        //public void AddBook(string name)
        //{
        //    objBookManager.AddBook(name);
        //}

        //[WebInvoke(Method = "POST", UriTemplate = "UpdateBook?id={id}&name={name}",
        // BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //public void UpdateBook(string id, string name)
        //{
        //    objBookManager.UpdateBook(id, name);
        //}

        //[WebInvoke(UriTemplate = "DeleteBook/{id}")]
        //public void DeleteBook(string id)
        //{
        //    objBookManager.DeleteBook(id);
        //}

        //[WebGet(ResponseFormat = WebMessageFormat.Json)]
        //public List<string> GetBooksNames()
        //{
        //    return objBookManager.GetBooksNames();
        //}
    }
}
