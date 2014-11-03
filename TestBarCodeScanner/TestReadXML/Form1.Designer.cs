namespace TestReadXML
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lblConfirmationMode = new System.Windows.Forms.Label();
            this.btnGetConfirmationMode = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblConfirmationMode
            // 
            this.lblConfirmationMode.Location = new System.Drawing.Point(41, 10);
            this.lblConfirmationMode.Name = "lblConfirmationMode";
            this.lblConfirmationMode.Size = new System.Drawing.Size(100, 20);
            this.lblConfirmationMode.Text = "label1";
            // 
            // btnGetConfirmationMode
            // 
            this.btnGetConfirmationMode.Location = new System.Drawing.Point(3, 156);
            this.btnGetConfirmationMode.Name = "btnGetConfirmationMode";
            this.btnGetConfirmationMode.Size = new System.Drawing.Size(101, 20);
            this.btnGetConfirmationMode.TabIndex = 1;
            this.btnGetConfirmationMode.Text = "Read";
            this.btnGetConfirmationMode.Click += new System.EventHandler(this.btnGetConfirmationMode_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(29, 76);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(197, 69);
            this.textBox1.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(128, 156);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 20);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnGetConfirmationMode);
            this.Controls.Add(this.lblConfirmationMode);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblConfirmationMode;
        private System.Windows.Forms.Button btnGetConfirmationMode;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnExit;
    }
}

