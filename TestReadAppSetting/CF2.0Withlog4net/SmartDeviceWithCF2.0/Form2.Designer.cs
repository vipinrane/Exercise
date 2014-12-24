namespace SmartDeviceWithCF2._0
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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.dytrexLogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dytrexLoggingDBDataSet = new SmartDeviceWithCF2._0.DytrexLoggingDBDataSet();
            this.dytrexLogTableAdapter = new SmartDeviceWithCF2._0.DytrexLoggingDBDataSetTableAdapters.DytrexLogTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dytrexLogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dytrexLoggingDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dytrexLogBindingSource
            // 
            this.dytrexLogBindingSource.DataMember = "DytrexLog";
            this.dytrexLogBindingSource.DataSource = this.dytrexLoggingDBDataSet;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.DataSource = this.dytrexLogBindingSource;
            this.dataGrid1.Location = new System.Drawing.Point(15, 26);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(198, 124);
            this.dataGrid1.TabIndex = 0;
            // 
            // dytrexLoggingDBDataSet
            // 
            this.dytrexLoggingDBDataSet.DataSetName = "DytrexLoggingDBDataSet";
            this.dytrexLoggingDBDataSet.Prefix = "";
            this.dytrexLoggingDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dytrexLogTableAdapter
            // 
            this.dytrexLogTableAdapter.ClearBeforeFill = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.dataGrid1);
            this.Menu = this.mainMenu1;
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dytrexLogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dytrexLoggingDBDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private DytrexLoggingDBDataSet dytrexLoggingDBDataSet;
        private System.Windows.Forms.BindingSource dytrexLogBindingSource;
        private SmartDeviceWithCF2._0.DytrexLoggingDBDataSetTableAdapters.DytrexLogTableAdapter dytrexLogTableAdapter;
    }
}