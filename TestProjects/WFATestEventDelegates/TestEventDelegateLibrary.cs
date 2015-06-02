using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFATestEventDelegates
{
    public class TestEventDelegateLibrary
    {
        public bool ConsumeSomeTime()
        {
            System.Threading.Thread.Sleep(5000);

            return true;
        }
    }
}
