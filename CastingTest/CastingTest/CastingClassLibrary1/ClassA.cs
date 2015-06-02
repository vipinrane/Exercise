using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastingClassLibrary1
{
    public class ClassA
    {
        public string CreateEvent(ModelClass obj)
        {
            string result=GetMessage(obj);
            return result;
        }

        private string GetMessage(ModelClass obj)
        {
            string str = obj.Messgae + "TEST";
            return str;
        }
    }
}
