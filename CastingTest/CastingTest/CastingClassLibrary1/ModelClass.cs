using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastingClassLibrary1
{
    public class ModelClass
    {
        string message = string.Empty;
        bool flag = false;

        public string Messgae
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
        public bool Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }
    }
}
