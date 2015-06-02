namespace SGCustomScrollBar
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
            this.smartGrid1 = new Resco.Controls.SmartGrid.SmartGrid();
            this.vScrollBar1 = new Resco.Controls.ScrollBar.VScrollBar();
            this.scrollBarButtonDown = new Resco.Controls.ScrollBar.ScrollBarButton();
            this.letterBar = new Resco.Controls.ScrollBar.LetterBar();
            this.scrollBarVThumb = new Resco.Controls.ScrollBar.ScrollBarThumb();
            this.scrollBarButtonUp = new Resco.Controls.ScrollBar.ScrollBarButton();
            this.hScrollBar1 = new Resco.Controls.ScrollBar.HScrollBar();
            this.scrollBarButtonLeft = new Resco.Controls.ScrollBar.ScrollBarButton();
            this.scrollBarButtonRight = new Resco.Controls.ScrollBar.ScrollBarButton();
            this.scrollBarHThumb = new Resco.Controls.ScrollBar.ScrollBarThumb();
            this.timer1 = new System.Windows.Forms.Timer();
            this.smartGrid1.SuspendLayout();
            this.SuspendLayout();
            // 
            // smartGrid1
            // 
            this.smartGrid1.Controls.Add(this.vScrollBar1);
            this.smartGrid1.Controls.Add(this.hScrollBar1);
            this.smartGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartGrid1.FullRowSelect = true;
            this.smartGrid1.HeaderVistaStyle = true;
            this.smartGrid1.HScrollBar = this.hScrollBar1;
            this.smartGrid1.Location = new System.Drawing.Point(0, 0);
            this.smartGrid1.Name = "smartGrid1";
            this.smartGrid1.ScrollHeight = 16;
            this.smartGrid1.ScrollWidth = 28;
            this.smartGrid1.SelectionVistaStyle = true;
            this.smartGrid1.Size = new System.Drawing.Size(240, 268);
            this.smartGrid1.TabIndex = 0;
            this.smartGrid1.TouchScrolling = true;
            this.smartGrid1.VScrollBar = this.vScrollBar1;
            this.smartGrid1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.smartGrid1_MouseUp);
            this.smartGrid1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.smartGrid1_MouseDown);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.DownButton = this.scrollBarButtonDown;
            this.vScrollBar1.Extension = this.letterBar;
            this.vScrollBar1.ExtensionWidth = 20;
            this.vScrollBar1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.vScrollBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.vScrollBar1.LargeChange = 0;
            this.vScrollBar1.Location = new System.Drawing.Point(210, 0);
            this.vScrollBar1.Maximum = 0;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(28, 250);
            this.vScrollBar1.SmallChange = 0;
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.TabStop = false;
            this.vScrollBar1.Thumb = this.scrollBarVThumb;
            this.vScrollBar1.UpButton = this.scrollBarButtonUp;
            // 
            // scrollBarButtonDown
            // 
            this.scrollBarButtonDown.GradientColor = new Resco.Controls.ScrollBar.GradientColor(System.Drawing.Color.DimGray, System.Drawing.Color.Transparent, System.Drawing.Color.Transparent, System.Drawing.Color.LightGray, 50, 50, Resco.Controls.ScrollBar.FillDirection.Vertical);
            // 
            // letterBar
            // 
            this.letterBar.BorderStyle = Resco.Controls.ScrollBar.ScrollBarBorderStyle.None;
            this.letterBar.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.letterBar.GradientColor = new Resco.Controls.ScrollBar.GradientColor(System.Drawing.Color.LightGray, System.Drawing.Color.Transparent, System.Drawing.Color.Transparent, System.Drawing.Color.DimGray, 50, 50, Resco.Controls.ScrollBar.FillDirection.Vertical);
            this.letterBar.IndexToValue += new Resco.Controls.ScrollBar.ScrollBarExtensionBase.ValueIndexConversionHandler(this.letterBar_IndexToValue);
            this.letterBar.ValueToIndex += new Resco.Controls.ScrollBar.ScrollBarExtensionBase.ValueIndexConversionHandler(this.letterBar_ValueToIndex);
            // 
            // scrollBarVThumb
            // 
            this.scrollBarVThumb.GradientColor = new Resco.Controls.ScrollBar.GradientColor(System.Drawing.Color.DimGray, System.Drawing.Color.LightGray, System.Drawing.Color.Transparent, System.Drawing.Color.DimGray, 50, 50, Resco.Controls.ScrollBar.FillDirection.Vertical);
            // 
            // scrollBarButtonUp
            // 
            this.scrollBarButtonUp.GradientColor = new Resco.Controls.ScrollBar.GradientColor(System.Drawing.Color.LightGray, System.Drawing.Color.Transparent, System.Drawing.Color.Transparent, System.Drawing.Color.DimGray, 50, 50, Resco.Controls.ScrollBar.FillDirection.Vertical);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.hScrollBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.hScrollBar1.LargeChange = 1;
            this.hScrollBar1.LeftButton = this.scrollBarButtonLeft;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 250);
            this.hScrollBar1.Maximum = 0;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.RightButton = this.scrollBarButtonRight;
            this.hScrollBar1.Size = new System.Drawing.Size(238, 16);
            this.hScrollBar1.TabIndex = 1;
            this.hScrollBar1.TabStop = false;
            this.hScrollBar1.Thumb = this.scrollBarHThumb;
            // 
            // scrollBarButtonLeft
            // 
            this.scrollBarButtonLeft.GradientColor = new Resco.Controls.ScrollBar.GradientColor(System.Drawing.Color.LightGray, System.Drawing.Color.Transparent, System.Drawing.Color.Transparent, System.Drawing.Color.DimGray, 50, 50, Resco.Controls.ScrollBar.FillDirection.Horizontal);
            // 
            // scrollBarButtonRight
            // 
            this.scrollBarButtonRight.GradientColor = new Resco.Controls.ScrollBar.GradientColor(System.Drawing.Color.DimGray, System.Drawing.Color.Transparent, System.Drawing.Color.Transparent, System.Drawing.Color.LightGray, 50, 50, Resco.Controls.ScrollBar.FillDirection.Horizontal);
            // 
            // scrollBarHThumb
            // 
            this.scrollBarHThumb.GradientColor = new Resco.Controls.ScrollBar.GradientColor(System.Drawing.Color.DimGray, System.Drawing.Color.LightGray, System.Drawing.Color.Transparent, System.Drawing.Color.DimGray, 50, 50, Resco.Controls.ScrollBar.FillDirection.Horizontal);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.smartGrid1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.smartGrid1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private Resco.Controls.SmartGrid.SmartGrid smartGrid1;
		private Resco.Controls.ScrollBar.VScrollBar vScrollBar1;
		private Resco.Controls.ScrollBar.ScrollBarButton scrollBarButtonUp;
		private Resco.Controls.ScrollBar.ScrollBarButton scrollBarButtonDown;
		private Resco.Controls.ScrollBar.ScrollBarThumb scrollBarVThumb;
		private Resco.Controls.ScrollBar.LetterBar letterBar;
		private Resco.Controls.ScrollBar.HScrollBar hScrollBar1;
		private Resco.Controls.ScrollBar.ScrollBarButton scrollBarButtonLeft;
		private Resco.Controls.ScrollBar.ScrollBarButton scrollBarButtonRight;
		private Resco.Controls.ScrollBar.ScrollBarThumb scrollBarHThumb;
		private System.Windows.Forms.Timer timer1;
	}
}

