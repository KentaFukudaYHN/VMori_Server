using ApplicationCore.Entities;
using System;

namespace ApplicationCore.ServiceReqRes.Channel
{
    /// <summary>
    /// チャンネル情報レスポンス
    /// </summary>
    public class ChannelServiceRes
    {
        private Entities.Channel _original;

        /// <summary>
        /// ID
        /// </summary>
        public string ID => _original.ID;

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title => _original.Title;

        /// <summary>
        /// 説明
        /// </summary>
        public string Description => _original.Description;

        /// <summary>
        /// チャンネル作成日
        /// </summary>
        public DateTime? PublishAt => _original.PublishAt;

        /// <summary>
        /// サムネイルURL
        /// </summary>
        public string ThumbnailUrl => _original.ThumbnailUrl;

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
        /// V森登録日時
        /// </summary>
        public DateTime GetRegistDateTime => _original.GetRegistDateTime;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public ChannelServiceRes(Entities.Channel original)
        {
            _original = original;
        }
    }
}
