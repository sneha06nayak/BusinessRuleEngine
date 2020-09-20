using RulesEngine.Common.Interface;
using System.Threading.Tasks;

namespace RulesEngine.Service.Helpers
{
    public class MailClient : IMailClient
    {
        /// <inheritdoc/>
        public async Task SendEmail(string from, string to, string subject, string html)
        {
            //send email to client
            await Task.CompletedTask;
        }
    }
}
