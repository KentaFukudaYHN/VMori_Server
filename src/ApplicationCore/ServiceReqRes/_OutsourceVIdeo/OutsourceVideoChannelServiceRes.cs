using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 外部サービスチャンネル情報
    /// </summary>
    public class OutsourceVideoChannelServiceRes
    {
        private OutsourceVideoChannel _original;

        /// <summary>
        /// チャンネルID
        /// </summary>
        public string ChannelId => _original.ID;

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title => _original.Title;

        /// <summary>
        /// 説明
        /// </summary>
        public string Description => _original.Description;

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime? PulishAt => _original.PublishAt;

        /// <summary>
        /// サムネイル
        /// </summary>
        public string ThmbnailUrl => _original.ThumbnailUrl;

        /// <summary>
        /// チャンネルの再生回数
        /// </summary>
        public ulong? ViewCount => _original.ViewCount;

        /// <summary>
        /// チャンネルのコメント数
        /// </summary>
        public ulong? CommentCount => _original.CommentCount;

        /// <summary>
        /// チャンネル登録者数
        /// </summary>
        public ulong? SubscriverCount => _original.SubscriverCount;

        /// <summary>
        /// チャンネルにアップロードされた動画数
        /// </summary>
        public ulong? VideoCount => _original.VideoCount;

        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime GetRegistDateTime => _original.GetRegistDateTime;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="entity"></param>
        public OutsourceVideoChannelServiceRes(OutsourceVideoChannel entity)
        {
            _original = entity;
        }
    }
}
