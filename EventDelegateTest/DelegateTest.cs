using System;

namespace EventDelegateTest
{
    public delegate string FirstDelegate (int x);
    public delegate string GreetingMessageHandler(string userName);
    class DelegateTest
    {
        string name;
        string userName;
        static void Main(string[] args)
        {
            FirstDelegate d1 = new FirstDelegate(DelegateTest.StaticMethod);

            DelegateTest instance = new DelegateTest();
            instance.name = "My instance";
            FirstDelegate d2 = new FirstDelegate(instance.InstanceMethod);

            Console.WriteLine(d1(10)); // Writes out "Static method: 10"
            Console.WriteLine(d2(5));  // Writes out "My instance: 5"

            Console.Write("Your Name:");
            instance.userName= Console.ReadLine();
            GreetingMessageHandler gMsg=new GreetingMessageHandler(instance.GetMessage);

            Console.WriteLine(gMsg(instance.userName));
            Console.ReadLine();
 
        }

        static string StaticMethod(int i)
        {
            return string.Format("Static method: {0}", i);
        }

        string InstanceMethod(int i)
        {
            return string.Format("{0}: {1}", name, i);
        }
        string GetMessage(string msg)
        {
            return string.Format("Welcome {0}", msg);
        }
    }
}
