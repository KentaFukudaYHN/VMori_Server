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
        private const string USER_ICON_CONATINER = "user-icons";

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
        public async Task<bool> RegistUserIcon(byte[] base64, string fileName)
        {
            return await _storageService.UploadImg(base64, USER_ICON_CONATINER, fileName);
        } 
    }
}
