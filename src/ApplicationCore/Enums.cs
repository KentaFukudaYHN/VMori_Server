using System;


namespace ApplicationCore.Enum
{
    /// <summary>
    /// 動画プラットフォームの種類
    /// </summary>
    public enum VideoPlatFormKinds
    {
        UnKnown = 0,
        Youtube = 10,
        NikoNiko = 20,
        Twitch = 30,
        TwitchCasting = 40,
    }

    /// <summary>
    /// 性別種類
    /// </summary>
    public enum GenderKinds
    {
        /// <summary>
        /// 不明・未回答
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// 男性
        /// </summary>
        Male = 100,
        /// <summary>
        /// 女性
        /// </summary>
        Female = 200,
        /// <summary>
        /// その他
        /// </summary>
        Other = 300
    }

    /// <summary>
    /// 動画の言語種類
    /// </summary>
    public enum VideoLanguageKinds
    {
        /// <summary>
        /// 不明
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// 日本語
        /// </summary>
        JP = 10,
        /// <summary>
        /// 英語
        /// </summary>
        English = 20,
        /// <summary>
        /// その他
        /// </summary>
        Other = 30
    }

    /// <summary>
    /// 動画アップロードの際のエラー種類
    /// </summary>
    public enum RegistVideoErrKinds
    {
        /// <summary>
        /// エラー情報なし
        /// </summary>
        None = 0,
        /// <summary>
        /// URLの形式が間違っている
        /// </summary>
        UrlFormat = 10,
        /// <summary>
        /// 対応してないplatform
        /// </summary>
        UnSupportPlatform = 20,
        /// <summary>
        /// 既に登録済み
        /// </summary>
        IsExits = 30,
        /// <summary>
        /// 動画が見つからなかった
        /// </summary>
        NotFound = 40,
        /// <summary>
        /// youtube動画が見つからなかった
        /// </summary>
        NotFoundByYoutube = 50,
        /// <summary>
        /// ニコニコ動画が見つからなかった
        /// </summary>
        NotFoundByNikoNiko = 60,
        /// <summary>
        /// Youtube動画IDが取得できなかった
        /// </summary>
        NotIdByYoutube = 100,
    }

    /// <summary>
    /// 動画のジャンル種類
    /// </summary>
    public enum VideoGenreKinds
    {
        /// <summary>
        /// 全て
        /// </summary>
        All = 0,
        //雑談
        SmallTalk = 10,
        //エンターテイメント
        Entertainment = 20,
        //歌枠
        Song = 30,
        //音楽
        Music = 40,
        //ショートムービー
        Short = 50,
        //ゲーム
        Game = 60,
        //お絵描き
        Drawing = 70,
        //ASMR
        Asmr = 80,
        //ニュース
        News = 90,
        //技術・工作
        Craft = 100,
        //解説・講座
        Course = 110,
        //ドッキリ
        Shock = 120,
        //アウトドア
        Outdoor = 130,
        //自然・環境
        Nature = 140,
        //センシティブ
        Sensitive = 150,
        //その他
        Other = 999,
    }

    /// <summary>
    /// チャンネル情報取得並び替え種類
    /// </summary>
    public enum ChannelSortKins
    {
        /// <summary>
        /// チャンネル情報取得日時
        /// </summary>
        GetRegistDateTime = 0,
        /// <summary>
        /// チャンネル登録者数
        /// </summary>
        SubscriverCount = 10,
        /// <summary>
        /// チャンネルの再生回数
        /// </summary>
        ViewCount = 20,
    }

    /// <summary>
    /// 期間の種類
    /// </summary>
    public enum PeriodKinds
    {
        /// <summary>
        /// 今日
        /// </summary>
        ToDay = 10,
        /// <summary>
        /// 今週
        /// </summary>
        Week = 20,
        /// <summary>
        /// 今月
        /// </summary>
        Month = 30,
        /// <summary>
        /// 全て
        /// </summary>
        All = 1000
    }
}

