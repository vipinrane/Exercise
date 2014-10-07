using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace EventAndDelegateTest
{
    public delegate void DelEventHandler();

    class Program : Form
    {
        public event DelEventHandler add;

        public Program()
        {
            //Designing button over form.
            Button btn = new Button();
            btn.Parent = this;
            btn.Text = "Click here";
            btn.Location = new Point(10, 50);

            //Event handler is assigned to the button click event
            btn.Click += new EventHandler(OnClick);

            btn = new Button();
            btn.Parent = this;
            btn.Text = "Click here to exit";
            btn.Location = new Point(100, 50);
            btn.Click += new EventHandler(OnExit_Click);


            add += new DelEventHandler(Initiate);//Event subscription-Registration
            //invoke the event
            add();//call

        }

        //call when event if fired
        public void Initiate()
        {
            Console.WriteLine("Event initiated.");
        }

        //Call when button is clicked
        public void OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Clicked me.");
        }

        //Call when exit is clicked
        public void OnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }


        static void Main(string[] args)
        {
            Application.Run(new Program());
            Console.ReadLine();
        }
    }
}
