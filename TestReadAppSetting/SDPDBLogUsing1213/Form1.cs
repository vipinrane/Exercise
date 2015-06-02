using System;

using System.Windows.Forms;
using System.IO;
using log4net;
using log4net.Appender;
using System.Data.SqlServerCe;
using System.Text;
using log4net.Config;

namespace SDPDBLogUsing1213
{
    public partial class Form1 : Form
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Program));

        public Form1()
        {
            InitializeComponent();
        }
      
        public void Test()
        {
            SqlCeConnection conn = new SqlCeConnection(@"Data Source=\Program Files\DatabaseAccessTest\testDatabase.sdf;");
            try
            {
                conn.Open();
                label1.Text = "Connection!";
            }
            catch (SqlCeException ee)  // <- Notice the use of SqlCeException to read your errors
            {
                SqlCeErrorCollection errorCollection = ee.Errors;

                StringBuilder bld = new StringBuilder();
                Exception inner = ee.InnerException;

                if (null != inner)
                {
                    MessageBox.Show("Inner Exception: " + inner.ToString());
                }
                // Enumerate the errors to a message box.
                foreach (SqlCeError err in errorCollection)
                {
                    bld.Append("\n Error Code: " + err.HResult.ToString("X"));
                    bld.Append("\n Message   : " + err.Message);
                    bld.Append("\n Minor Err.: " + err.NativeError);
                    bld.Append("\n Source    : " + err.Source);

                    // Enumerate each numeric parameter for the error.
                    foreach (int numPar in err.NumericErrorParameters)
                    {
                        if (0 != numPar) bld.Append("\n Num. Par. : " + numPar);
                    }

                    // Enumerate each string parameter for the error.
                    foreach (string errPar in err.ErrorParameters)
                    {
                        if (String.Empty != errPar) bld.Append("\n Err. Par. : " + errPar);
                    }

                }
                label1.Text = bld.ToString();
                bld.Remove(0, bld.Length);
            }
        }

        public void TestDBLogging()
        {
            var codeBase = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            var directory = Path.GetDirectoryName(codeBase);
            var path = Path.Combine(directory, "log4Net.config");

            log4net.Config.XmlConfigurator.Configure(new Uri(path));

            var repo = LogManager.GetRepository();
            var appender1 = repo.GetAppenders().FirstOrDefault(x => x.Name.Equals("SQLCEAppender")) as AdoNetAppender;
            var appender = appender1.GetValue(0) as AdoNetAppender;
            appender.ConnectionString = "Data Source=" + Path.Combine(directory, "DytrexLoggingDB.sdf;Persist Security Info=False;");
            appender.ActivateOptions();

            ILog log = LogManager.GetLogger(typeof(Form1));
            log.Info("Starting application");
            log.Debug("Printing debug");
        }

        private void btnCreateLog_Click(object sender, EventArgs e)
        {
            try
            {
                //To get the Config File
                string path = System.IO.Path.GetDirectoryName(
                   System.Reflection.Assembly.GetExecutingAssembly()
                  .GetModules()[0].FullyQualifiedName)
                  + "\\Config.xml";

                if (System.IO.File.Exists(path))
                {
                    XmlConfigurator.Configure( new System.IO.FileInfo(path));

                    txtLogRead.Text = string.Empty;
                    txtLogRead.Text = "We have Log File";
                    Log.Error("This is Compact Framework with log4net demo");
                    Log.Info("Info-Test");
                    Log.Debug("Debug-Test");
                }
                else
                {
                    txtLogRead.Text = "We Dont have file";
                }


                //Test();
                TestDBLogging();
            }
            catch (Exception ex)
            { }
        }

        private void btnOPenLogFile_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(
              System.Reflection.Assembly.GetExecutingAssembly()
             .GetModules()[0].FullyQualifiedName)
             + "\\logtxt.txt";

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

        private void btnclr_Click_1(object sender, EventArgs e)
        {
            txtLogRead.Text = string.Empty;
        }
    }
}