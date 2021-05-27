using System;
using System.IO;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// ストレージサービス
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// 画像のアップロード
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="containername"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        Task<bool> UploadImg(byte[] base64, string containername, string filename);

        /// <summary>
        /// 画像のダウンロード
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<string> DownloadImg(string containerName, string fileName);

        /// <summary>
        /// 画像の削除
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<bool> DeleteImg(string containerName, string fileName);

        /// <summary>
        /// ストレージのドメインを取得
        /// </summary>
        /// <returns></returns>
        string GetStorageDomain();
    }
}
