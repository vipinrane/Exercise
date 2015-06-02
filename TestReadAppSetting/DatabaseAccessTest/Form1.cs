using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace DatabaseAccessTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Test();
        }

        public void Test()
        {
            SqlCeConnection conn = new SqlCeConnection(@"Data Source=\Program Files\DatabaseAccessTest\testDatabase.sdf;");
            try
            {
                conn.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter("INSERT INTO TUser(Name) VALUES('Vipin')", conn);
                DataSet ds=new DataSet();
                da.Fill(ds);
                label1.Text = "Connection!";
            }
            catch (SqlCeException ee)  // <- Notice the use of SqlCeException to read your errors
            {
                SqlCeErrorCollection errorCollection = ee.Errors;

                StringBuilder bld = new StringBuilder();
                Exception inner = ee.InnerException;

                if (null != inner)
                {
                    MessageBox.Show("Inner Exception: " + inner.ToString());
                }
                // Enumerate the errors to a message box.
                foreach (SqlCeError err in errorCollection)
                {
                    bld.Append("\n Error Code: " + err.HResult.ToString("X"));
                    bld.Append("\n Message   : " + err.Message);
                    bld.Append("\n Minor Err.: " + err.NativeError);
                    bld.Append("\n Source    : " + err.Source);

                    // Enumerate each numeric parameter for the error.
                    foreach (int numPar in err.NumericErrorParameters)
                    {
                        if (0 != numPar) bld.Append("\n Num. Par. : " + numPar);
                    }

                    // Enumerate each string parameter for the error.
                    foreach (string errPar in err.ErrorParameters)
                    {
                        if (String.Empty != errPar) bld.Append("\n Err. Par. : " + errPar);
                    }

                }
                label1.Text = bld.ToString();
                bld.Remove(0, bld.Length);
            }
        }

    }
}