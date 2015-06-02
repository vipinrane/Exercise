namespace TestProgressBar
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
            this.btnStartProgressBar = new System.Windows.Forms.Button();
            this.btnStopProgressBar = new System.Windows.Forms.Button();
            this.progressBar1 = new Resco.Controls.ProgressBar.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.uiProgressBar1 = new Resco.UIElements.UIProgressBar();
            this.uiElementPanelControl1 = new Resco.UIElements.Controls.UIElementPanelControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.uiProgressBar2 = new Resco.UIElements.UIProgressBar();
            this.uiElementPanelControl1.SuspendElementLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartProgressBar
            // 
            this.btnStartProgressBar.Location = new System.Drawing.Point(3, 46);
            this.btnStartProgressBar.Name = "btnStartProgressBar";
            this.btnStartProgressBar.Size = new System.Drawing.Size(115, 20);
            this.btnStartProgressBar.TabIndex = 1;
            this.btnStartProgressBar.Text = "StartProgressBar";
            this.btnStartProgressBar.Click += new System.EventHandler(this.btnStartProgressBar_Click);
            // 
            // btnStopProgressBar
            // 
            this.btnStopProgressBar.Location = new System.Drawing.Point(124, 46);
            this.btnStopProgressBar.Name = "btnStopProgressBar";
            this.btnStopProgressBar.Size = new System.Drawing.Size(113, 20);
            this.btnStopProgressBar.TabIndex = 4;
            this.btnStopProgressBar.Text = "Stop ProgressBar";
            this.btnStopProgressBar.Click += new System.EventHandler(this.btnStopProgressBar_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.BarStyle = Resco.Controls.ProgressBar.ProgressBarBarStyle.MarqueeBlocks;
            this.progressBar1.Location = new System.Drawing.Point(23, 206);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(164, 20);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Text = "0 %";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(140, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "label1";
            // 
            // uiProgressBar1
            // 
            this.uiProgressBar1.BarStyle = Resco.UIElements.ProgressBarBarStyle.MarqueeContinuous;
            this.uiProgressBar1.Layout = new Resco.UIElements.ElementLayout(Resco.UIElements.HAlignment.Left, Resco.UIElements.VAlignment.Top, 0, 0, 0, 0, 164, 20);
            this.uiProgressBar1.MarqueeAnimationSpeed = 80;
            this.uiProgressBar1.Name = "uiProgressBar1";
            this.uiProgressBar1.ShowText = false;
            // 
            // uiElementPanelControl1
            // 
            this.uiElementPanelControl1.Children.Add(this.uiProgressBar1);
            this.uiElementPanelControl1.Children.Add(this.uiProgressBar2);
            this.uiElementPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiElementPanelControl1.Name = "uiElementPanelControl1";
            this.uiElementPanelControl1.Size = new System.Drawing.Size(240, 77);
            this.uiElementPanelControl1.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 226);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 100);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.uiElementPanelControl1);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 77);
            this.tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(240, 77);
            this.tabPage2.Text = "tabPage2";
            // 
            // uiProgressBar2
            // 
            this.uiProgressBar2.BarStyle = Resco.UIElements.ProgressBarBarStyle.MarqueeContinuous;
            this.uiProgressBar2.Layout = new Resco.UIElements.ElementLayout(Resco.UIElements.HAlignment.Left, Resco.UIElements.VAlignment.Top, 63, 35, 0, 0, 164, 20);
            this.uiProgressBar2.Name = "uiProgressBar2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnStopProgressBar);
            this.Controls.Add(this.btnStartProgressBar);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.uiElementPanelControl1.ResumeElementLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartProgressBar;
        private System.Windows.Forms.Button btnStopProgressBar;
        private Resco.Controls.ProgressBar.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private Resco.UIElements.UIProgressBar uiProgressBar1;
        private Resco.UIElements.Controls.UIElementPanelControl uiElementPanelControl1;
        private Resco.UIElements.UIProgressBar uiProgressBar2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

