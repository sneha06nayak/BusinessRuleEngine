using Microsoft.Extensions.DependencyInjection;
using RulesEngine.Common.Interface;
using RulesEngine.Common.Model;
using RulesEngine.Service;
using RulesEngine.Service.Helpers;
using RulesEngine.Service.RuleExecutors;
using RulesEngine.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesEngine.API
{
    public static class DependencyExtension
    {
        public static void RegisterApplicationDI(this IServiceCollection services)
        {
            services.AddScoped<IRuleEvaluator<RuleRequest, List<RuleResult>>, RuleOrchestrator>();

            services.AddSingleton<IMailClient, MailClient>();

            services.AddScoped<ISlipGenerator<OrderInfo, PackagingSlip>, SlipGenerator>();

            services.AddScoped<BookRulesHandler>();
            services.AddScoped<ProductRuleHandler>();
            services.AddScoped<MembershipRulesHandler>();
            services.AddScoped<VideoRulesHandler>();

            services.AddScoped<Func<RuleType, IRuleEvaluator<OrderInfo, RuleResult>>>(sp => res =>
            {
                switch (res)
                {
                    case RuleType.Product:
                        return sp.GetService<ProductRuleHandler>();
                    case RuleType.Book:
                        return sp.GetService<BookRulesHandler>();
                    case RuleType.MembershipActivation:
                        return sp.GetService<MembershipRulesHandler>();
                    case RuleType.MemeberShipUpgrade:
                        return sp.GetService<MembershipRulesHandler>();
                    case RuleType.Video:
                        return sp.GetService<VideoRulesHandler>();
                    default:
                        throw new NotSupportedException();
                }
            });
        }
    }
}
