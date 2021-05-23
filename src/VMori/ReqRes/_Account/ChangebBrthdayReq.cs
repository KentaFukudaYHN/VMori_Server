
namespace VMori.ReqRes
{
    /// <summary>
    /// 誕生日変更リクエスト
    /// </summary>
    public class ChangebBrthdayReq
    {
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public int Date { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChangebBrthdayReq() { }
    }
}
