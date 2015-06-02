using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGCustomScrollBar
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			vScrollBar1.ExtensionVisible = false;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				Resco.Controls.SmartGrid.SmartGrid.AutoColumns = true;

				// load the data from XML file:
				string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
				DataSet ds = new DataSet("DSCustomers");
				ds.ReadXml(path + "\\DSCustomers.xml");
				ds.AcceptChanges();

				// set up the data source:
				BindingSource bs = new BindingSource(ds.Tables[0], null);
				smartGrid1.DataSource = bs;

			}
			catch(Exception ex)
			{
				MessageBox.Show("Unable to load data: " + ex.Message, "Error");
			}
		}

		// update SmartGrid position by the selected letter
		private int letterBar_IndexToValue(object sender, Resco.Controls.ScrollBar.ScrollBarExtensionBase.ValueIndexConversionEventArgs e)
		{
			// get datasource, return if not found
			BindingSource bs = smartGrid1.DataSource as BindingSource;
			if (bs == null)
				return -1;

			// get DataTable
			DataTable dt = bs.DataSource as DataTable;
			if (dt == null)
				return -1;

			// find all rows which starts with the selected letter
			DataRow[] rows = dt.Select("CustomerID LIKE '" + letterBar.IndexToLetter(e.Parameter) + "%'", "CustomerID");
			if (rows.Length > 0)
			{
				// get the position of the first row
				int position = bs.Find("CustomerID", Convert.ToString(rows[0]["CustomerID"]));

				// uncomment this line to select the row
				//smartGrid1.ActiveRowIndex = position;

				// return position of the row
				return position;
			}

			// return -1 to not update ScrollBar.Value
			return -1;
		}

		private int letterBar_ValueToIndex(object sender, Resco.Controls.ScrollBar.ScrollBarExtensionBase.ValueIndexConversionEventArgs e)
		{
			// get the row at the ScrollBar value
			Resco.Controls.SmartGrid.Row row = smartGrid1.Rows[e.Parameter];
			if (row == null)
				return -1;

			string text = Convert.ToString(row["CustomerID"]);
			if (text.Length > 0)
			{
				return letterBar.LetterToIndex(text[0]);
			}

			return -1;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Enabled = false;
			vScrollBar1.ExtensionVisible = false;
		}

		private void smartGrid1_MouseDown(object sender, MouseEventArgs e)
		{
			timer1.Enabled = false;
			vScrollBar1.ExtensionVisible = true;
		}

		private void smartGrid1_MouseUp(object sender, MouseEventArgs e)
		{
			timer1.Enabled = true;
		}
	}
}