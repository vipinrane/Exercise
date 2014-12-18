namespace TestReadXML
{
    partial class frmMain
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
            this.lbSettings = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReadXML = new System.Windows.Forms.Button();
            this.btnReadEmployee = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbSettings
            // 
            this.lbSettings.Location = new System.Drawing.Point(24, 73);
            this.lbSettings.Name = "lbSettings";
            this.lbSettings.Size = new System.Drawing.Size(201, 156);
            this.lbSettings.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 20);
            this.button1.TabIndex = 3;
            this.button1.Text = "Read";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(102, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 20);
            this.button2.TabIndex = 4;
            this.button2.Text = "Write";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(180, 24);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 20);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReadXML
            // 
            this.btnReadXML.Location = new System.Drawing.Point(24, 245);
            this.btnReadXML.Name = "btnReadXML";
            this.btnReadXML.Size = new System.Drawing.Size(72, 20);
            this.btnReadXML.TabIndex = 6;
            this.btnReadXML.Text = "ReadXML";
            this.btnReadXML.Click += new System.EventHandler(this.btnReadXML_Click);
            // 
            // btnReadEmployee
            // 
            this.btnReadEmployee.Location = new System.Drawing.Point(102, 245);
            this.btnReadEmployee.Name = "btnReadEmployee";
            this.btnReadEmployee.Size = new System.Drawing.Size(107, 20);
            this.btnReadEmployee.TabIndex = 7;
            this.btnReadEmployee.Text = "Read Employees";
            this.btnReadEmployee.Click += new System.EventHandler(this.btnReadEmployee_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnReadEmployee);
            this.Controls.Add(this.btnReadXML);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbSettings);
            this.Menu = this.mainMenu1;
            this.Name = "frmMain";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbSettings;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReadXML;
        private System.Windows.Forms.Button btnReadEmployee;
    }
}