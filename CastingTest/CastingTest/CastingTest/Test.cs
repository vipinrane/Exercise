using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace CastingTest
{

    public class Test
    {

        private class A
        {
            private readonly int _value;

            public A(int value)
            {
                _value = value;
            }

            public int Value { get { return _value; } }
        }

        private class B
        {
            private readonly int _value;

            private B(int value)
            {
                _value = value;
            }

            public int Value { get { return _value; } }

            public static explicit operator B(A value)
            {
                return new B(value.Value);
            }
        }
        private static B ConvertFromObject(object a)
        {
            if (a == null) return null;
            var p = Expression.Parameter(typeof(object));
            var c1 = Expression.Convert(p, a.GetType());
            var c2 = Expression.Convert(c1, typeof(B));
            var e = (Func<object, B>)Expression.Lambda(c2, p).Compile();
            return e(a);
        }

        public static void Main()
        {
            object a = new A(5);
            var b = ConvertFromObject(a);
            Console.WriteLine("{0}", b.Value);
            Console.ReadLine();
        }
    }

}
