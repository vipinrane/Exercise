using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartDeviceWithCF2._0
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dytrexLoggingDBDataSet.DytrexLog' table. You can move, or remove it, as needed.
            this.dytrexLogTableAdapter.Fill(this.dytrexLoggingDBDataSet.DytrexLog);

        }
    }
}