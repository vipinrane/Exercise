namespace SortOnHeaderClick
{
    partial class Form3
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
            Resco.Controls.SmartGrid.Column column1 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column2 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column3 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column4 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column5 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column6 = new Resco.Controls.SmartGrid.Column();
            Resco.Controls.SmartGrid.Column column7 = new Resco.Controls.SmartGrid.Column();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.smartGrid1 = new Resco.Controls.SmartGrid.SmartGrid();
            this.SuspendLayout();
            // 
            // smartGrid1
            // 
            this.smartGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            column1.CustomizeCells = true;
            column1.DataMember = "Name";
            column1.HeaderText = "Name";
            column2.CustomizeCells = true;
            column2.DataMember = "City";
            column2.HeaderText = "City";
            column3.CustomizeCells = true;
            column3.DataMember = "Country";
            column3.HeaderText = "Country";
            column4.CustomizeCells = true;
            column4.DataMember = "OrderDate";
            column4.HeaderText = "OrderDate";
            column5.CustomizeCells = true;
            column5.DataMember = "ShippedDate";
            column5.HeaderText = "ShippedDate";
            column5.Width = 100;
            column6.CustomizeCells = true;
            column6.DataMember = "ShipCity";
            column6.HeaderText = "ShipCity";
            column7.CustomizeCells = true;
            column7.DataMember = "ShipCountry";
            column7.HeaderText = "ShipCountry";
            column7.Width = 100;
            this.smartGrid1.Columns.Add(column1);
            this.smartGrid1.Columns.Add(column2);
            this.smartGrid1.Columns.Add(column3);
            this.smartGrid1.Columns.Add(column4);
            this.smartGrid1.Columns.Add(column5);
            this.smartGrid1.Columns.Add(column6);
            this.smartGrid1.Columns.Add(column7);
            this.smartGrid1.Location = new System.Drawing.Point(4, 4);
            this.smartGrid1.Name = "smartGrid1";
            this.smartGrid1.Size = new System.Drawing.Size(233, 161);
            this.smartGrid1.TabIndex = 0;
            this.smartGrid1.HeaderClick += new Resco.Controls.SmartGrid.CellClickHandler(this.smartGrid1_HeaderClick);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.smartGrid1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        #endregion

        private Resco.Controls.SmartGrid.SmartGrid smartGrid1;
    }
}