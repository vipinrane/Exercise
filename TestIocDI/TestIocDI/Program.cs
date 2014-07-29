using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIocDI
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer objCustomer = new Customer(new MSAccessABC());
            objCustomer.CustomerAdd();
            Console.ReadLine();
        }
    }


    class Customer
    {
        private IDal objDal;// = new SqlServer();
        public Customer(IDal obj)
        {
            objDal = obj;
        }

        public void CustomerAdd()
        {
            objDal.Add();
        }
    }

    interface IDal
    {
       void Add();
    }

    class SqlServer:IDal
    {

        public void Add()
        {
            Console.WriteLine("SqlServer: Added.");
        }
    }
    class OracleServer : IDal
    {
        public void Add()
        {
            Console.WriteLine("OracleServer: Added.");
        }
    }

    class MSAccessABC 
    {
        public void Add()
        {
            Console.WriteLine("OracleServer: Added.");
        }
    }
}
