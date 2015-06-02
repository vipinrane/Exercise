using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using log4net.Config;

namespace SDPDatabaseTest
{
    public partial class Form1 : Form
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType);
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Form1));
        public Form1()
        {
            InitializeComponent();

            InitializeConfig();
        }

        public void InitializeConfig()
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName)+ "\\Config.xml";

            XmlConfigurator.Configure(new System.IO.FileInfo(path));
            //log4net.Config.XmlConfigurator.Configure();
        }

        private void btnTestDBLog_Click(object sender, EventArgs e)
        {
            try
            {
                log.Debug("log Debug");
                log.Info("log Info");
                log.Warn("log Warn");
                log.Error("log Error");
                log.Fatal("log Fatal");
            }
            catch (Exception ex)
            { }
        }
    }
}