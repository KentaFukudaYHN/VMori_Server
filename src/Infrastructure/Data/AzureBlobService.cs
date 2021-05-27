using ApplicationCore.Config;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    /// <summary>
    /// AzureBlobサービス
    /// </summary>
    public class AzureBlobService : IStorageService
    {
        private readonly StorageConfig _config;
        private readonly CloudBlobClient _cloudBlobClient;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="config"></param>
        public AzureBlobService(IOptions<StorageConfig> config)
        {
            _config = config.Value;

            CloudStorageAccount storageAccount;
            if(CloudStorageAccount.TryParse(_config.ConnectionStrings, out storageAccount))
            {
                _cloudBlobClient = storageAccount.CreateCloudBlobClient();
            }
            else
            {
                throw new Exception("ストレージの接続に失敗しました");
            }
        }

        /// <summary>
        /// 画像のアップロード
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> UploadImg(byte[] base64, string containerName, string filename)
        {
            var container = _cloudBlobClient.GetContainerReference(containerName);
            //コンテナが無ければ作る
            await container.CreateIfNotExistsAsync();
            var cloudBlockBlob = container.GetBlockBlobReference(filename);
            await cloudBlockBlob.UploadFromByteArrayAsync(base64, 0, base64.Length);

            return true;
        }

        /// <summary>
        /// 画像のダウンロード
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<string> DownloadImg(string containerName, string fileName)
        {
            var container = _cloudBlobClient.GetContainerReference(containerName);
            var cloudBlockBlob = container.GetBlockBlobReference(fileName);
            using (var stream = await cloudBlockBlob.OpenReadAsync())
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return  Convert.ToBase64String(ms.ToArray());
                }
            }

        }

        /// <summary>
        /// 画像の削除
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<bool> DeleteImg(string containerName, string fileName)
        {
            var container = _cloudBlobClient.GetContainerReference(containerName);
            var cloudBlockBlob = container.GetBlockBlobReference(fileName);
            await cloudBlockBlob.DeleteAsync();

            return true;
        }

        /// <summary>
        /// ストレージのドメイン名を取得
        /// </summary>
        /// <returns></returns>
        public string GetStorageDomain()
        {
            return _config.Domain;
        }
    }
}
