using ApplicationCore.ServiceReqRes;


namespace VMori.ReqRes
{
    /// <summary>
    /// 動画コメントレスポンス
    /// </summary>
    public class VideoCommentRes
    {
        private VideoCommentServiceRes _original;
        /// <summary>
        /// テキスト
        /// </summary>
        public string Text => _original.Text;
        /// <summary>
        /// 時間 ※動画開始からの秒数
        /// </summary>
        public int Time => _original.Time;

        public VideoCommentRes(VideoCommentServiceRes original)
        {
            _original = original;
        }
    }
}
