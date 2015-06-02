namespace TestProjects
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
            this.progressBar1 = new Resco.Controls.ProgressBar.ProgressBar();
            this.uiButton1 = new Resco.UIElements.UIButton();
            this.uiElementPanelControl1 = new Resco.UIElements.Controls.UIElementPanelControl();
            this.uiProgressBar1 = new Resco.UIElements.UIProgressBar();
            this.uiPageIndicator1 = new Resco.UIElements.UIPageIndicator();
            this.uiElementPanelControl1.SuspendElementLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(19, 61);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(164, 20);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Text = "0 %";
            // 
            // uiButton1
            // 
            this.uiButton1.Layout = new Resco.UIElements.ElementLayout(Resco.UIElements.HAlignment.Left, Resco.UIElements.VAlignment.Top, 72, 110, 0, 0, 80, 20);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Text = "uiButton1";
            this.uiButton1.Click += new Resco.UIElements.UIMouseEventHandler(this.uiButton1_Click);
            // 
            // uiElementPanelControl1
            // 
            this.uiElementPanelControl1.Children.Add(this.uiButton1);
            this.uiElementPanelControl1.Children.Add(this.uiProgressBar1);
            this.uiElementPanelControl1.Children.Add(this.uiPageIndicator1);
            this.uiElementPanelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiElementPanelControl1.Name = "uiElementPanelControl1";
            this.uiElementPanelControl1.Size = new System.Drawing.Size(240, 268);
            this.uiElementPanelControl1.TabIndex = 1;
            // 
            // uiProgressBar1
            // 
            this.uiProgressBar1.Layout = new Resco.UIElements.ElementLayout(Resco.UIElements.HAlignment.Left, Resco.UIElements.VAlignment.Top, 34, 60, 0, 0, 164, 20);
            this.uiProgressBar1.MarqueeStyle = Resco.UIElements.ProgressBarMarqueeStyle.MoveInside;
            this.uiProgressBar1.Name = "uiProgressBar1";
            this.uiProgressBar1.TabIndex = 1;
            // 
            // uiPageIndicator1
            // 
            this.uiPageIndicator1.DataBindings.Add(new Resco.UIElements.ElementBinding("Tag", "", System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, true, "N2"));
            this.uiPageIndicator1.Layout = new Resco.UIElements.ElementLayout(Resco.UIElements.HAlignment.Left, Resco.UIElements.VAlignment.Top, 17, 234, 0, 0, 200, 8);
            this.uiPageIndicator1.Name = "uiPageIndicator1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.uiElementPanelControl1);
            this.Controls.Add(this.progressBar1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.uiElementPanelControl1.ResumeElementLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Resco.Controls.ProgressBar.ProgressBar progressBar1;
        private Resco.UIElements.UIButton uiButton1;
        private Resco.UIElements.Controls.UIElementPanelControl uiElementPanelControl1;
        private Resco.UIElements.UIProgressBar uiProgressBar1;
        private Resco.UIElements.UIPageIndicator uiPageIndicator1;
    }
}

