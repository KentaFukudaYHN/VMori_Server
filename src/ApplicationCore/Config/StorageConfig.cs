namespace ApplicationCore.Config
{
    /// <summary>
    /// ストレージ用設定情報
    /// </summary>
    public class StorageConfig
    {
        /// <summary>
        /// ストレージ接続文字列
        /// </summary>
        public string ConnectionStrings { get; set; }

        /// <summary>
        /// ストレージのドメイン
        /// </summary>
        public string Domain { get; set; }
    }
}
