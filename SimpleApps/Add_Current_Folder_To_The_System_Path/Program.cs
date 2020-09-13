using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Add_Current_Folder_To_The_System_Path
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string currentDirectory = Environment.CurrentDirectory;

            string name = "PATH";
            EnvironmentVariableTarget scope = EnvironmentVariableTarget.Machine;
            string oldValue = Environment.GetEnvironmentVariable(name, scope);

            if (oldValue.Contains(currentDirectory))
            {
                Console.WriteLine("Current folder is already added to the system's path." + Environment.NewLine + "\t{0}", currentDirectory);
            }
            else
            {
                string newValue = oldValue + ";" + currentDirectory;
                Environment.SetEnvironmentVariable(name, newValue, scope);

                Console.WriteLine("Added to the system's path:" + Environment.NewLine + "\t{0}", currentDirectory);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}
