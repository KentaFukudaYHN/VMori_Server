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
        /// 未設定
        /// </summary>
        UnKnown = 0,
        /// <summary>
        ///雑談
        /// <summary>
        SmallTalk = 10,
        /// <summary>
        ///ショートムービー
        /// <summary>
        Short = 20,
        /// <summary>
        ///エンターテイメント
        /// <summary>
        Entertainment = 30,
        /// <summary>
        ///game
        /// <summary>
        Game = 40,
        /// <summary>
        ///音楽
        /// <summary>
        Music = 50,
        /// <summary>
        /// MAD
        /// </summary>
        MAD = 60,
        /// <summary>
        ///ダンス
        /// <summary>
        Dance = 70,
        /// <summary>
        ///ラジオ
        /// <summary>
        Radio = 80,
        /// <summary>
        ///動物
        /// <summary>
        Animal = 90,
        /// <summary>
        ///自然
        /// <summary>
        Nature = 100,
        /// <summary>
        ///料理
        /// <summary>
        Cooking = 110,
        /// <summary>
        ///旅行
        /// <summary>
        Travel = 120,
        /// <summary>
        ///アウトドア
        /// <summary>
        Outdoor = 130,
        /// <summary>
        ///スポーツ
        /// <summary>
        Sports = 140,
        /// <summary>
        ///政治・社会・時事
        /// <summary>
        Politics = 150,
        /// <summary>
        ///技術・工作
        /// <summary>
        Craft = 160,
        /// <summary>
        ///解説・講座
        /// <summary>
        Course = 170,
        /// <summary>
        ///MMD
        /// <summary>
        MMD = 180,
        /// <summary>
        ///その他
        /// <summary>

        Other = 999
    }
}

