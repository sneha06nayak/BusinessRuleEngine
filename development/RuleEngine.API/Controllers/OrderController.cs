using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RulesEngine.Common.Interface;
using RulesEngine.Common.Model;

namespace RulesEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRuleEvaluator<RuleRequest, List<RuleResult>> _ruleEvaluator;
        public OrderController(IRuleEvaluator<RuleRequest, List<RuleResult>> ruleEvaluator)
        {
            _ruleEvaluator = ruleEvaluator ?? throw new ArgumentNullException(nameof(ruleEvaluator));
        }
        [HttpPost("generatePackagingSlip")]
        public async Task<IActionResult> GeneratePackagingSlip(RuleRequest ruleRequest)
        {
            try
            {
                if (ruleRequest == null)
                {
                    return BadRequest("Request input cannot be empty.");
                }
                if (ruleRequest.OrderInfo.ProductInfo?.Any() == false || ruleRequest.OrderInfo.ProductInfo == null)
                {
                    return BadRequest("Request doesnot have any product.");
                }

                return Ok(await _ruleEvaluator.ExecuteAsync(ruleRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
