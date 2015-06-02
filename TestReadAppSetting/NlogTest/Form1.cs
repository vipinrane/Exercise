using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NlogTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Debug("We're going to throw an exception now.");
                Logger.Trace("This is a Trace message");
                Logger.Debug("This is a Debug message");
                Logger.Info("This is an Info message");
                Logger.Warn("This is a Warn message");
                Logger.Error("This is an Error message",null);
                Logger.Fatal("This is a Fatal error message");

                throw new ApplicationException();
            }
            catch (ApplicationException ae)
            {
                Logger.Error("Error doing something...", ae);
            }
        }
    }
}