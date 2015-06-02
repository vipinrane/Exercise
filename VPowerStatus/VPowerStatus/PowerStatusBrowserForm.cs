using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPowerStatus
{
    public partial class PowerStatusBrowserForm : Form
    {
        public PowerStatusBrowserForm()
        {
            InitializeComponent();
            this.SuspendLayout();
            //InitForm();

            //Add each property of the PowerStatus class to the list box.
            Type t = typeof(System.Windows.Forms.PowerStatus);
            PropertyInfo[] pi = t.GetProperties();
            for (int i = 0; i < pi.Length; i++)
                listBox1.Items.Add(pi[i].Name);
            textBox1.Text = "The PowerStatus class has " + pi.Length.ToString() + " properties.\r\n";

            // Configure the list item selected handler for the list box to invoke a  
            // method that displays the value of each property.           
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            this.ResumeLayout(false);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Return if no item is selected. 
            if (listBox1.SelectedIndex == -1) return;
            // Get the property name from the list item 
            string propname = listBox1.Text;

            // Display the value of the selected property of the PowerStatus type.
            Type t = typeof(System.Windows.Forms.PowerStatus);
            PropertyInfo[] pi = t.GetProperties();
            PropertyInfo prop = null;
            for (int i = 0; i < pi.Length; i++)
                if (pi[i].Name == propname)
                {
                    prop = pi[i];
                    break;
                }

            object propval = prop.GetValue(SystemInformation.PowerStatus, null);
            textBox1.Text += "\r\nThe value of the " + propname + " property is: " + propval.ToString();

            if (prop.Name.Contains("BatteryLifePercent"))
            {
                ///Here 0.15 is lowest threshold for battery charge.
                ///and 0.90 is highest threshold for battery charge.
                int belowChargeCheck = Decimal.Compare(Convert.ToDecimal(propval), new decimal(0.15));
                int overChargeCheck = Decimal.Compare(Convert.ToDecimal(propval), new decimal(0.90));
                string message = string.Empty;
                if (belowChargeCheck < 0)
                {
                    message = "Switch On the power supply to charge the battery.";
                }
                else if (overChargeCheck > 0)
                {
                    message = "Switch Off the power supply.";
                }
                MessageBox.Show("Battery Charge=" + (Convert.ToDecimal(propval) * 100).ToString() + "% \r\n" + message);
            }
        }

        public static int CompareDecimals(decimal first, decimal second)
        {
            ///Decimal.Compare return three type of outputs.
            ///if result > 0, means first is greater than second.
            ///if result = 0, means first and second are equal.
            ///if result < 0, means first is less than second.
            int result = Decimal.Compare(first, second);
            return result;
        }


        private void InitForm()
        {
            // Initialize the form settings 
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Location = new System.Drawing.Point(8, 16);
            this.listBox1.Size = new System.Drawing.Size(172, 496);
            this.listBox1.TabIndex = 0;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(188, 16);
            this.textBox1.Multiline = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(420, 496);
            this.textBox1.TabIndex = 1;
            this.ClientSize = new System.Drawing.Size(616, 525);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Text = "Select a PowerStatus property to get the value of";
        }

    }
}
