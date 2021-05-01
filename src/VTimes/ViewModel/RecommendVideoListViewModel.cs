using System.Collections.Generic;

namespace VMori.ViewModel
{
    /// <summary>
    /// おすすめ動画のリストViewModel
    /// </summary>
    public class RecommendVideoHeaderViewModel
    {
        /// <summary>
        /// おすすめ動画のリスト
        /// </summary>
        public List<RecommendVieoViewModel> Videos { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RecommendVideoHeaderViewModel() { }
    }
}
