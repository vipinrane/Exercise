using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;

namespace TestEventService
{
    public class TestEvent
    {
        public delegate void DelegateSayHello();
        public delegate int DelegateCount();

        [EventPublication(TestEventNames.SayHello)]
        public event DelegateSayHello SayHello;

        [EventPublication(TestEventNames.GetCount)]
        public event DelegateCount GetCount;

        public void RaiseSayHello()
        {
            if (this.SayHello != null) this.SayHello();
        }

        public int RaiseGetCount()
        {
            int count = 0;
            if (this.GetCount != null)
                count = this.GetCount();
            return count;
        }

    }

    public static class TestEventNames
    {
        public const string SayHello = "SayHello";
        public const string GetCount = "GetCount";
    }
}
