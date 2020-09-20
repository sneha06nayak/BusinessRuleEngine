using Microsoft.Extensions.Logging;
using RulesEngine.Common.Enums;
using RulesEngine.Common.Interface;
using RulesEngine.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Service.RuleExecutors
{
    public class ProductRuleHandler : IRuleEvaluator<OrderInfo, RuleResult>
    {
        private readonly ILogger<ProductRuleHandler> _logger;
        private readonly ISlipGenerator<OrderInfo, PackagingSlip> _slipGenerator;
        public ProductRuleHandler(ILogger<ProductRuleHandler> logger, ISlipGenerator<OrderInfo, PackagingSlip> slipGenerator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _slipGenerator = slipGenerator ?? throw new ArgumentNullException(nameof(slipGenerator));
        }

        /// <inheritdoc/>
        public async Task<RuleResult> ExecuteAsync(OrderInfo orderInfo)
        {
            orderInfo.ProductInfo = orderInfo.ProductInfo.Where(p => p.ProductType == RuleType.Book).ToList();
            return new RuleResult
            {
                PackagingSlip = new List<PackagingSlip>
                {
                    await _slipGenerator.GenerateSlip(orderInfo),
                    await _slipGenerator.GenerateSlip(orderInfo, Department.Commission)
                }
            };
        }
    }
}
