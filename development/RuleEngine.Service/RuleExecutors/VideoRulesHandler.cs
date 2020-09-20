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
    public class VideoRulesHandler : IRuleEvaluator<OrderInfo, RuleResult>
    {
        private readonly ILogger<VideoRulesHandler> _logger;
        private readonly ISlipGenerator<OrderInfo, PackagingSlip> _slipGenerator;
        public VideoRulesHandler(ILogger<VideoRulesHandler> logger, ISlipGenerator<OrderInfo, PackagingSlip> slipGenerator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _slipGenerator = slipGenerator ?? throw new ArgumentNullException(nameof(slipGenerator));
        }

        /// <inheritdoc/>
        public async Task<RuleResult> ExecuteAsync(OrderInfo orderInfo)
        {
            orderInfo.ProductInfo = orderInfo.ProductInfo.Where(p => p.ProductType == RuleType.Book).ToList();
            orderInfo.ProductInfo.Add(new Product { Name = "First Aid", ProductType = RuleType.Video});
            return new RuleResult
            {
                PackagingSlip = new List<PackagingSlip>
                {
                    await _slipGenerator.GenerateSlip(orderInfo)
                }
            };
        }
    }
}
