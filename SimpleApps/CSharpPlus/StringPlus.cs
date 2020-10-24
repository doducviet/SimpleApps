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
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="isRemoveEmptyEntries">(Optional, default is TRUE) TRUE : omit empty elements from the list returned; 
        /// or FALSE to include empty elements in the list returned.</param>
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
