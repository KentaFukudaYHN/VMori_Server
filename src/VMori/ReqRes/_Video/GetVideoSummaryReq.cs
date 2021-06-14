
namespace VMori.ReqRes
{
    /// <summary>
    /// 動画サマリー情報取得リクエスト
    /// </summary>
    public class GetVideoSummaryReq
    {
        /// <summary>
        /// ページ
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 表示件数
        /// </summary>
        public int DisplayNum { get; set; }

        /// <summary>
        /// 近須徳太
        /// </summary>
        public GetVideoSummaryReq() { }
    }
}
