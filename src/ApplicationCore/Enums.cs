using System;


namespace ApplicationCore.Enum
{
    /// <summary>
    /// 動画プラットフォームの種類
    /// </summary>
    public enum PlatFormKinds
    {
        Youtube = 10,
        NikoNiko = 20,
        Twitch = 30,
        TwitchCasting = 40,
    }

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
}
