using ApplicationCore.Entities;

namespace ApplicationCore.ReqRes._RecommendVideos
{
    /// <summary>
    /// おすすめ動画レスポンスクラス
    /// </summary>
    public class RecommendVideosRes
    {
        private VideoInfo _original;

        /// <summary>
        /// ID
        /// </summary>
        public string ID => _original.ID;

        /// <summary>
        /// 動画title
        /// </summary>
        public string Title => _original.Title;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public RecommendVideosRes(VideoInfo original)
        {
            this._original = original;
        }
    }
}
