using ApplicationCore.Entities;
using ApplicationCore.Enum;
using System;
using System.Collections.Generic;

namespace ApplicationCore.ServiceReqRes
{
    /// <summary>
    /// 動画情報
    /// </summary>
    public  class OutsourceVideoServiceRes
    {
        private Video _original;

        public string Id => _original.ID;

        /// <summary>
        /// 動画ID
        /// </summary>
        public string VideoId => _original.VideoId;

        /// <summary>
        /// 動画プラットフォームの種類
        /// </summary>
        public VideoPlatFormKinds PlatFormKinds => _original.PlatFormKinds;

        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string VideoTitle => _original.VideoTitle;

        /// <summary>
        /// チャンネルID
        /// </summary>
        public string ChannelId => _original.ChanelId;
        
        /// <summary>
        /// チャンネルタイトル
        /// </summary>
        public string ChannelTitle => _original.ChanelTitle;

        /// <summary>
        /// 動画説明
        /// </summary>
        public string Description => _original.Description;

        /// <summary>
        /// サムネイルリンク
        /// </summary>
        public string ThumbnailLink => _original.ThumbnailLink;

        /// <summary>
        /// ジャンル
        /// </summary>
        public VideoGenreKinds Genre => _original.Genre;

        /// <summary>
        /// タグ
        /// </summary>
        public List<string> Tags => _original.Tags;

        /// <summary>
        /// 日本語を話しているか　
        /// </summary>
        public bool SpeakJP => _original.SpeakJP;

        /// <summary>
        /// 英語を話しているか
        /// </summary>
        public bool SpeakEnglish => _original.SpeakEnglish;

        /// <summary>
        /// その他の言語を話しているか
        /// </summary>
        public bool SpeakOther => _original.SpeakOther;

        /// <summary>
        /// 翻訳の有無
        /// </summary>
        public bool IsTranslation => _original.IsTranslation;

        /// <summary>
        /// 日本語の翻訳がされているか
        /// </summary>
        public bool TranslationJp => _original.TranslationJP;

        /// <summary>
        /// 英語の翻訳がされているか
        /// </summary>
        public bool TranslationEnglish => _original.TranslationEnglish;

        /// <summary>
        /// その他の言語で翻訳されているか
        /// </summary>
        public bool TranslationOther => _original.TranslationOther;

        /// <summary>
        /// 投稿日時
        /// </summary>
        public DateTime PublishDateTime => _original.PublishDateTime;

        /// <summary>
        /// V森への登録日時
        /// </summary>
        public DateTime RegistDateTime => _original.RegistDateTime;

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
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public OutsourceVideoServiceRes(Video original)
        {
            _original = original;
        }
    }
}
