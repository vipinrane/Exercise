using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Dytrex.EndirePro.BarcodeScanner.Model;


namespace TestReadXML
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            //Settings obj = new Settings();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbSettings.Items.Clear();
            lbSettings.Items.Add("DatabasePath=" + Settings.DatabasePath);
            lbSettings.Items.Add("LastTransmit=" + Settings.LastTransmit);
            lbSettings.Items.Add("Password=" + Settings.Password);
            lbSettings.Items.Add("PhoneNumber=" + Settings.PhoneNumber);
            lbSettings.Items.Add("ServerIP=" + Settings.ServerIP);
            lbSettings.Items.Add("TimeOut=" + Settings.TimeOut);
            lbSettings.Items.Add("UserName=" + Settings.UserName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings.DatabasePath = @"\Test\test.sdf";
            Settings.LastTransmit = System.DateTime.Now.ToString();
            Settings.Password = "etwkdf**";
            Settings.PhoneNumber = "1111111111";
            Settings.ServerIP = "192.168.1.1";
            Settings.TimeOut = "30";
            Settings.UserName = "user2";
            Settings.Update();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReadXML_Click(object sender, EventArgs e)
        {
            string m_settingsPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            m_settingsPath += @"\Employee.xml";

            if (!File.Exists(m_settingsPath))
                throw new FileNotFoundException(m_settingsPath + " could not be found.");

        }

        private void btnReadEmployee_Click(object sender, EventArgs e)
        {
            lbSettings.Items.Clear();
            lbSettings.Items.Add("DataconfirmationMode=" + SettingsNew.DataConfirmationMode);
            lbSettings.Items.Add("Preamble=" + SettingsNew.PreambleDevice);
            lbSettings.Items.Add("Postamble=" + SettingsNew.PostambleDevice);
            lbSettings.Items.Add("SecurityModeDevice=" + SettingsNew.SecurityModeDevice);
            
            lbSettings.Items.Add("SCDGoodScanActionType=" + SettingsNew.SCDGoodScanActionType);
            lbSettings.Items.Add("SCDGoodScanFrequency=" + SettingsNew.SCDGoodScanFrequency);
            lbSettings.Items.Add("SCDGoodScanDuration=" + SettingsNew.SCDGoodScanDuration);
            lbSettings.Items.Add("SCDGoodScanPause=" + SettingsNew.SCDGoodScanPause);

            lbSettings.Items.Add("SCDGoodScanLocalActionType=" + SettingsNew.SCDGoodScanLocalActionType);
            lbSettings.Items.Add("SCDGoodScanLocalFrequency=" + SettingsNew.SCDGoodScanLocalFrequency);
            lbSettings.Items.Add("SCDGoodScanLocalDuration=" + SettingsNew.SCDGoodScanLocalDuration);
            lbSettings.Items.Add("SCDGoodScanLocalPause=" + SettingsNew.SCDGoodScanLocalPause);

            lbSettings.Items.Add("SCDBadScanActionType=" + SettingsNew.SCDBadScanActionType);
            lbSettings.Items.Add("SCDBadScanFrequency=" + SettingsNew.SCDBadScanFrequency);
            lbSettings.Items.Add("SCDBadScanDuration=" + SettingsNew.SCDBadScanDuration);
            lbSettings.Items.Add("SCDBadScanPause=" + SettingsNew.SCDBadScanPause);

            lbSettings.Items.Add("SCDBadScanLocalActionType=" + SettingsNew.SCDBadScanLocalActionType);
            lbSettings.Items.Add("SCDBadScanLocalFrequency=" + SettingsNew.SCDBadScanLocalFrequency);
            lbSettings.Items.Add("SCDBadScanLocalDuration=" + SettingsNew.SCDBadScanLocalDuration);
            lbSettings.Items.Add("SCDBadScanLocalPause=" + SettingsNew.SCDBadScanLocalPause);

        }
    }
}