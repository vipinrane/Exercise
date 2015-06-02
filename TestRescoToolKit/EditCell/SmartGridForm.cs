/**********************************************************************************\
 **
 **		This application is used to demonstrate the capabilities of 
 **		the Resco SmartGrid control. 
 **		Using the CustomizeCell event you can set up specific display for columns
 **		that require that.
 **
\**********************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

using Resco.Controls.SmartGrid;
using System.Resources;


namespace EditCell
{
	public class SmartGridForm : System.Windows.Forms.Form
	{
		Resco.Controls.SmartGrid.SmartGrid SmartGrid1;
		System.Windows.Forms.MainMenu MainMenu1;
		System.Windows.Forms.ImageList ImageList1;

		// images cached from ImageList
		Image imgUp;
        private Column column1;
        private Column column2;
        private Column column3;
        private Style Text;
        private Style Currency;
        private Style Number_blue;
        private Style Number_red;
        private MenuItem menuItem1;
        private Resco.Controls.CommonControls.ToolbarControl toolbarControl2;
        private Resco.Controls.CommonControls.ToolbarItem toolbarItem4;
        private Resco.Controls.CommonControls.ToolbarItem toolbarItem5;
        private Resco.Controls.KeyboardPro.KeyboardPro keyboardPro1;
        private System.ComponentModel.IContainer components;
        private Resco.Controls.OutlookControls.ImageButton imageButton2;
		Image imgDown;

		public SmartGridForm()
		{
			InitializeComponent();
            using (Graphics graphics = base.CreateGraphics())
            {
                //ScreenDpi = new Size((int)graphics.DpiX, (int)graphics.DpiY);
                m_scaleFactor.Width = graphics.DpiX / 96F;
                m_scaleFactor.Height = graphics.DpiY / 96F;
            }
            // 
            // m_appTitle
            // 
            TitleControl m_appTitle = new TitleControl();
            m_appTitle.Dock = System.Windows.Forms.DockStyle.Top;
            m_appTitle.Location = new Point(0, 0);
            if (this.AutoScaleDimensions == new SizeF(96, 96))
                m_appTitle.Size = new Size(240, 10);
            else
                m_appTitle.Size = new Size(240, 20);
            m_appTitle.TabIndex = 20;
            m_appTitle.Font = new Font("Tahoma", 8F, FontStyle.Bold); ;
            m_appTitle.ForeColor = Color.White;
            m_appTitle.BackColor = this.BackColor;
            m_appTitle.Visible = true;
            this.Controls.Add(m_appTitle);

			// cache up images from imagelist:
			imgUp = ImageList1.Images[0];
			imgDown = ImageList1.Images[1];

            SmartGrid1.EditingTextBox.GotFocus += new EventHandler(EditingTextBox_GotFocus);
            SmartGrid1.EditingTextBox.LostFocus += new EventHandler(EditingTextBox_LostFocus); ;
		}

        void EditingTextBox_LostFocus(object sender, EventArgs e)
        {
            keyboardPro1.Visible = false;
        }

        void EditingTextBox_GotFocus(object sender, EventArgs e)
        {
            keyboardPro1.Visible = true;
        }

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmartGridForm));
            Resco.Controls.OutlookControls.ImageMargin imageMargin3 = new Resco.Controls.OutlookControls.ImageMargin(0, 0, 0, 0);
            this.column1 = new Resco.Controls.SmartGrid.Column();
            this.column2 = new Resco.Controls.SmartGrid.Column();
            this.column3 = new Resco.Controls.SmartGrid.Column();
            this.ImageList1 = new System.Windows.Forms.ImageList();
            this.Text = new Resco.Controls.SmartGrid.Style();
            this.Currency = new Resco.Controls.SmartGrid.Style();
            this.Number_blue = new Resco.Controls.SmartGrid.Style();
            this.Number_red = new Resco.Controls.SmartGrid.Style();
            this.MainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.SmartGrid1 = new Resco.Controls.SmartGrid.SmartGrid();
            this.toolbarControl2 = new Resco.Controls.CommonControls.ToolbarControl();
            this.toolbarItem4 = new Resco.Controls.CommonControls.ToolbarItem();
            this.toolbarItem5 = new Resco.Controls.CommonControls.ToolbarItem();
            this.keyboardPro1 = new Resco.Controls.KeyboardPro.KeyboardPro(this, this.components);
            this.imageButton2 = new Resco.Controls.OutlookControls.ImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageButton2)).BeginInit();
            this.SuspendLayout();
            // 
            // column1
            // 
            this.column1.AlternatingStyle = "Text";
            this.column1.CellEdit = Resco.Controls.SmartGrid.CellEditType.TextBox;
            this.column1.DataMember = "ProductName";
            this.column1.EditMode = Resco.Controls.SmartGrid.EditMode.Auto;
            this.column1.HeaderText = "Product";
            this.column1.ImageMember = "Discounted";
            this.column1.MinimumWidth = 10;
            this.column1.Name = "column1";
            this.column1.SelectionStyle = "Text";
            this.column1.Style = "Text";
            this.column1.Width = 212;
            // 
            // column2
            // 
            this.column2.AlternatingStyle = "Currency";
            this.column2.CustomizeCells = true;
            this.column2.DataMember = "Sales";
            this.column2.HeaderText = "Sales";
            this.column2.MinimumWidth = 10;
            this.column2.Name = "column2";
            this.column2.SelectionStyle = "Currency";
            this.column2.Style = "Currency";
            this.column2.Width = 112;
            // 
            // column3
            // 
            this.column3.Alignment = Resco.Controls.SmartGrid.Alignment.TopRight;
            this.column3.CustomizeCells = true;
            this.column3.DataMember = "Change";
            this.column3.FillWeight = 110F;
            this.column3.HeaderText = "Change";
            this.column3.ImageList = this.ImageList1;
            this.column3.MinimumWidth = 10;
            this.column3.Name = "column3";
            this.column3.Width = 110;
            this.ImageList1.Images.Clear();
            this.ImageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.ImageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            // 
            // Text
            // 
            this.Text.Name = "Text";
            // 
            // Currency
            // 
            this.Currency.AutoTransparent = true;
            this.Currency.ImagePosition = Resco.Controls.SmartGrid.ImagePosition.Left;
            this.Currency.Name = "Currency";
            this.Currency.TextAlignment = Resco.Controls.SmartGrid.Alignment.TopRight;
            // 
            // Number_blue
            // 
            this.Number_blue.AutoResize = true;
            this.Number_blue.AutoTransparent = true;
            this.Number_blue.ForeColor = System.Drawing.Color.Blue;
            this.Number_blue.FormatString = "{0}%";
            this.Number_blue.ImagePosition = Resco.Controls.SmartGrid.ImagePosition.Left;
            this.Number_blue.Name = "Number_blue";
            this.Number_blue.TextAlignment = Resco.Controls.SmartGrid.Alignment.TopRight;
            // 
            // Number_red
            // 
            this.Number_red.AutoResize = true;
            this.Number_red.AutoTransparent = true;
            this.Number_red.ForeColor = System.Drawing.Color.Red;
            this.Number_red.FormatString = "{0}%";
            this.Number_red.ImagePosition = Resco.Controls.SmartGrid.ImagePosition.Left;
            this.Number_red.Name = "Number_red";
            this.Number_red.TextAlignment = Resco.Controls.SmartGrid.Alignment.TopRight;
            // 
            // MainMenu1
            // 
            this.MainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Back";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // SmartGrid1
            // 
            this.SmartGrid1.AlternatingBackColor = System.Drawing.Color.PaleGoldenrod;
            this.SmartGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.SmartGrid1.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.SmartGrid1.ColumnHeaderHeight = 40;
            this.SmartGrid1.ColumnResizeSensitivity = 4;
            this.SmartGrid1.Columns.Add(this.column1);
            this.SmartGrid1.Columns.Add(this.column2);
            this.SmartGrid1.Columns.Add(this.column3);
            this.SmartGrid1.FullRowSelect = true;
            this.SmartGrid1.HeaderBackColor = System.Drawing.Color.Silver;
            this.SmartGrid1.Location = new System.Drawing.Point(2, 98);
            this.SmartGrid1.Name = "SmartGrid1";
            this.SmartGrid1.RowHeadersVisible = true;
            this.SmartGrid1.RowHeaderWidth = 24;
            this.SmartGrid1.Size = new System.Drawing.Size(476, 490);
            this.SmartGrid1.Styles.Add(this.Text);
            this.SmartGrid1.Styles.Add(this.Currency);
            this.SmartGrid1.Styles.Add(this.Number_blue);
            this.SmartGrid1.Styles.Add(this.Number_red);
            this.SmartGrid1.TabIndex = 0;
            this.SmartGrid1.TouchScrolling = true;
            this.SmartGrid1.CustomizeCell += new Resco.Controls.SmartGrid.CustomizeCellHandler(this.SmartGrid1_CustomizeCell);
            // 
            // toolbarControl2
            // 
            this.toolbarControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.toolbarControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolbarControl2.Items.Add(this.toolbarItem4);
            this.toolbarControl2.Items.Add(this.toolbarItem5);
            this.toolbarControl2.Location = new System.Drawing.Point(0, 590);
            this.toolbarControl2.Name = "toolbarControl2";
            this.toolbarControl2.Size = new System.Drawing.Size(480, 50);
            this.toolbarControl2.TabIndex = 22;
            this.toolbarControl2.ItemEntered += new System.EventHandler(this.toolbarControl2_ItemEntered_1);
            // 
            // toolbarItem4
            // 
            this.toolbarItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.toolbarItem4.CustomSize = new System.Drawing.Size(240, 40);
            this.toolbarItem4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.toolbarItem4.ForeColor = System.Drawing.Color.White;
            this.toolbarItem4.ItemSizeType = Resco.Controls.CommonControls.ToolbarItemSizeType.ByCustomSize;
            this.toolbarItem4.Name = "toolbarItem4";
            this.toolbarItem4.Text = "Close";
            this.toolbarItem4.ToolbarItemBehavior = Resco.Controls.CommonControls.ToolbarItemBehaviorType.UnselectAfterClick;
            // 
            // toolbarItem5
            // 
            this.toolbarItem5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.toolbarItem5.CustomSize = new System.Drawing.Size(240, 40);
            this.toolbarItem5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.toolbarItem5.ForeColor = System.Drawing.Color.White;
            this.toolbarItem5.ItemSizeType = Resco.Controls.CommonControls.ToolbarItemSizeType.ByCustomSize;
            this.toolbarItem5.Name = "toolbarItem5";
            this.toolbarItem5.Text = "Keyboard";
            this.toolbarItem5.ToolbarItemBehavior = Resco.Controls.CommonControls.ToolbarItemBehaviorType.UnselectAfterClick;
            // 
            // imageButton2
            // 
            this.imageButton2.AutoTransparent = false;
            this.imageButton2.ButtonStyle = Resco.Controls.OutlookControls.ImageButton.ButtonType.PictureBox;
            this.imageButton2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.imageButton2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.imageButton2.ImageDefault = global::EditCell.ImageManager1.GetImage("MainForm.TopBar");
            this.imageButton2.ImageLocation = new System.Drawing.Point(0, 0);
            this.imageButton2.ImageMargin = imageMargin3;
            this.imageButton2.Location = new System.Drawing.Point(0, 24);
            this.imageButton2.Name = "imageButton2";
            this.imageButton2.Size = new System.Drawing.Size(480, 74);
            this.imageButton2.TabIndex = 31;
            this.imageButton2.Text = "SmartGrid";
            // 
            // SmartGridForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(480, 640);
            this.Controls.Add(this.imageButton2);
            this.Controls.Add(this.toolbarControl2);
            this.Controls.Add(this.SmartGrid1);
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmartGridForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageButton2)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
        //static void Main()
        //{
        //    Application.Run(new Form4());
        //}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			// get current path
			string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			// load data from XML dataset
			DataSet myDataSet = new DataSet();
			myDataSet.ReadXml(path + "\\Sales.xml");
			this.SmartGrid1.DataSource = myDataSet.Tables[0];
		}

		private void SmartGrid1_CustomizeCell(object sender, Resco.Controls.SmartGrid.CustomizeCellEventArgs e)
		{
			// customize only non-header cells
			if (e.Cell.Header == false) 
			{
				if (e.Cell.Column.DataMember == "Change") 
				{
					// set up number style and image according to positive or negative change
					if (Convert.ToInt32(e.Cell.Data) > 0) 
					{
						e.Cell.Style = this.SmartGrid1.Styles["Number_blue"];
						e.Cell.Image = imgUp;
					} 
					else 
					{
						e.Cell.Style = this.SmartGrid1.Styles["Number_red"];
						e.Cell.Image = imgDown;
					}
				} 
				else if (e.Cell.Column.DataMember == "Sales") 
				{
					// perform custom text formating
					e.Cell.Text = "$" + 
						Microsoft.VisualBasic.Strings.FormatNumber(
							e.Cell.Data, 0, 
							Microsoft.VisualBasic.TriState.True, 
							Microsoft.VisualBasic.TriState.False, 
							Microsoft.VisualBasic.TriState.True);
				}
			}
		}

        private void menuItem1_Click(object sender, EventArgs e)
        {
                this.Close();
        }

        private void toolbarControl2_ItemEntered_1(object sender, EventArgs e)
        {
            if (toolbarControl2.SelectedIndex == 0)
                this.Close();
            if (toolbarControl2.SelectedIndex == 1)
                keyboardPro1.Visible = (keyboardPro1.Visible) ? false : true;
        }

        #region Title

        /// <summary>
        /// The current scale factor.
        /// </summary>
        public static SizeF m_scaleFactor = new SizeF(1, 1);

        /// <summary>
        /// Scales a coordinate by the current scale factor.
        /// </summary>
        /// <remarks>
        /// Assumes that the coordinate is in the standard QVGA pixels.
        /// Result is in the native resolution.
        /// </remarks>
        /// <param name="x">Coordinate to scale.</param>
        /// <returns>Scaled coordinate.</returns>
        public static int ScaleCoord(int x)
        {
            return (int)(x * m_scaleFactor.Width);
        }


        /// <summary>
        /// Scales a coordinate by the current scale factor.
        /// </summary>
        /// <remarks>
        /// Assumes that the coordinate is in the standard QVGA pixels.
        /// Result is in the native resolution.
        /// </remarks>
        /// <param name="x">Coordinate to scale.</param>
        /// <returns>Scaled coordinate.</returns>
        public static float ScaleCoord(float x)
        {
            return (x * m_scaleFactor.Width);
        }

        #endregion
	}
}