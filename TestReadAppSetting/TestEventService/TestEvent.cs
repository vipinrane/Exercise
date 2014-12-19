using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;

namespace TestEventService
{
    public class TestEvent
    {
        public delegate string DelegateGetWelcomeMessage();
        public delegate void DelegateSayHello();
        public delegate int DelegateCount();
        

        [EventPublication(TestEventNames.GetWelcomeMessage)]
        public event DelegateGetWelcomeMessage GetWelcomeMessage;

        [EventPublication(TestEventNames.SayHello)]
        public event DelegateSayHello SayHello;

        [EventPublication(TestEventNames.GetCount)]
        public event DelegateCount GetCount;

        public string RaiseGetWelcomeMessage()
        {
            string messageString = string.Empty;
            if (this.GetWelcomeMessage != null)
                messageString = this.GetWelcomeMessage();
            return messageString;
        }

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
        public const string GetWelcomeMessage = "GetWelcomeMessage";
        public const string SayHello = "SayHello";
        public const string GetCount = "GetCount";
    }
}
