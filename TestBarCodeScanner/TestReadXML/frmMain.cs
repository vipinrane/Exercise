using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


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
    }
}