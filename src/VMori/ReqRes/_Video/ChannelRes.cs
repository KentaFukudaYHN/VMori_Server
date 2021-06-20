using ApplicationCore.ServiceReqRes;
using System;

namespace VMori.ReqRes
{
    /// <summary>
    /// チャンネル情報レスポンス
    /// </summary>
    public class ChannelRes
    {
        private OutsourceVideoChannelServiceRes _original;

        /// <summary>
        /// チャンネルID
        /// </summary>
        public string ChannelId => _original.ChannelId;

        /// <summary>
        /// チェンネルタイトル
        /// </summary>
        public string Title => _original.Title;

        /// <summary>
        /// チャンネル説明
        /// </summary>
        public string Description => _original.Description;

        /// <summary>
        /// チャンネル作成日
        /// </summary>
        public DateTime? PublishAt => _original.PulishAt;

        /// <summary>
        /// チャンネルサムネイルURL
        /// </summary>
        public string ThumbnailUrl => _original.ThmbnailUrl;

        /// <summary>
        /// チャンネルの再生回数
        /// </summary>
        public ulong? ViewCount => _original.VideoCount;

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
        public ChannelRes(OutsourceVideoChannelServiceRes original)
        {
            _original = original;
        }
    }
}
