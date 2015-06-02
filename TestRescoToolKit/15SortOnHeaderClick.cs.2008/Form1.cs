/*
 * This sample demonstrates how SmartGrid can be designed to sort the rows when a HeaderClick event is raised.
 * SmartGrid does not sort the rows really. It rather constructs a new SQL command and queries the server to get
 * the sorted data.
 */ 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Data.SqlServerCe;
using Resco.Controls.SmartGrid;

namespace SortOnHeaderClick
{
    public partial class Form1 : Form
    {
        string m_sPath;
        SqlCeConnection m_conn;
        SqlCeCommand m_cmd;
        // command that will be used to query the SQL CE server
        string m_sCommand = "SELECT customers.ContactName AS Name, customers.City AS City, customers.Country AS Country, orders.OrderDate, orders.ShippedDate, orders.ShipCity, orders.ShipCountry FROM customers JOIN orders ON customers.CustomerID = orders.CustomerID";
        bool m_bDesc;
        string m_sSortedColumn;

        public Form1()
        {
            InitializeComponent();

            // find out the path to this application
            m_sPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            // create a new SqlCeConnection
            m_conn = new SqlCeConnection("Data source = " + m_sPath + "\\Northwind.sdf");
            // Create a new command
            m_cmd = new SqlCeCommand(m_sCommand, m_conn);
            
            // assign the SQL CE command to SmartGrid's DbConnector
            this.smartGrid1.DbConnector.Command = m_cmd;
            // Load the data, but do not create the columns. Columns were created in the designer.
            this.smartGrid1.LoadData(false);
            // intialize a data member that says, whether we are sorting descending or not
            m_bDesc = false;
        }

        /// <summary>
        /// This method is called by SmartGrid, if header row is clicked.
        /// </summary>
        private void smartGrid1_HeaderClick(object sender, CellEventArgs e)
        {
            // find out the name of the column that was clicked
            string sHeaderText = e.Cell.Column.HeaderText;
            // start creating a new SQL command string
            string sCmd = m_sCommand;
            // append the ORDER BY clause
            sCmd += " ORDER BY ";
            // if the clicked column differs from the sorted one
            if (m_sSortedColumn != sHeaderText)
            {
                // remember the sorted column
                m_sSortedColumn = sHeaderText;
                // and append the name of the column that will be sorted to the new SQL command
                switch (m_sSortedColumn)
                {
                    case "Name":
                        sCmd += "Name";
                        break;
                    case "City":
                        sCmd += "City";
                        break;
                    case "Country":
                        sCmd += "Country";
                        break;
                    case "OrderDate":
                        sCmd += "OrderDate";
                        break;
                    case "ShippedDate":
                        sCmd += "ShippedDate";
                        break;
                    case "ShipCity":
                        sCmd += "ShipCity";
                        break;
                    case "ShipCountry":
                        sCmd += "ShipCountry";
                        break;
                }
                // we will sort the column ascending
                m_bDesc = false;
            }
            else
            {
                // if the same column was clicked as the sorted one
                // then toggle ascending to descending
                m_bDesc = !m_bDesc;
                // append the name of the column to the SQL command
                if (m_bDesc)
                    sCmd += m_sSortedColumn + " DESC";
                else
                    sCmd += m_sSortedColumn;
            }
            // delete all the rows
            this.smartGrid1.Rows.Clear();
            // create a new SqlCeCommand based on the new SQL command string
            this.smartGrid1.DbConnector.Command = new SqlCeCommand(sCmd, m_conn);
            // load data
            this.smartGrid1.LoadData(false);
        }

        private void smartGrid1_CustomizeCell(object sender, CustomizeCellEventArgs e)
        {
            // if we are processing a header cell
            if (e.Cell.Header)
            {
                // if we are processing the header cell in the sorted column
                if (e.Cell.Column.HeaderText == m_sSortedColumn)
                {
                    // apply the "Header" style to this cell
                    e.Cell.Style = this.smartGrid1.Styles["Header"];
                    // choose an arrow image that will be displayed in the right corner of the header cell
                    // depending on whether the column is sorted ascending or descending
                    if (m_bDesc)
                        e.Cell.Image = this.imageList1.Images[1];
                    else
                        e.Cell.Image = this.imageList1.Images[0];
                }
            }
        }
    }
}