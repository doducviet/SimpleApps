using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpPlus;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Test : public static List<string> Split(this string strInput, string strSeperator, bool isRemoveEmptyEntries = true)
            //string strTest = "a,b,c,d,,e,f,,g";

            //Console.WriteLine("isRemoveEmptyEntries : TRUE");
            //foreach (string str in strTest.Split(","))
            //{
            //    Console.WriteLine(str);
            //}

            //Console.WriteLine("isRemoveEmptyEntries : FALSE");
            //foreach (string str in strTest.Split(",", false))
            //{
            //    Console.WriteLine(str);
            //}
            #endregion

            #region Test : public static void CopyValues(this object source, object target, Dictionary<string, string> mapper = null)
            A a = new A(1, "2", "3", "5", "7");
            B b1 = new B();
            B b2 = new B();

            Console.WriteLine("Old : " + b1);
            a.CopyTo(b1);
            Console.WriteLine("New : " + b1);

            a.Name1 = 11;
            a.Name2 = "22";
            a.Name3 = "33";
            a.Name5 = "55";
            a.Name7 = "77";
            Console.WriteLine("Old : " + b2);
            b2.CopyFrom(a);
            Console.WriteLine("New : " + b2);

            Console.ReadLine();
            #endregion
        }

        #region Test : public static void CopyValues(this object source, object target, Dictionary<string, string> mapper = null)
        class A
        {
            public int Name1 { get; set; }
            public string Name2 { get; set; }
            public string Name3 { get; set; }
            public string Name5 { get; set; }
            public string Name7 { get; set; }

            public A(int name1, string name2, string name3, string name5, string name7)
            {
                Name1 = name1;
                Name2 = name2;
                Name3 = name3;
                Name5 = name5;
                Name7 = name7;
            }

            public override string ToString()
            {
                return Name1 + " " + Name2 + " " + Name3 + " " + Name5 + " " + Name7;
            }
        }

        class B
        {
            public string Name1 { get; set; } 
            public string Name2 { get; set; }
            public string Name3 { get; set; }
            public string Name4 { get; set; }
            public string Name5 { get; set; }
            public string Name6 { get; set; }

            public override string ToString()
            {
                return Name1 + " " + Name2 + " " + Name3 + " " + Name4 + " " + Name5 + " " + Name6;
            }
        }
        #endregion
    }
}
