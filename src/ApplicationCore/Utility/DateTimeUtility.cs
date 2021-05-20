using ApplicationCore.Interfaces;
using System;

namespace ApplicationCore.Utility
{
    public class DateTimeUtility : IDateTimeUtility
    {


        /// <summary>
        /// yyyyMMddの形式の文字列をDateTimeに変換
        /// </summary>
        /// <param name="date">yyyyMMdd</param>
        /// <returns></returns>
        public DateTime ConvertStringToDate(string date)
        {
            return new DateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(4, 2)), int.Parse(date.Substring(6, 2)));
        }

        /// <summary>
        /// 文字列をDateTimeに変換
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime ConvertStringToDate(string year, string month, string date)
        {
            return new DateTime(int.Parse(year), int.Parse(month), int.Parse(date));
        }

        /// <summary>
        /// DateTimeをyyyyMMddの形式に変換
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string ConvertDateToString(DateTime date)
        {
            return date.ToString("yyyMMdd");
        }
    }
}
