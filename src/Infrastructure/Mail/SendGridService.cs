using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
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
        private readonly string from = "system@vmori.com";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="configuration"></param>
        public SendGridService()
        {
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
            var apikey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apikey);
            var from = new EmailAddress(Environment.GetEnvironmentVariable("SENDGRID_TO"));
            var to = new EmailAddress(toAddress);
            var content = MailHelper.CreateSingleEmail(from, to, title, "", msg);
            var res = await client.SendEmailAsync(content).ConfigureAwait(false);

            return true;
        }
    }
}
