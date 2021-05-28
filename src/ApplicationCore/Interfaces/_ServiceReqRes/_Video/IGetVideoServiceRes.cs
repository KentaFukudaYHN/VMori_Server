namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// 取得動画情報のインターフェース
    /// </summary>
    public interface IGetVideoServiceRes
    {
        /// <summary>
        /// 動画タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 動画リンク
        /// </summary>
        public string VideoLink { get; set; }

        /// <summary>
        /// サムネイルリンク
        /// </summary>
        public string ThumbnailLink { get; set; }
    }
}
