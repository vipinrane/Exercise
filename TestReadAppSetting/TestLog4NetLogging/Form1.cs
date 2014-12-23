using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestLog4NetLogging
{
    public partial class Form1 : Form
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Form1));

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Log.Error("This is Compact Framework with log4net demo");
        }
    }
}