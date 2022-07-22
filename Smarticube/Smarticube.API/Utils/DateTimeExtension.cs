using System.Globalization;

namespace Smarticube.API.Utils
{
    public static class DateTimeExtension
    {

        private static DateTime tempDateTime;
        private static DateTime? tempDateTime1;

        public static DateTime GetLastDateOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static DateTime GetLastDateOfMonth(int year, int month)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }


        public static DateTime GetFirstDateOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime GetFirstDateOfMonth(int year, int month)
        {
            return new DateTime(year, month, 1);
        }


        public static DateTime? Convert(string value)
        {
            return DateTime.TryParse(value, out tempDateTime) ? tempDateTime : new DateTime?();
        }


        public static decimal GetNoOfDays(DateTime startDate, DateTime endDate)
        {
            return (decimal)(Math.Abs((startDate - endDate).TotalDays) + 1);
        }


        public static decimal GetNoOfHours(DateTime startDate, DateTime endDate)
        {
            return (decimal)(Math.Abs((startDate - endDate).TotalHours));
        }


        public static decimal GetNoOfMinutes(DateTime startDate, DateTime endDate)
        {
            return (decimal)(Math.Abs((startDate - endDate).TotalMinutes));
        }


        public static string GetDateLabel(DateTime theDate, string language, bool engShortDesc)
        {

            string result = theDate.ToString("yyyy-MM-dd");
            CultureInfo culture = new CultureInfo(string.Empty);

            if (language.ToUpper().Equals("ENG"))
            {
                result = theDate.Day.ToString().PadLeft(2, '0') + " " + GetMonthLabel(theDate.Month, language, engShortDesc) + " " + theDate.Year.ToString();
            }

            if (language.ToUpper().Equals("CHI"))
            {
                result = theDate.Year.ToString() + "年" + theDate.Month.ToString() + "月" + theDate.Day.ToString() + "日";
            }

            return result;
        }


        public static string RestoreDateLabel(string dateLabel)
        {
            string result = string.Empty;

            dateLabel = dateLabel.Replace("年", "-").Replace("月", "-").Replace("日", string.Empty);
            DateTime? theDate = null;
            DateTime theDate1 = new DateTime();
            theDate = DateTime.TryParse(dateLabel, out theDate1) ? theDate1 : theDate;

            if (theDate != null) result = theDate.Value.Year.ToString() + "-" + theDate.Value.Month.ToString().PadLeft(2, '0') + "-" + theDate.Value.Day.ToString().PadLeft(2, '0');

            return result;
        }


        /// <summary>
        /// language = "ENG" or "CHI" or "NUM"
        /// </summary>
        /// <param name="month"></param>
        /// <param name="language"></param>
        /// <param name="engShortDesc"></param>
        /// <returns></returns>
        public static string GetMonthLabel(int month, string language, bool engShortDesc)
        {

            string result = string.Empty;
            if (language.ToUpper().Trim().Equals("ENG"))
            {
                if (month.Equals(1)) result = engShortDesc ? "JAN" : "JANUARY";
                if (month.Equals(2)) result = engShortDesc ? "FEB" : "FEBRUARY";
                if (month.Equals(3)) result = engShortDesc ? "MAR" : "MARCH";
                if (month.Equals(4)) result = engShortDesc ? "APR" : "APRIL";
                if (month.Equals(5)) result = engShortDesc ? "MAY" : "MAY";
                if (month.Equals(6)) result = engShortDesc ? "JUN" : "JUNE";
                if (month.Equals(7)) result = engShortDesc ? "JUL" : "JULY";
                if (month.Equals(8)) result = engShortDesc ? "AUG" : "AUGUST";
                if (month.Equals(9)) result = engShortDesc ? "SEP" : "SEPTEMBER";
                if (month.Equals(10)) result = engShortDesc ? "OCT" : "OCTOBER";
                if (month.Equals(11)) result = engShortDesc ? "NOV" : "NOVEMBER";
                if (month.Equals(12)) result = engShortDesc ? "DEC" : "DECEMBER";
            }

            if (language.ToUpper().Trim().Equals("CHI"))
            {
                if (month.Equals(1)) result = "一月";
                if (month.Equals(2)) result = "二月";
                if (month.Equals(3)) result = "三月";
                if (month.Equals(4)) result = "四月";
                if (month.Equals(5)) result = "五月";
                if (month.Equals(6)) result = "六月";
                if (month.Equals(7)) result = "七月";
                if (month.Equals(8)) result = "八月";
                if (month.Equals(9)) result = "九月";
                if (month.Equals(10)) result = "十月";
                if (month.Equals(11)) result = "十一月";
                if (month.Equals(12)) result = "十二月";
            }


            if (language.ToUpper().Trim().Equals("NUM"))
            {
                if (month.Equals(1)) result = "01";
                if (month.Equals(2)) result = "02";
                if (month.Equals(3)) result = "03";
                if (month.Equals(4)) result = "04";
                if (month.Equals(5)) result = "05";
                if (month.Equals(6)) result = "06";
                if (month.Equals(7)) result = "07";
                if (month.Equals(8)) result = "08";
                if (month.Equals(9)) result = "09";
                if (month.Equals(10)) result = "10";
                if (month.Equals(11)) result = "11";
                if (month.Equals(12)) result = "12";
            }
            return result;
        }




        public static List<DateTime> GetAllSatAndSuns(int year)
        {
            List<DateTime> Dates = new List<DateTime>();


            DateTime Date = new DateTime(year, 1, 1);


            while (Date.Year == year)
            {
                if ((Date.DayOfWeek == DayOfWeek.Saturday) ||
                (Date.DayOfWeek == DayOfWeek.Sunday))
                    Dates.Add(Date);
                Date = Date.AddDays(1);
            }
            return Dates;
        }


        public static DateTime GetChinaStandardDatetime()
        {
            DateTime serverTime = DateTime.Now;
            DateTime utcTime = serverTime.ToUniversalTime();
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

            return localTime;
        }

    }
}
