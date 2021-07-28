using ApplicationCore.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMori.ReqRes.Channel
{
    /// <summary>
    /// チャンネル情報リストリクエスト
    /// </summary>
    public class ChannelListReq
    {
        /// <summary>
        /// ページ番号
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 表示数
        /// </summary>
        public int DisplayNum { get; set; }

        /// <summary>
        /// 並び替え種類
        /// </summary>
        public ChannelSortKins SortKinds { get; set; }

        /// <summary>
        /// 並び替えが降順かどうか
        /// </summary>
        public bool IsDesc { get; set; }
    }
}
