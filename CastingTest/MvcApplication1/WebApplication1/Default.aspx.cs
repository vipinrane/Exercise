/*
 * 
 * 
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    /// <summary>
    /// Default class
    /// </summary>
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TestComment(10,"AAA");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Firstname"></param>
        public void TestA(string Firstname)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public int TestA(int age)
        {
            return 10;//M-1
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="age"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        ///<Modificiations>
        ///     <ModificationNo>
        ///         Date: Date <BR/>
        ///		    Reason: Reason <BR/>
        ///		    Modified by: Author Name<BR/>
        ///     </ModificationNo>
        ///</Modificiations>
        ///<ModificiationsA>
        ///</ModificiationsA>
        public int TestComment(int age, string name)
        {
            string process = name;
            int total = age;

            return 0;
        }
    }
}