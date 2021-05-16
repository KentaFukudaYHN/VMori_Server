using ApplicationCore.ReqRes._RecommendVideos;

namespace VMori.ReqRes
{
    /// <summary>
    /// おすすめ動画情報
    /// </summary>
    public class RecommendVieoRes
    {
        private RecommendVideosRes _original;

        /// <summary>
        /// おすすめ動画のタイトル
        /// </summary>
        public string Title => _original.Title;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="orginal"></param>
        public RecommendVieoRes(RecommendVideosRes orginal)
        {
            _original = orginal;
        }
    }
}
