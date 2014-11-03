using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace TestReadXML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetConfirmationMode_Click(object sender, EventArgs e)
        {
            ReadConfigPropertiesFromXML();
            //TestReadXML();
        }

        public void ReadConfigPropertiesFromXML()
        {
            try
            {
                StringBuilder result = new StringBuilder();
                var fileName = "\\XMLFile1.xml";
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + fileName;
                var document = XDocument.Load(path);
                Dictionary<string, string> parametersDict = new Dictionary<string, string>();
                var DataConfirmationMode = document.Root.Descendants("ScannerP");
                //.Select(arg => arg.Attribute("DataConfirmationMode"))
                //.Where(arg => arg != null)
                //.Select(arg => arg.Value)
                //.ToList();

                foreach (var lv in DataConfirmationMode)
                {
                    var paramNodes = document.Root.Descendants("param");
                    foreach (var item in paramNodes)
                    {
                        result.AppendLine(item.Value);
                        var attrib=item.Attributes().ToList();
                        parametersDict.Add(attrib[0].Value, attrib[1].Value);
                    }
                }

                textBox1.Text = result.ToString();
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.ToString();
            }
        }

        public void TestReadXML()
        {
            try
            {
                StringBuilder result = new StringBuilder(); //needed for result below
                var xdoc = XDocument.Load(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\\data.xml"); //you'll have to edit your path
                var lv1s = xdoc.Root.Descendants("level1");
                var lvs = lv1s.SelectMany(l =>
                     new string[] { l.Attribute("name").Value }
                     .Union(
                         l.Descendants("level2")
                         .Select(l2 => "   " + l2.Attribute("name").Value)
                      )
                    );
                foreach (var lv in lvs)
                {
                    result.AppendLine(lv);
                }
                textBox1.Text = result.ToString();//added this so you could see the result
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}