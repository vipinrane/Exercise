namespace TimeSpanTest
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnGetTimeSpan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(3, 84);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(72, 20);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnGetTimeSpan
            // 
            this.btnGetTimeSpan.Location = new System.Drawing.Point(111, 84);
            this.btnGetTimeSpan.Name = "btnGetTimeSpan";
            this.btnGetTimeSpan.Size = new System.Drawing.Size(95, 20);
            this.btnGetTimeSpan.TabIndex = 1;
            this.btnGetTimeSpan.Text = "GetTimeSpan";
            this.btnGetTimeSpan.Click += new System.EventHandler(this.btnGetTimeSpan_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "label1";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(106, 4);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(100, 21);
            this.txtLength.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetTimeSpan);
            this.Controls.Add(this.btnStart);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnGetTimeSpan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLength;
    }
}

