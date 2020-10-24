//    Copyright(C) 2020  Viet Do <https://github.com/doducviet>
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
using System;

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

            if (oldValue == null)
            {
                oldValue = string.Empty;
            }

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
