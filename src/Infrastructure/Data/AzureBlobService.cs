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
        public async Task<bool> UploadImg(Stream stream, string containername, string filename)
        {
            var container = _cloudBlobClient.GetContainerReference(containername);
            var cloudBloblBlob = container.GetBlockBlobReference(filename);
            await cloudBloblBlob.UploadFromStreamAsync(stream);

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
