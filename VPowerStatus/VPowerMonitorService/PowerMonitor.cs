using System;
using System.ServiceProcess;
using System.Windows.Forms;

namespace VPowerMonitorService
{
    public partial class PowerMonitor : ServiceBase
    {

        //System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private static System.Timers.Timer timer1;
        private DateTime m_showUntil;
        public PowerMonitor()
        {
            InitializeComponent();
            //timer1.Interval = 1000; //Period of Tick
            //timer1.Tick += timer1_Tick;

            timer1 = new System.Timers.Timer(30000);
            timer1.Enabled = true;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);
        }



        private void timer1_Tick(object sender, System.Timers.ElapsedEventArgs e)
        //private void timer1_Tick(object sender, EventArgs e)
        {
            CheckBatteryStatus();
        }

        protected override void OnStart(string[] args)
        {
            this.Start();
        }

        protected override void OnStop()
        {
            this.Stop();
        }

        //Custom method to Start the timer
        public void Start()
        {
            timer1.Start();
        }
        //Custom method to Stop the timer
        public void Stop()
        {
            timer1.Stop();
        }

        private void CheckBatteryStatus()
        {
            String message = string.Empty;
            PowerStatus pw = SystemInformation.PowerStatus;

            if (pw.BatteryLifeRemaining >= 90)
            {
                //Do stuff here
                message = "Switch OFF the power supply.";
            }
            else if (pw.BatteryLifeRemaining < 15)
            {
                //Do stuff here
                message = "Switch ON the power supply.";
            }
            DisplayNOtification(1,message);
        }

        private void DisplayNOtification(int toolTipDurationInMinutes, string message)
        {
            try
            {
                notifyIcon1.Icon = new System.Drawing.Icon(@"E:\Exercise\VPowerStatus\VPowerMonitorService\battery-icon.ico");
                notifyIcon1.Text = "My applicaiton";
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipText = "I am a NotifyIcon Balloon";
                notifyIcon1.BalloonTipTitle = message;

                //notifyIcon1.ShowBalloonTip(1000);
                notifyIcon1.ShowBalloonTip(toolTipDurationInMinutes * 60 * 1000);
                m_showUntil = DateTime.Now.AddMinutes(toolTipDurationInMinutes);
            }
            catch (Exception ex)
            {
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            m_showUntil = DateTime.MinValue;
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            if (m_showUntil > DateTime.Now)
                notifyIcon1.ShowBalloonTip(60 * 1000);
        }

        //private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        //{

        //}

        //private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        //{
        //    m_showUntil = DateTime.MinValue;
        //}

        //private void notifyIcon1_Click(object sender, EventArgs e)
        //{
        //    string str = "sdsfsf";
        //}
    }
}
