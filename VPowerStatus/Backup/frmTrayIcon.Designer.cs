namespace TrayIconExp
{
    partial class frmTrayIcon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrayIcon));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSeconds = new System.Windows.Forms.TextBox();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.lblElapsed = new System.Windows.Forms.Label();
            this.MenuConT = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuRemaining = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.MenuConT.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(97, 5);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "SystemTray Example";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Enter number of seconds :";
            // 
            // txtSeconds
            // 
            this.txtSeconds.Location = new System.Drawing.Point(243, 50);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(82, 26);
            this.txtSeconds.TabIndex = 3;
            // 
            // btnMonitor
            // 
            this.btnMonitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnMonitor.ForeColor = System.Drawing.Color.Black;
            this.btnMonitor.Location = new System.Drawing.Point(62, 97);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Size = new System.Drawing.Size(168, 26);
            this.btnMonitor.TabIndex = 4;
            this.btnMonitor.Text = "Track Second Elapsed";
            this.btnMonitor.UseVisualStyleBackColor = false;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click);
            // 
            // lblElapsed
            // 
            this.lblElapsed.AutoSize = true;
            this.lblElapsed.BackColor = System.Drawing.Color.Navy;
            this.lblElapsed.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsed.ForeColor = System.Drawing.Color.SandyBrown;
            this.lblElapsed.Location = new System.Drawing.Point(239, 101);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(0, 19);
            this.lblElapsed.TabIndex = 5;
            // 
            // MenuConT
            // 
            this.MenuConT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRemaining,
            this.mnuExit});
            this.MenuConT.Name = "MenuConT";
            this.MenuConT.Size = new System.Drawing.Size(178, 48);
            this.MenuConT.Text = "ContextMenu";
            // 
            // mnuRemaining
            // 
            this.mnuRemaining.Name = "mnuRemaining";
            this.mnuRemaining.Size = new System.Drawing.Size(177, 22);
            this.mnuRemaining.Text = "Remaining Seconds";
            this.mnuRemaining.Click += new System.EventHandler(this.mnuRemaining_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(177, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TrayIcon.BalloonTipText = "Time Elapsed. Check the form";
            this.TrayIcon.BalloonTipTitle = "Elapsed";
            this.TrayIcon.ContextMenuStrip = this.MenuConT;
            this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
            this.TrayIcon.Text = "notifyIcon1";
            this.TrayIcon.Visible = true;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmTrayIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(389, 137);
            this.Controls.Add(this.lblElapsed);
            this.Controls.Add(this.btnMonitor);
            this.Controls.Add(this.txtSeconds);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmTrayIcon";
            this.Text = "Tray Icon Example";
            this.Load += new System.EventHandler(this.frmTrayIcon_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTrayIcon_FormClosing);
            this.MenuConT.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSeconds;
        private System.Windows.Forms.Button btnMonitor;
        private System.Windows.Forms.Label lblElapsed;
        private System.Windows.Forms.ContextMenuStrip MenuConT;
        private System.Windows.Forms.ToolStripMenuItem mnuRemaining;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.Timer timer;
    }
}

