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
using CSharpPlus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Read_IIS_Features_Status
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
            Console.WriteLine("Reading ...");
            Console.WriteLine("\tStatus :");
            Console.WriteLine("\t\t★ - ON");
            Console.WriteLine("\t\t☆ - OFF");
            Console.WriteLine(" - - - - - - - - - - - - - - - - - - - -");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c DISM /online /get-features /format:table | find \"IIS\"";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            string result = string.Empty;

            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    result = reader.ReadToEnd();
                }
            }

            List<string> lstIISFeaturesInfo = result.Split(Environment.NewLine);

            foreach (string iisFeatureInfo in lstIISFeaturesInfo)
            {
                IISFeature iisFeature = new IISFeature();
                iisFeature.FeatureName = iisFeatureInfo.Substring(0, iisFeatureInfo.IndexOf(' '));
                iisFeature.Status = GetFeatureStatus(iisFeatureInfo);
                Console.WriteLine(iisFeature.ToString());
            }

            Console.WriteLine(" - - - - - - - - - - - - - - - - - - - -");
            Console.WriteLine("Press any key to exit.");

            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iisFeatureInfo"></param>
        /// <returns></returns>
        static bool GetFeatureStatus(string iisFeatureInfo)
        {
            bool result = false;

            if (iisFeatureInfo.Contains("ON")) // English
            {
                result = true;
            }
            else if (iisFeatureInfo.Contains("有効")) // Japanese
            {
                result = true;
            }

            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class IISFeature
    {
        public string FeatureName { get; set; }
        public bool Status { get; set; }

        public override string ToString()
        {
            return "\t" + FeatureName + " : " + (Status ? "★" : "☆");
        }
    }
}
