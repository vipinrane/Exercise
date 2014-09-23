using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestStruct
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        // ... Create struct on stack.
    //        Simple s;
    //        s.Position = 1;
    //        s.Exists = false;
    //        s.LastValue = 5.5;

    //        // ... Write struct field.
    //        Console.WriteLine(s.Position);
    //        Console.ReadLine();
    //    }

    //    struct Simple
    //    {
    //        public int Position;
    //        public bool Exists;
    //        public double LastValue;
    //    };
    //}

    class TheClass
    {
        public int x;
    }

    struct TheStruct
    {
        public int x;
    }

    class TestClass
    {
        public static void structtaker(TheStruct s)
        {
            s.x = 5;
        }
        public static void classtaker(TheClass c)
        {
            c.x = 5;
        }
        public static void Main()
        {
            TheStruct a = new TheStruct();
            TheClass b = new TheClass();
            a.x = 1;
            b.x = 1;
            structtaker(a);
            classtaker(b);
            Console.WriteLine("a.x = {0}", a.x);
            Console.WriteLine("b.x = {0}", b.x);
            Console.ReadLine();
        }
    }
}