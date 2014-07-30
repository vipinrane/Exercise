using System;
using System.Windows;
using System.Net;
using System.Runtime.Serialization;

namespace WPF4_WCF_SOAP_REST_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSOAPCall_Click(object sender, RoutedEventArgs e)
        {
            MyRef.ServiceClient ProxySOAP = new MyRef.ServiceClient();
            dgEmpSOAP.ItemsSource = ProxySOAP.GetEmployees();  
        }

        private void btnRESTCall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ServiceUrl = "http://localhost:5460/Service.svc/XmlService/Employees";
                WebRequest theRequest = WebRequest.Create(ServiceUrl);
                WebResponse theResponse = theRequest.GetResponse();
                DataContractSerializer collectionData = new DataContractSerializer(typeof(MyRef.Employee[]));

                var arrEmp = collectionData.ReadObject(theResponse.GetResponseStream());

          dgEmpREST.ItemsSource = arrEmp as MyRef.Employee[];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
