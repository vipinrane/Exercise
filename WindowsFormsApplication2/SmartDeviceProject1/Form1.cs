using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a,b;
            testCommand("a",out a, out b);
            string aa = a;
            string bb = b;
        }

        public void testCommand(string str, out string p1, out string p2)
        {
            str = "aa";
            p1 = "bb";
            p2 = "cc";
        }
    }
}