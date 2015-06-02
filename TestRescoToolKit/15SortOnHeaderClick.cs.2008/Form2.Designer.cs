namespace SortOnHeaderClick
{
    partial class Form2
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.smartGrid1 = new Resco.Controls.SmartGrid.SmartGrid();
            this.SuspendLayout();
            // 
            // smartGrid1
            // 
            this.smartGrid1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            column1.DataMember = "Name";
            column1.HeaderText = "Name";
            column2.DataMember = "Age";
            column2.HeaderText = "Age";
            this.smartGrid1.Columns.Add(column1);
            this.smartGrid1.Columns.Add(column2);
            this.smartGrid1.Location = new System.Drawing.Point(4, 4);
            this.smartGrid1.Name = "smartGrid1";
            this.smartGrid1.Size = new System.Drawing.Size(233, 195);
            this.smartGrid1.TabIndex = 0;
            this.smartGrid1.HeaderClick += new Resco.Controls.SmartGrid.CellClickHandler(this.smartGrid1_HeaderClick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.smartGrid1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private Resco.Controls.SmartGrid.SmartGrid smartGrid1;
    }
}