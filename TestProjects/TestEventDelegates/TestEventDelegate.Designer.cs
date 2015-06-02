namespace TestEventDelegates
{
    partial class TestEventDelegate
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
            this.btnGetMessage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnGetMessage
            // 
            this.btnGetMessage.Location = new System.Drawing.Point(16, 88);
            this.btnGetMessage.Name = "btnGetMessage";
            this.btnGetMessage.Size = new System.Drawing.Size(80, 20);
            this.btnGetMessage.TabIndex = 0;
            this.btnGetMessage.Text = "GetMessage";
            this.btnGetMessage.Click += new System.EventHandler(this.btnGetMessage_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "label1";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(115, 46);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(86, 21);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.Text = "TestABC";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(115, 88);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.TabIndex = 3;
            this.txtName.Text = "Name";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(164, 20);
            // 
            // TestEventDelegate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetMessage);
            this.Menu = this.mainMenu1;
            this.Name = "TestEventDelegate";
            this.Text = "TestEventDelegate";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGetMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}