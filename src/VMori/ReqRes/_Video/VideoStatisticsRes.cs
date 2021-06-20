using ApplicationCore.ServiceReqRes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMori.ReqRes
{
    /// <summary>
    /// 動画統計情報レスポンス
    /// </summary>
    public class VideoStatisticsRes
    {
        private OutsourceVideoStatisticsServiceRes _original;

        /// <summary>
        /// 再生回数
        /// </summary>
        public ulong ViewCount => _original.ViewCount;

        /// <summary>
        /// いいね数
        /// </summary>
        public ulong LikeCount => _original.LikeCount;

        /// <summary>
        /// コメント数
        /// </summary>
        public ulong CommentCount => _original.CommentCount;

        /// <summary>
        /// 取得日時
        /// </summary>
        public DateTime GetDateTime => _original.GetDateTime;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public VideoStatisticsRes(OutsourceVideoStatisticsServiceRes original)
        {
            _original = original;
        }
    }
}
