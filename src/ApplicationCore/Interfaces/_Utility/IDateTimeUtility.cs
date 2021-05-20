using System;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// DateTim変換Utilitu
    /// </summary>
    public interface IDateTimeUtility
    {

        /// <summary>
        /// yyyMMddの形式の文字列をDateTimeに変換
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTime ConvertStringToDate(string date);

        /// <summary>
        /// 文字列をDateTimeに変換
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTime ConvertStringToDate(string year, string month, string date);

        /// <summary>
        /// DateTimeをyyyyMMddの形式に変換
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string ConvertDateToString(DateTime date);

    }
}
