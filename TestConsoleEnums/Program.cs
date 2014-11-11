using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsoleEnums
{
    class Program
    {
        static void Main(string[] args)
        {
            Test3ReadValueAndGetEnumString();
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
            Test4ReadEnumStringAndGetEnumValue();
        }

        public enum EnumDisplayStatus
        {
            None = 1,
            Visible = 2,
            Hidden = 3,
            MarkedForDeletion = 4
        }

        public static void Test3ReadValueAndGetEnumString()
        {
            try
            {
                //int value = 2;
                Console.WriteLine("Enter the integer constant from (1-None,2-Visible,3-Hidden,4-MarkedForDeletion):");
                int value = Convert.ToInt32(Console.ReadLine());
                EnumDisplayStatus enumDisplayStatus = ((EnumDisplayStatus)value);
                string stringValue = enumDisplayStatus.ToString();

                Console.WriteLine("Enum Constant Name = {0}", stringValue);

                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Test4ReadEnumStringAndGetEnumValue()
        {
            try
            {
                //int value = 2;
                Console.WriteLine("Enter the string constant from (1-None,2-Visible,3-Hidden,4-MarkedForDeletion):");
                string enumConstantString = Console.ReadLine();
                var number = (int)((EnumDisplayStatus)Enum.Parse(typeof(EnumDisplayStatus), enumConstantString));
                Console.WriteLine("Enum Constant Value = {0}", number);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}