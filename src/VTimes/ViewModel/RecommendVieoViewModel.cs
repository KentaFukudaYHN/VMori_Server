using ApplicationCore.ReqRes._RecommendVideos;

namespace VMori.ViewModel
{
    /// <summary>
    /// おすすめ動画情報
    /// </summary>
    public class RecommendVieoViewModel
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
        public RecommendVieoViewModel(RecommendVideosRes orginal)
        {
            _original = orginal;
        }
    }
}
