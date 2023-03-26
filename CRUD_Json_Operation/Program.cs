using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Json_Operation
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentList studentlist = new StudentList();
            Console.WriteLine("**--**--** Student Management **--**--**\n========================================");
            studentlist.DashBoard();
            Console.ReadLine();
        }
    }
}
