/*
 * Title:
 * ProgressBar.cs
 * 
 * Copyright (c) 2011 Socket Mobile Inc.
 * All rights reserved.
 *
 * Description:
 * Progress Bar form for lengthy operation such as
 * reading or writing scanner settings
 *
 * Revision 	Who 		History
 * 04/19/11		EricG		First release
 *
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleScannerTest
{
    public partial class ProgressBar : Form
    {
        private int _count;
        ScannerTest _thisApp;
        bool _bRead;

        public delegate void ProgressBarCompleteDelegate();
        public event ProgressBarCompleteDelegate ProgressBarCompleteEvent;
        
        public ProgressBar(ScannerTest thisApp, bool readSettings, int count)
        {
            InitializeComponent();
            _count = count;
            _bRead = readSettings;
            _thisApp = thisApp;
            _thisApp.UpdateProgressBarEvent += new ScannerTest.UpdateProgressBarDelegate(UpdateProgressBarEvent);
        }

        void UpdateProgressBarEvent(bool bForceToClose)
        {
            if (bForceToClose == false)
                Increase(1);
            else
                Close();
        }

        private void ProgressBar_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = _count;
            if (_bRead)
                labelText.Text = "Reading scanner settings, please wait...";
            else
                labelText.Text = "Writing scanner settings, please wait...";
        }

        /// <summary>
        /// Increase process bar
        /// </summary>
        /// <param name="nValue">the value increased</param>
        /// <returns></returns>
        public bool Increase(int nValue)
        {
            if (nValue > 0)
            {
                if (progressBar1.Value + nValue < progressBar1.Maximum)
                {
                    progressBar1.Value += nValue;
                    return true;
                }
                else
                {
                    progressBar1.Value = progressBar1.Maximum;
                    this.Close();
                    if (ProgressBarCompleteEvent != null)
                        ProgressBarCompleteEvent();
                    return false;
                }
            }
            return false;
        }

        private void ProgressBar_Closing(object sender, CancelEventArgs e)
        {
            _thisApp.UpdateProgressBarEvent -= new ScannerTest.UpdateProgressBarDelegate(UpdateProgressBarEvent);

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}