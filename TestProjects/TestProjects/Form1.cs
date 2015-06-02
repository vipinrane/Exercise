using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestProjects
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void uiButton1_Click(object sender, Resco.UIElements.UIMouseEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                uiProgressBar1.Value = i;
            }
        }

    }
}