namespace WindowsFormProgressBarTest
{
    partial class Form1
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
            this.uiProgressBar1 = new Resco.UIElements.UIProgressBar();
            this.uiElementPanelControl1 = new Resco.UIElements.Controls.UIElementPanelControl();
            this.button1 = new System.Windows.Forms.Button();
            this.uiElementPanelControl1.SuspendElementLayout();
            this.SuspendLayout();
            // 
            // uiProgressBar1
            // 
            this.uiProgressBar1.BarStyle = Resco.UIElements.ProgressBarBarStyle.MarqueeContinuous;
            this.uiProgressBar1.Layout = new Resco.UIElements.ElementLayout(Resco.UIElements.HAlignment.Left, Resco.UIElements.VAlignment.Top, 8, 6, 0, 0, 177, 20);
            this.uiProgressBar1.MarqueeAnimationSpeed = 80;
            this.uiProgressBar1.Name = "uiProgressBar1";
            // 
            // uiElementPanelControl1
            // 
            this.uiElementPanelControl1.Children.Add(this.uiProgressBar1);
            this.uiElementPanelControl1.Location = new System.Drawing.Point(72, 103);
            this.uiElementPanelControl1.Name = "uiElementPanelControl1";
            this.uiElementPanelControl1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(106, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 402);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uiElementPanelControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.uiElementPanelControl1.ResumeElementLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Resco.UIElements.UIProgressBar uiProgressBar1;
        private Resco.UIElements.Controls.UIElementPanelControl uiElementPanelControl1;
        private System.Windows.Forms.Button button1;
    }
}

