using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// ハッシュ化Service
    /// </summary>
    public interface IHashService
    {
        /// <summary>
        /// ハッシュ化
        /// </summary>
        /// <param name="val"></param>
        /// <returns>ソルト付きのハッシュ値</returns>
        string Hashing(string val, byte[] salt);

        /// <summary>
        /// ハッシュ化
        /// </summary>
        /// <param name="val"></param>
        /// <returnsソルト付きのハッシュ値></returns>
        string Hashing(string val);

        /// <summary>
        /// 確認
        /// </summary>
        /// <param name="hash">ソルト付きのハッシュ</param>
        /// <returns></returns>
        bool Verify(string hash, string target);
    }
}
