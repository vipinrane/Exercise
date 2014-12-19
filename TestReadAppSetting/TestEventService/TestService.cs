using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using System.Threading;

namespace TestEventService
{
    public class TestService
    {
        public int testId { get; set; }
        public string testName { get; set; }

        [EventSubscription(TestEventNames.GetWelcomeMessage, ThreadOption.Caller)]
        public string GetWelcomeMessage()
        {
            return "Welcome ABC";
        }

        //[EventSubscription(TestEventNames.SayHello, ThreadOption.Caller)]
        public void TestHello()
        {
            string str = "Inside TestHello Method";
        }

        [EventSubscription(TestEventNames.GetCount, ThreadOption.Caller)]
        public int TestGetCount()
        {
            return 10;
        }

    }
}
