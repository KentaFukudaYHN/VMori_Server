
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
        public string Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChangebBrthdayReq() { }
    }
}
