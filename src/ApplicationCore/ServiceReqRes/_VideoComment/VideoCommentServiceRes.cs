using ApplicationCore.Entities;


namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 動画コメントレスポンス
    /// </summary>
    public class VideoCommentServiceRes
    {
        private VideoComment _original;

        /// <summary>
        /// テキスト
        /// </summary>
        public string Text => _original.Text;

        /// <summary>
        /// タイム
        /// </summary>
        public int Time => _original.Time;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VideoCommentServiceRes(VideoComment original)
        {
            _original = original;
        }
    }
}
