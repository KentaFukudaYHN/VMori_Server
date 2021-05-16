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
        Task<bool> UploadImg(Stream stream, string containername, string filename);

        /// <summary>
        /// ストレージのドメインを取得
        /// </summary>
        /// <returns></returns>
        string GetStorageDomain();
    }
}
