using System.ServiceModel;
using System.ServiceModel.Web;

namespace RESTService.Lib
{
    [ServiceContract(Name = "RESTDemoServices")]
    public interface IRESTDemoServices
    {
        [OperationContract]
        [WebGet(UriTemplate = Routing.GetClientRoute, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetClientNameById(string Id);
    }

    public static class Routing
    {
        public const string GetClientRoute = "/Client/{id}";
    }
}
