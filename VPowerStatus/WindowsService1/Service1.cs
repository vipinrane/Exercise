using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        //Declare a timer static variable
        private static System.Timers.Timer _aTimer;

        public Service1()
        {
            _aTimer = new System.Timers.Timer(30000);
            _aTimer.Enabled = true;
            _aTimer.Elapsed += new System.Timers.ElapsedEventHandler(_aTimer_Elapsed);

            InitializeComponent();
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
            _aTimer.Start();
        }
        //Custom method to Stop the timer
        public void Stop()
        {
            _aTimer.Stop();
        }
        //Handle the Timer Elapsed event
        void _aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Create an instance of Process
            Process notePad = new Process();
            //Set the FileName to "notepad.exe"
            notePad.StartInfo.FileName = "notepad.exe";
            //Start the process
            notePad.Start();
            //You may have to write extra code for handling exit code and
            //other System.Process handling code
        }


    }
}
