namespace SmartDeviceProject1
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
            this.uiProgressBar1 = new Resco.UIElements.UIProgressBar();
            this.uiElementPanelControl1 = new Resco.UIElements.Controls.UIElementPanelControl();
            this.button1 = new System.Windows.Forms.Button();
            this.uiElementPanelControl1.SuspendElementLayout();
            this.SuspendLayout();
            // 
            // uiProgressBar1
            // 
            this.uiProgressBar1.Layout = new Resco.UIElements.ElementLayout(Resco.UIElements.HAlignment.Left, Resco.UIElements.VAlignment.Top, 24, 23, 0, 0, 164, 20);
            this.uiProgressBar1.Name = "uiProgressBar1";
            // 
            // uiElementPanelControl1
            // 
            this.uiElementPanelControl1.Children.Add(this.uiProgressBar1);
            this.uiElementPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiElementPanelControl1.Name = "uiElementPanelControl1";
            this.uiElementPanelControl1.Size = new System.Drawing.Size(240, 268);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uiElementPanelControl1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.uiElementPanelControl1.ResumeElementLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Resco.UIElements.UIProgressBar uiProgressBar1;
        private Resco.UIElements.Controls.UIElementPanelControl uiElementPanelControl1;
        private System.Windows.Forms.Button button1;
    }
}

