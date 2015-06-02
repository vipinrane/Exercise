using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestEventDelegates
{
    public partial class TestEventDelegate : Form
    {
        public delegate void GetMessageHandler(string message);
        public event GetMessageHandler eventReadMessage;

        public delegate string GetNameHandler(string firstName, string lastName);
        public event GetNameHandler eventGetNameHandler;

        public TestEventDelegate()
        {
            InitializeComponent();
            eventReadMessage += new GetMessageHandler(GetTestMessage);
            eventGetNameHandler += new GetNameHandler(GetCompleteName);
        }

        private void btnGetMessage_Click(object sender, EventArgs e)
        {
            if (eventReadMessage != null)
            {
                eventReadMessage(txtName.Text);
            }
            AddDelay();

            SetDelay(1000);


            if (eventGetNameHandler != null)
            {
                txtName.Text = eventGetNameHandler("AAA", "BBB");
            }


        }
        public void AddDelay()
        {
            for (int i = 0; i < 20000; i++)
            {
                int amount = 200;
                int result = (amount / 100) * (200 + 10 - 50);
            }
        }
        public void GetTestMessage(string name)
        {
            txtMessage.Text = "ABCD";
        }

        public string GetCompleteName(string firstName, string lastName)
        {
            return string.Concat(firstName, " - ", lastName);
        }

        private void SetDelay(int counter)
        {
            // Display the ProgressBar control.
            progressBar1.Visible = true;
            // Set Minimum to 1 to represent the first file being copied.
            progressBar1.Minimum = 1;
            // Set Maximum to the total number of files to copy.
            progressBar1.Maximum = counter;
            // Set the initial value of the ProgressBar.
            progressBar1.Value = 1;
            // Set the Step property to a value of 1 to represent each file being copied.
            //progressBar1.Step = 1;

            // Loop through all files to copy. 
            for (int x = 1; x <= counter; x++)
            {
                progressBar1.Value = x;
            }
        }
    }
}