using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCF_SOAP_REST_Service
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebGet(UriTemplate="/Employees",ResponseFormat=WebMessageFormat.Json )]
        Employee[] GetEmployees();
    }

    [DataContract]
    public class Employee
    {
        [DataMember]
        public int EmpNo { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public string DeptName { get; set; }
    }

     
}
