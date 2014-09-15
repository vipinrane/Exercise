/*
 * Title:
 * ProgressBar.Designer.cs
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
namespace SimpleScannerTest
{
    partial class ProgressBar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.labelText = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(4, 17);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(223, 20);
            this.labelText.Text = "Reading scanner settings, please wait...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(164, 20);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(76, 79);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 20);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(230, 148);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelText);
            this.MinimizeBox = false;
            this.Name = "ProgressBar";
            this.Text = "ScannerSettings";
            this.Load += new System.EventHandler(this.ProgressBar_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ProgressBar_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button buttonCancel;
    }
}