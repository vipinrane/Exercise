using System;

using System.Windows.Forms;
using log4net.Config;
using System.IO;

namespace SmartDeviceWithCF2._0
{
    public partial class Form1 : Form
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Program));

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreateLog_Click_1(object sender, EventArgs e)
        {
            //To get the Config File
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\Config.xml";

            if (System.IO.File.Exists(path))
            {
                XmlConfigurator.Configure(new System.IO.FileInfo(path));
                txtLogRead.Text = string.Empty;
                txtLogRead.Text = "We have Log File";
                Log.Error("This is Compact Framework with log4net demo");
                Log.Info("Info-vipin");
                Log.Debug("Debug-Vipin" + DateTime.Now);
            }
            else
            {
                txtLogRead.Text = "We Dont have file";
            }
        }


        private void btnclr_Click(object sender, EventArgs e)
        {
            txtLogRead.Text = string.Empty;
        }

        private void btnOPenLogFile_Click_1(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\logtxt.txt";

            if (System.IO.File.Exists(path))
            {
                txtLogRead.Text = string.Empty;
                StreamReader streamReader = new StreamReader(path);
                txtLogRead.Text = streamReader.ReadToEnd();
                streamReader.Close();
            }
            else
            {
                txtLogRead.Text = "We dont have Log File";
            }
        }

        private void btnDBLogging_Click(object sender, EventArgs e)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                Log.Error("Test");
            }
            catch (Exception ex)
            {

            }
        }
    }
}