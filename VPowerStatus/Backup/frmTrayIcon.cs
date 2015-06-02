using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TrayIconExp
{
    public partial class frmTrayIcon : Form
    {
        public frmTrayIcon()
        {
            InitializeComponent();
        }

        //Tray_01: Variable to know when should I exit the form
        private bool EndNow = false;
        private int m_secs ; 

        //Tray_02: Simply wait for the Seconds to elapse
        private void btnMonitor_Click(object sender, EventArgs e)
        {
            m_secs = Int32.Parse(txtSeconds.Text);            
            timer.Enabled = true;
        }

        //Tray_03: Implement Context Menu 
        private void mnuRemaining_Click(object sender, EventArgs e)
        {
            this.Show();
        }
        private void mnuExit_Click(object sender, EventArgs e)
        {
            EndNow = true;
            this.Close();
        }

        //Tray_04: Timer will display the remaining seconds and ends 
        //              when all the seconds passed by. Also it shows balloon tip
        //              When you are in form hided state (Tray iconified)
        private void timer_Tick(object sender, EventArgs e)
        {
            m_secs = m_secs - 1;
            lblElapsed.Text = m_secs.ToString();
            if (m_secs == 0)
            {
                timer.Enabled = false;
                if (this.Visible == false)
                    TrayIcon.ShowBalloonTip(5);
            }
        }

        //Tray_05: When the close button clicked, hide the form and show TrayIcon
        private void frmTrayIcon_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (EndNow == false)
            {
                e.Cancel = true;
                Application.DoEvents();
                this.Hide();
                TrayIcon.Visible = true;
            }
        }

        //Tray_06: Set the Tray Icon visibility to False
        private void frmTrayIcon_Load(object sender, EventArgs e)
        {
            TrayIcon.Visible = false;
        }

    }
}