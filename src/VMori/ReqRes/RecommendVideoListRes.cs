using System.Collections.Generic;

namespace VMori.ReqRes
{
    /// <summary>
    /// おすすめ動画のリストViewModel
    /// </summary>
    public class RecommendVideoHeaderRes
    {
        /// <summary>
        /// おすすめ動画のリスト
        /// </summary>
        public List<RecommendVieoRes> Videos { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RecommendVideoHeaderRes() { }
    }
}
