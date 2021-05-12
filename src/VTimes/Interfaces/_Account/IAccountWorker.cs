using System;
using System.Threading.Tasks;
using VMori.ViewModel;

namespace VMori.Interfaces
{
    public interface IAccountWorker
    {
        /// <summary>
        /// アカウントの登録
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        Task<bool> Regist(RegistAccountViewModel vm);

        /// <summary>
        /// メールアドレスの使用チェック
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Task<bool> NotExitsMail(string mail);
    }
}
