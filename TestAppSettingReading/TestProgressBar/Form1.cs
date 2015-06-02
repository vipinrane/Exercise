using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestProgressBar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //uiProgressBar1.BarStyle = Resco.UIElements.ProgressBarBarStyle.MarqueeContinuous;//ProgressBarStyle.Marquee;
            //uiProgressBar1.MarqueeAnimationSpeed = 30;
            //uiProgressBar1.StartMarquee();
            uiElementPanelControl1.Show();
        }

        private void btnStartProgressBar_Click(object sender, EventArgs e)
        {
            uiProgressBar1.StartMarquee();
            uiProgressBar1.Visible = true;

            progressBar1.StartMarquee();
            progressBar1.Visible = true;

            label1.Visible = false;
            uiProgressBar2.StartMarquee();
        }

        private void btnStopProgressBar_Click(object sender, EventArgs e)
        {
            uiProgressBar1.StopMarquee();
            uiProgressBar1.Visible = false;

            progressBar1.StopMarquee();
            progressBar1.Visible = false;

            label1.Visible = true;
        }
    }
}