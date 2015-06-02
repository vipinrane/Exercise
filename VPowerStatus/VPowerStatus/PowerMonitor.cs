using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPowerStatus
{
    public partial class PowerMonitor : Form
    {
        public PowerMonitor()
        {
            InitializeComponent();
            timer1.Interval = 1000; //Period of Tick
            timer1.Tick += timer1_Tick;
        }

        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckBatteryStatus();
        }
        private void CheckBatteryStatus()
        {
            PowerStatus pw = SystemInformation.PowerStatus;

            if (pw.BatteryLifeRemaining >= 90)
            {
                //Do stuff here
            }
        }

    }
}
