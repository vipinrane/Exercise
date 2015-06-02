/*
 * This sample shows how SmartGrid can be designed to sort the data when a HeaderClick event occurs. In this sample
 * an ArrayList is used as a datasource which supports sorting by calling its Sort() method.
 */ 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace SortOnHeaderClick
{
    public partial class Form2 : Form
    {
        // create a new ArrayList that will be used as a datasource
        ArrayList al = new ArrayList();
        // create some names
        string[] names = new string[] { "John", "Kevin", "Jane", "Jeniffer", "Susan" };
        // ages corresponding to the names
        int[] ages = new int[] { 20, 25, 30, 35, 40 };

        public Form2()
        {
            InitializeComponent();

            for (int i = 0; i < names.Length; i++)
            {
                Data d = new Data(names[i], ages[i]);
                al.Add(d);
            }

            this.smartGrid1.DataSource = al;
        }

        private void smartGrid1_HeaderClick(object sender, Resco.Controls.SmartGrid.CellEventArgs e)
        {
             // name was clicked
            if (e.Cell.ColumnIndex == 0)
            {
                Data.CompareBy = 2;
                al.Sort();
                this.smartGrid1.DataSource = al;
            }
            if (e.Cell.ColumnIndex == 1)
            {
                Data.CompareBy = 1;
                al.Sort();
                this.smartGrid1.DataSource = al;
            }
        }
    }

    /// <summary>
    /// This class holds a name of a person and his/her age.
    /// </summary>
    public class Data : IComparable
    {
        string m_sName;
        int m_nAge;

        /// <summary>
        /// This static property determines whether two Data objects will be compared based on the Age or the Name.
        /// If its value is 1 then comparison is based on the age.
        /// If its value is 2 then comparison is based on the name.
        /// If the value is neither 1 nor 2, all the objects are considered equal.
        /// </summary>
        public static int CompareBy = 1;


        /// <summary>
        /// Constructs a new Data object with specified name and age.
        /// </summary>
        /// <param name="sName">Specifies the name.</param>
        /// <param name="nAge">Specifies the age.</param>
        public Data(string sName, int nAge)
        {
            m_sName = sName;
            m_nAge = nAge;
        }

        /// <summary>
        /// Gets or sets the age of this Data object.
        /// </summary>
        public int Age
        {
            get
            {
                return m_nAge;
            }
            set
            {
                m_nAge = value;
            }
        }

        /// <summary>
        /// Gets or sets the Name of this Data object.
        /// </summary>
        public string Name
        {
            get
            {
                return m_sName;
            }
            set
            {
                m_sName = value;
            }
        }

        #region IComparable Members

        /// <summary>
        /// Implements the IComparable interface.
        /// </summary>
        public int CompareTo(object obj)
        {
            Data d = obj as Data;
            // compare by age
            if (CompareBy == 1)
            {
                return m_nAge.CompareTo(d.Age);
            }
            // compare by name
            if (CompareBy == 2)
            {
                return m_sName.CompareTo(d.Name);
            }
            return 0;
        }

        #endregion
    }
}