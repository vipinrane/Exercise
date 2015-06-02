namespace SortOnHeaderClick
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Resco.Controls.SmartGrid.Column column15 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column16 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column17 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column18 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column19 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column20 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column21 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Style style3 = new Resco.Controls.SmartGrid.Style();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.smartGrid1 = new Resco.Controls.SmartGrid.SmartGrid();
            this.SuspendLayout();
            this.imageList1.Images.Clear();
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.imageList1.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            // 
            // smartGrid1
            // 
            this.smartGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            column15.CustomizeCells = true;
            column15.DataMember = "Name";
            column15.HeaderText = "Name";
            column15.ImageList = this.imageList1;
            column16.CustomizeCells = true;
            column16.DataMember = "City";
            column16.HeaderText = "City";
            column16.ImageList = this.imageList1;
            column17.CustomizeCells = true;
            column17.DataMember = "Country";
            column17.HeaderText = "Country";
            column17.ImageList = this.imageList1;
            column18.CustomizeCells = true;
            column18.DataMember = "OrderDate";
            column18.HeaderText = "OrderDate";
            column18.ImageList = this.imageList1;
            column19.CustomizeCells = true;
            column19.DataMember = "ShippedDate";
            column19.HeaderText = "ShippedDate";
            column19.ImageList = this.imageList1;
            column19.Width = 100;
            column20.CustomizeCells = true;
            column20.DataMember = "ShipCity";
            column20.HeaderText = "ShipCity";
            column20.ImageList = this.imageList1;
            column21.CustomizeCells = true;
            column21.DataMember = "ShipCountry";
            column21.HeaderText = "ShipCountry";
            column21.ImageList = this.imageList1;
            column21.Width = 100;
            this.smartGrid1.Columns.Add(column15);
            this.smartGrid1.Columns.Add(column16);
            this.smartGrid1.Columns.Add(column17);
            this.smartGrid1.Columns.Add(column18);
            this.smartGrid1.Columns.Add(column19);
            this.smartGrid1.Columns.Add(column20);
            this.smartGrid1.Columns.Add(column21);
            this.smartGrid1.Location = new System.Drawing.Point(4, 4);
            this.smartGrid1.Name = "smartGrid1";
            this.smartGrid1.Size = new System.Drawing.Size(233, 161);
            style3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            style3.ImageAlignment = Resco.Controls.SmartGrid.Alignment.MiddleRight;
            style3.ImagePosition = Resco.Controls.SmartGrid.ImagePosition.Background;
            style3.Name = "Header";
            this.smartGrid1.Styles.Add(style3);
            this.smartGrid1.TabIndex = 0;
            this.smartGrid1.CustomizeCell += new Resco.Controls.SmartGrid.CustomizeCellHandler(this.smartGrid1_CustomizeCell);
            this.smartGrid1.HeaderClick += new Resco.Controls.SmartGrid.CellClickHandler(this.smartGrid1_HeaderClick);
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
            this.ResumeLayout(false);

        }

        #endregion

        private Resco.Controls.SmartGrid.SmartGrid smartGrid1;
        private System.Windows.Forms.ImageList imageList1;
    }
}

