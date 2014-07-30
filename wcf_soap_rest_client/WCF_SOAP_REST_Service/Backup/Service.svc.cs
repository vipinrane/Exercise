using System.ServiceModel.Activation;

namespace WCF_SOAP_REST_Service
{
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Required)]
    public class Service : IService
    {
        public Employee[] GetEmployees()
        {
            return new Employee[] 
             {
                  new Employee() {EmpNo=101,EmpName="Mahesh",DeptName="CTD"},
                  new Employee() {EmpNo=102,EmpName="Akash",DeptName="HRD"}
             };
        }
    }
}
