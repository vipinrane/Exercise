namespace WFATestEventDelegates
{
    partial class TestEventDelegates
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
            this.btnGetMessage = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGetMessage
            // 
            this.btnGetMessage.Location = new System.Drawing.Point(12, 55);
            this.btnGetMessage.Name = "btnGetMessage";
            this.btnGetMessage.Size = new System.Drawing.Size(75, 23);
            this.btnGetMessage.TabIndex = 0;
            this.btnGetMessage.Text = "GetMessage";
            this.btnGetMessage.UseVisualStyleBackColor = true;
            this.btnGetMessage.Click += new System.EventHandler(this.btnGetMessage_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(28, 9);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(120, 55);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(100, 20);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.Text = "Message";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 98);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 3;
            this.txtName.Text = "Name";
            // 
            // TestEventDelegates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnGetMessage);
            this.Name = "TestEventDelegates";
            this.Text = "TestEventDelegates";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetMessage;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtName;
    }
}