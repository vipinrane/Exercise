using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using KnockModel;

namespace TestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        // TODO: Add your service operations here
        [OperationContract]
        //[WebGet]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetBooksList")]
        List<BookModel> GetBooksList();

        [OperationContract]
        //[WebGet]
        //[WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "Book/{id}")]
        BookModel GetBookById(string id);

        //[OperationContract]
        ////[WebInvoke(UriTemplate = "AddBook/{name}")]
        //void AddBook(string name);

        //[OperationContract]
        ////[WebInvoke(UriTemplate = "UpdateBook/{id}/{name}")]
        //void UpdateBook(string id, string name);

        //[OperationContract]
        ////[WebInvoke(UriTemplate = "DeleteBook/{id}")]
        //void DeleteBook(string id);

        //[OperationContract]
        ////[WebGet(ResponseFormat = WebMessageFormat.Json)]
        //List<string> GetBooksNames();
    }
}
