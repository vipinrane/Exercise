using System;
using System.Windows.Forms;
using TestEventService;
using OpenNETCF.IoC;
using OpenNETCF;

namespace TestEvents
{
    public partial class Form1 : Form
    {
        private TestService objService { get; set; }
        private TestEvent objEvent { get; set; }

        public Form1()
        {
            objService = RootWorkItem.Services.AddNew<TestService>();
            objEvent = RootWorkItem.Services.AddNew<TestEvent>();
            //objEvent.SayHello += objService.TestHello;
            //objEvent.GetCount += objService.TestGetCount;
            InitializeComponent();
        }

        [EventSubscription(TestEventNames.SayHello, ThreadOption.UserInterface)]
        private void btn_Click(object sender, EventArgs e)
        {
            objEvent.RaiseSayHello();
            label1.Text = objEvent.RaiseGetCount().ToString();
        }
    }
}