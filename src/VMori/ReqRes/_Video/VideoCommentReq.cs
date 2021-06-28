

namespace VMori.ReqRes
{
    /// <summary>
    /// 動画コメント登録リクエスト
    /// </summary>
    public class VideoCommentReq
    {
        /// <summary>
        /// 動画ID
        /// </summary>
        public string VideoId { get; set; }
        /// <summary>
        /// テキスト
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// タイム
        /// </summary>
        public int Time { get; set; }

        public VideoCommentReq() { }
    }
}
