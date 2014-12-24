namespace SmartDeviceWithCF2._0
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnCreateLog = new System.Windows.Forms.Button();
            this.txtLogRead = new System.Windows.Forms.TextBox();
            this.btnOPenLogFile = new System.Windows.Forms.Button();
            this.btnclr = new System.Windows.Forms.Button();
            this.btnDBLogging = new System.Windows.Forms.Button();
            this.dytrexLogTableAdapterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dytrexLogTableAdapterBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateLog
            // 
            this.btnCreateLog.Location = new System.Drawing.Point(3, 212);
            this.btnCreateLog.Name = "btnCreateLog";
            this.btnCreateLog.Size = new System.Drawing.Size(72, 20);
            this.btnCreateLog.TabIndex = 1;
            this.btnCreateLog.Text = "Create Log";
            this.btnCreateLog.Click += new System.EventHandler(this.btnCreateLog_Click_1);
            // 
            // txtLogRead
            // 
            this.txtLogRead.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtLogRead.Location = new System.Drawing.Point(0, 0);
            this.txtLogRead.Multiline = true;
            this.txtLogRead.Name = "txtLogRead";
            this.txtLogRead.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogRead.Size = new System.Drawing.Size(240, 206);
            this.txtLogRead.TabIndex = 3;
            // 
            // btnOPenLogFile
            // 
            this.btnOPenLogFile.Location = new System.Drawing.Point(81, 212);
            this.btnOPenLogFile.Name = "btnOPenLogFile";
            this.btnOPenLogFile.Size = new System.Drawing.Size(97, 20);
            this.btnOPenLogFile.TabIndex = 4;
            this.btnOPenLogFile.Text = "Open Log File";
            this.btnOPenLogFile.Click += new System.EventHandler(this.btnOPenLogFile_Click_1);
            // 
            // btnclr
            // 
            this.btnclr.Location = new System.Drawing.Point(184, 212);
            this.btnclr.Name = "btnclr";
            this.btnclr.Size = new System.Drawing.Size(51, 20);
            this.btnclr.TabIndex = 5;
            this.btnclr.Text = "Clear";
            // 
            // btnDBLogging
            // 
            this.btnDBLogging.Location = new System.Drawing.Point(3, 238);
            this.btnDBLogging.Name = "btnDBLogging";
            this.btnDBLogging.Size = new System.Drawing.Size(72, 20);
            this.btnDBLogging.TabIndex = 6;
            this.btnDBLogging.Text = "DB Logging";
            this.btnDBLogging.Click += new System.EventHandler(this.btnDBLogging_Click);
            // 
            // dytrexLogTableAdapterBindingSource
            // 
            this.dytrexLogTableAdapterBindingSource.DataSource = typeof(SmartDeviceWithCF2._0.DytrexLoggingDBDataSetTableAdapters.DytrexLogTableAdapter);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.DataSource = this.dytrexLogTableAdapterBindingSource;
            this.dataGrid1.Location = new System.Drawing.Point(3, 264);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(229, 46);
            this.dataGrid1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 359);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnDBLogging);
            this.Controls.Add(this.btnclr);
            this.Controls.Add(this.btnOPenLogFile);
            this.Controls.Add(this.txtLogRead);
            this.Controls.Add(this.btnCreateLog);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Use Log4Net in CF2.0";
            ((System.ComponentModel.ISupportInitialize)(this.dytrexLogTableAdapterBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateLog;
        private System.Windows.Forms.TextBox txtLogRead;
        private System.Windows.Forms.Button btnOPenLogFile;
        private System.Windows.Forms.Button btnclr;
        private System.Windows.Forms.Button btnDBLogging;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.BindingSource dytrexLogTableAdapterBindingSource;
    }
}

