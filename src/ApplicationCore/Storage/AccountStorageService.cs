using ApplicationCore.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// アカウント情報ストレージサービス
    /// </summary>
    public class AccountStorageService : IAccountStorageService
    {
        private readonly IStorageService _storageService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="storageService"></param>
        public AccountStorageService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <summary>
        /// ユーザーアイコンのアップロード
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RegistUserIcon(byte[] base64, string containerName, string fileName)
        {
            return await _storageService.UploadImg(base64, containerName, fileName);
        } 

        /// <summary>
        /// 画像の取得
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<string> GetUserIcon(string containerName, string fileName)
        {
            return await _storageService.DownloadImg(containerName, fileName);
        }
    }
}
