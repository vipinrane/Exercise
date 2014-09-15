using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAbstract.TInterface;

namespace TestAbstract.TImplementor
{
    class TImplementor : UIAnimationHandler
    {
        public string TestMessage()
        {
            return "Implementor message";
        }
    }
}
