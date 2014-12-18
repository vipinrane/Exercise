using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Collections.Specialized;

namespace TimeSpanTest
{
    public partial class Form1 : Form
    {
        Stopwatch timer = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
        }

        public void GetSpan()
        {
            //Hashtable hs = new Hashtable();
            //StringCollection sc = new StringCollection();

            //for (int i = 0; i < int.MaxValue / 1000; i++)
            //{
            //    hs.Add(i.ToString(), i);
            //    sc.Add(i.ToString());
            //}

            Random rnd = new Random();


            int val = rnd.Next(int.MaxValue / 1000);

            timer.Start();
            //if (sc.Contains(val.ToString()))
            //{
            //    Console.WriteLine("Contains");
            //}
            int collection = int.Parse(txtLength.Text);
            for (int i = 0; i < collection; i++)
            {
                int abc = i + int.Parse(txtLength.Text);
            }
            timer.Stop();

            Console.WriteLine(timer.ElapsedTicks);
            label1.Text = timer.ElapsedTicks.ToString();
            timer.Reset();

        }

        public void WriteToWindowsEvents()
        {
            //EventLog eventLog1 = new EventLog();
            //eventLog1.Source = "test";

            //eventLog1.WriteEntry("Dot Net Perls article being written.");

            //eventLog1.WriteEntry("Please stand by while article continues.",
            //EventLogEntryType.Information);

            //eventLog1.WriteEntry("This website is being worked on.",
            //EventLogEntryType.Warning,
            //1000);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        private void btnGetTimeSpan_Click(object sender, EventArgs e)
        {
            GetSpan();
        }

    }
}