using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    /// <summary>
    /// メールサービス
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// メールの送信
        /// </summary>
        /// <param name="To"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task<bool> SendMail(string To, string title, string msg);
    }
}
