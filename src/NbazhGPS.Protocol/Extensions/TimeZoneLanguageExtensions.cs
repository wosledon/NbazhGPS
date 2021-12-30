using System;
using System.Globalization;

namespace NbazhGPS.Protocol.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TimeZoneLanguageExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tzl"></param>
        /// <returns></returns>
        public static string FormatTimeZoneLanguageTime(this float tzl)
        {
            var formatString = tzl.ToString("0.00");
            var nums = formatString.Split('.');
            if (nums.Length >= 2)
            {
                return $"{nums[0]}:{(nums[1].Length >= 2 ? nums[1] : "0" + nums[1])}";
            }

            return $"{formatString.Substring(0, formatString.Length - 2)}:{formatString.Substring(formatString.Length - 2)}";
        }

        /// <summary>
        /// 进一步划分时区, 如: 东八区又1/4时区
        /// </summary>
        /// <param name="tzl"></param>
        /// <returns></returns>
        [Obsolete("时区不进行细致划分", false)]
        public static string FormatTimeZoneLanguageZone(this int tzl)
        {
            var formatString = tzl.ToString();
            if (formatString.Length <= 2)
            {
                return $"{tzl}:00";
            }

            return $"{formatString.Substring(0, formatString.Length - 2)}:{formatString.Substring(formatString.Length - 2)}";
        }
    }
}