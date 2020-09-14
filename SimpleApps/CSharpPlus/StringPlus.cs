using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlus
{
    public static class StringPlus
    {
        /// <summary>
        /// Extension to the string's Split method. 
        ///  Splits a string into substrings based on a string.
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="strSeperator">A string that delimits the substrings in this string</param>
        /// <param name="isRemoveEmptyEntries">(Optional, default is TRUE) omit empty array elements from
        /// the array returned; or System.StringSplitOptions.None to include empty array
        /// elements in the array returned.</param>
        /// <returns></returns>
        public static List<string> Split(this string strInput, string strSeperator, bool isRemoveEmptyEntries = true)
        {
            List<string> lstResult;

            if (!string.IsNullOrEmpty(strInput.Trim()))
            {
                lstResult = strInput.Split(   new string[] { strSeperator }
                                            , isRemoveEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None
                                           ).ToList();
            }
            else
            {
                lstResult = new List<string>();
            }

            return lstResult;
        }
    }
}
