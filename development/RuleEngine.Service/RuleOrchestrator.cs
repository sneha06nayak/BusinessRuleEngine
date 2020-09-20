using Microsoft.Extensions.Logging;
using RulesEngine.Common.Interface;
using RulesEngine.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Service
{
    public class RuleOrchestrator : IRuleEvaluator<RuleRequest, List<RuleResult>>
    {
        private readonly Func<RuleType, IRuleEvaluator<OrderInfo, RuleResult>> _ruleFactory;
        private readonly ILogger<RuleOrchestrator> _logger;

        public RuleOrchestrator(Func<RuleType, IRuleEvaluator<OrderInfo, RuleResult>> ruleFactory, ILogger<RuleOrchestrator> logger)
        {
            _ruleFactory = ruleFactory ?? throw new ArgumentNullException(nameof(ruleFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
        }

        /// <inheritdoc/>
        public async Task<List<RuleResult>> ExecuteAsync(RuleRequest ruleRequest)
        {
            var result = new List<RuleResult>();
            try
            {
                if (ruleRequest.OrderInfo.ProductInfo?.Any() == false || ruleRequest.OrderInfo.ProductInfo == null)
                {
                    return result;
                }
                foreach (var ruleType in ruleRequest.OrderInfo.ProductInfo)
                {
                    var ruleHandler = _ruleFactory(ruleType.ProductType);
                    result.Add(await ruleHandler.ExecuteAsync(ruleRequest.OrderInfo));
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"RuleOrchestrator::ExecuteAsync - {ex.Message}", ex);
            }
            return result;
        }
    }
}
