using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormProgressBarTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            uiProgressBar1.BarStyle = Resco.UIElements.ProgressBarBarStyle.MarqueeContinuous;//ProgressBarStyle.Marquee;
            uiProgressBar1.MarqueeAnimationSpeed = 30;
            uiProgressBar1.StartMarquee();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "";
        }
    }
}
