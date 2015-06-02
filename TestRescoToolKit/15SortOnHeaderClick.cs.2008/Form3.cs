/*
 * This sample shows how SmartGrid can be designed to sort the rows when HeaderClick event is fired. SmartGrid uses
 * a DataView of a DataTable as a datasource. SmartGrid does not sort the rows really, but uses the Sort property
 * of the DataView object to sort the rows.
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
    public partial class Form3 : Form
    {
        string m_sPath;
        SqlCeConnection m_conn;
        SqlCeCommand m_cmd;
        // command that will be used to query the SQL CE server
        string m_sCommand = "SELECT customers.ContactName AS Name, customers.City AS City, customers.Country AS Country, orders.OrderDate, orders.ShippedDate, orders.ShipCity, orders.ShipCountry FROM customers JOIN orders ON customers.CustomerID = orders.CustomerID";
        DataTable m_dt;
        DataView m_dv;

        public Form3()
        {
            InitializeComponent();

            // find out the path to this application
            m_sPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            // create a new SqlCeConnection
            m_conn = new SqlCeConnection("Data source = " + m_sPath + "\\Northwind.sdf");
            // Create a new command
            m_cmd = new SqlCeCommand(m_sCommand, m_conn);
            // Create a new SqlCe data adapter
            SqlCeDataAdapter da = new SqlCeDataAdapter(m_cmd);
            // create a new DataTable
            m_dt = new DataTable();
            // fill the DataTable object with data
            da.Fill(m_dt);
            // Create a default DataView of the data in DataTable
            m_dv = m_dt.DefaultView;
            // use the DataView as a datasource for SmartGrid
            this.smartGrid1.DataSource = m_dv;
        }

        private void smartGrid1_HeaderClick(object sender, CellEventArgs e)
        {
            // if the header was clicked, sort the data in the DataView object.
            m_dv.Sort = e.Cell.Column.DataMember;
        }
    }
}