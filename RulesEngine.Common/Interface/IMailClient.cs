using RulesEngine.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Common.Interface
{
    public interface IMailClient
    {
        /// <summary>
        /// Mail Client.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        Task SendEmail(string from, string to, string subject, string html);
    }
}
