using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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

            List<string> lstIISFeaturesInfo = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            lstIISFeaturesInfo.Remove(string.Empty);

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
