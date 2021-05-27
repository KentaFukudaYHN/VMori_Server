using System.IO;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// アカウント情報ストレージサービス
    /// </summary>
    public interface IAccountStorageService
    {
        /// <summary>
        /// ユーザーアイコンのアップロード
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<bool> RegistUserIcon(byte[] base64,string containerName, string fileName);

    }
}
