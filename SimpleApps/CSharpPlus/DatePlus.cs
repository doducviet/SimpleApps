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
using System.Globalization;
using System.Linq;

namespace CSharpPlus
{
    public static class DatePlus
    {
        /// <summary>
        /// Get the total day in the month
        /// </summary>
        /// <param name="iYear">Year of the month to get total days</param>
        /// <param name="iMonth">Month to get total days</param>
        /// <returns></returns>
        public static int GetDayInMonth(int iYear, int iMonth)
        {
            return DateTime.DaysInMonth(iYear, iMonth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static int GetDistance(DateTime fromDate, DateTime toDate)
        {
            return toDate.DayOfYear - fromDate.DayOfYear;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iFromYear"></param>
        /// <param name="iFromMonth"></param>
        /// <param name="iFromDay"></param>
        /// <param name="iToYear"></param>
        /// <param name="iToMonth"></param>
        /// <param name="iToDay"></param>
        /// <returns></returns>
        public static int GetDistance(int iFromYear, int iFromMonth, int iFromDay, int iToYear, int iToMonth, int iToDay)
        {
            DateTime startDate = new DateTime(iFromYear, iFromMonth, iFromDay);
            DateTime toDate = new DateTime(iToYear, iToMonth, iToDay);

            return GetDistance(startDate, toDate);
        }

        /// <summary>
        /// Get info of the day
        /// </summary>
        /// <param name="dateTime">Date of the Day to get information</param>
        /// <returns>Day's information</returns>
        public static DayInfo GetDayInfo(DateTime dateTime)
        {
            return GetDayInfo(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        /// <summary>
        /// Get info of the day
        /// </summary>
        /// <param name="iYear">Year of the Day to get information</param>
        /// <param name="iMonth">Month of the Day to get information</param>
        /// <param name="iDay">Day to get information</param>
        /// <returns>Day's information</returns>
        public static DayInfo GetDayInfo(int iYear, int iMonth, int iDay)
        {
            return (new DayInfo(iYear, iMonth, iDay));
        }

        /// <summary>
        /// Get info of the month
        /// </summary>
        /// <param name="dateTime">Date of the Month to get information</param>
        /// <returns>Month's information (List of all Day's information in month)</returns>
        public static List<DayInfo> GetMonthInfo(DateTime dateTime)
        {
            return GetMonthInfo(dateTime.Year, dateTime.Month);
        }

        /// <summary>
        /// Get info of the month
        /// </summary>
        /// <param name="iYear">Year of the Month to get information</param>
        /// <param name="iMonth">Month to get information</param>
        /// <returns>Month's information (List of all Day's information in month)</returns>
        public static List<DayInfo> GetMonthInfo(int iYear, int iMonth)
        {
            List<DayInfo> lst = new List<DayInfo>();
            for (int iDay = 1; iDay <= DateTime.DaysInMonth(iYear, iMonth); iDay++)
            {
                lst.Add(GetDayInfo(iYear, iMonth, iDay));
            }

            return lst;
        }

        /// <summary>
        /// Get info of the year
        /// </summary>
        /// <param name="dateTime">Date of the Year to get information</param>
        /// <returns>Year's information (List of all Day's information in year)</returns>
        public static List<DayInfo> GetYearInfo(DateTime dateTime)
        {
            return GetYearInfo(dateTime.Year);
        }

        /// <summary>
        /// Get info of the year
        /// </summary>
        /// <param name="iYear">Year to get information</param>
        /// <returns>Year's information (List of all Day's information in year)</returns>
        public static List<DayInfo> GetYearInfo(int iYear)
        {
            List<DayInfo> lst = new List<DayInfo>();

            for (int iMonth = 1; iMonth <= 12; iMonth++)
            {
                lst.AddRange(GetMonthInfo(iYear, iMonth));
            }

            return lst;
        }

        /// <summary>
        /// Find the next DayOfWeek
        /// </summary>
        /// <param name="startDate">Start date to find the next DayOfWeek</param>
        /// <param name="dayOfWeek">Next DayOfWeek want to find</param>
        /// <returns>DateTime of the next DayOfWeek</returns>
        public static DateTime FindNextDayOfWeek(DateTime startDate, DayOfWeek dayOfWeek)
        {
            return FindNextDayOfWeek(startDate.Year, startDate.Month, startDate.Day, dayOfWeek);
        }

        /// <summary>
        /// Find the next DayOfWeek
        /// </summary>
        /// <param name="iYear">Year of start date to start find the next DayOfWeek</param>
        /// <param name="iMonth">Month of start date to start find the next DayOfWeek</param>
        /// <param name="iStartDay">Day of start date to start find the next DayOfWeek</param>
        /// <param name="dayOfWeek">Next DayOfWeek want to find</param>
        /// <returns>DateTime of the next DayOfWeek</returns>
        public static DateTime FindNextDayOfWeek(int iYear, int iMonth, int iStartDay, DayOfWeek dayOfWeek)
        {
            DateTime dateTime = new DateTime(iYear, iMonth, iStartDay);

            for (; ; )
            {
                if (dateTime.DayOfWeek == dayOfWeek)
                {
                    break;
                }

                dateTime = dateTime.AddDays(1);
            }

            return dateTime;
        }

        /// <summary>
        /// Find all DayOfWeek in the month
        /// </summary>
        /// <param name="dateTime">Date of month to get all DayOfWeek</param>
        /// <param name="dayOfWeek"></param>
        /// <returns>All DayOfWeek in the year</returns>
        public static List<DayInfo> GetListDayOfWeekInMonth(DateTime dateTime, DayOfWeek dayOfWeek)
        {
            return GetListDayOfWeekInMonth(dateTime.Year, dateTime.Month, dayOfWeek);
        }

        /// <summary>
        /// Find all DayOfWeek in the month
        /// </summary>
        /// <param name="iYear">Year of month to get all DayOfWeek</param>
        /// <param name="iMonth">Month to get all DayOfWeek</param>
        /// <param name="dayOfWeek"></param>
        /// <returns>All DayOfWeek in the month</returns>
        public static List<DayInfo> GetListDayOfWeekInMonth(int iYear, int iMonth, DayOfWeek dayOfWeek)
        {
            return GetMonthInfo(iYear, iMonth).Where(a => a.DayOfWeek == dayOfWeek).ToList();
        }

        /// <summary>
        /// Find all DayOfWeek in the year
        /// </summary>
        /// <param name="dateTime">Date of year to get all DayOfWeek</param>
        /// <param name="dayOfWeek"></param>
        /// <returns>All DayOfWeek in the year</returns>
        public static List<DayInfo> GetListDayOfWeekInYear(DateTime dateTime, DayOfWeek dayOfWeek)
        {
            return GetListDayOfWeekInYear(dateTime.Year, dayOfWeek);
        }

        /// <summary>
        /// Find all DayOfWeek in the year
        /// </summary>
        /// <param name="iYear">Year to get all DayOfWeek</param>
        /// <param name="dayOfWeek"></param>
        /// <returns>All DayOfWeek in the year</returns>
        public static List<DayInfo> GetListDayOfWeekInYear(int iYear, DayOfWeek dayOfWeek)
        {
            return GetYearInfo(iYear).Where(a => a.DayOfWeek == dayOfWeek).ToList();
        }
    }

    public class DayInfo
    {
        public DateTime Date
        {
            get;
            private set;
        }

        public string DateStr
        {
            get;
            private set;
        }

        public DayOfWeek DayOfWeek
        {
            get;
            private set;
        }

        public int DayOfMonth
        {
            get;
            private set;
        }

        public int DayOfYear
        {
            get;
            private set;
        }

        public int WeekOfMonth
        {
            get;
            private set;
        }

        public int WeekOfYear
        {
            get;
            private set;
        }

        ///// <summary>
        ///// Default constructor
        ///// </summary>
        //public DayInfo()
        //{
        //}

        public DayInfo(DateTime date)
        {
            this.Date = date;

            SetDetailInfo();
        }

        public DayInfo(int iYear, int iMonth, int iDay) : this(new DateTime(iYear, iMonth, iDay))
        {
        }

        private void SetDetailInfo()
        {
            this.DateStr = this.Date.ToString("yyyyMMdd");
            this.DayOfWeek = this.Date.DayOfWeek;
            this.DayOfMonth = this.Date.Day;
            this.DayOfYear = this.Date.DayOfYear;

            GregorianCalendar gc = new GregorianCalendar();
            this.WeekOfMonth = gc.GetWeekOfYear(this.Date, CalendarWeekRule.FirstDay, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                                    - gc.GetWeekOfYear(new DateTime(this.Date.Year, this.Date.Month, 1), CalendarWeekRule.FirstDay, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                                    + 1;
            this.WeekOfYear = gc.GetWeekOfYear(this.Date, CalendarWeekRule.FirstDay, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

        }
    }
}
