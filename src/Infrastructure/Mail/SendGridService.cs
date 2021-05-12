using ApplicationCore.Config;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Mail
{
    /// <summary>
    /// SendGridを使用するメールサービス
    /// </summary>
    public class SendGridService : IMailService
    {
        private readonly MailConfig _mailConfig;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configuration"></param>
        public SendGridService(IOptions<MailConfig> mailConfig)
        {
            _mailConfig = mailConfig.Value;
        }

        /// <summary>
        /// メールの送信
        /// </summary>
        /// <param name="To"></param>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public async Task<bool> SendMail(string toAddress, string title, string msg)
        {
            var apikey = _mailConfig.ApiKey;
            var client = new SendGridClient(apikey);
            var from = new EmailAddress(_mailConfig.SystemMailAddress);
            var to = new EmailAddress(toAddress);
            var content = MailHelper.CreateSingleEmail(from, to, title, "", msg);
            var res = await client.SendEmailAsync(content).ConfigureAwait(false);

            return true;
        }
    }
}
