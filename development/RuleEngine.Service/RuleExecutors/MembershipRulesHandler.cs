using Microsoft.Extensions.Logging;
using RulesEngine.Common.Interface;
using RulesEngine.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Service.RuleExecutors
{
    public class MembershipRulesHandler : IRuleEvaluator<OrderInfo, RuleResult>
    {
        private readonly IMailClient _mailClient;
        private readonly ILogger<MembershipRulesHandler> _logger;
        public MembershipRulesHandler(IMailClient mailClient, ILogger<MembershipRulesHandler> logger)
        {
            _mailClient = mailClient ?? throw new ArgumentNullException(nameof(mailClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<RuleResult> ExecuteAsync(OrderInfo orderInfo)
        {
            var membershipActivationReq = orderInfo.ProductInfo.FirstOrDefault(p => p.ProductType == RuleType.MembershipActivation);
            var membershipUpgradeReq = orderInfo.ProductInfo.FirstOrDefault(p => p.ProductType == RuleType.MemeberShipUpgrade);
            if(membershipActivationReq != null)
            {
                //activate membership
            }
            else
            {
                //upgrade membership
            }
            await _mailClient.SendEmail("customerservice@desk.com", orderInfo.Email, "Membership Details", "");
            return new RuleResult { Message = "Membership request processed" };
        }
    }
}
