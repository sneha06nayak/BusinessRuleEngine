using RulesEngine.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Common.Interface
{
    /// <summary>
    /// Rule Evaluator.
    /// </summary>
    /// <typeparam name="T">Rule Request.</typeparam>
    public interface IRuleEvaluator<T1,T2> where T1 : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleRequest"></param>
        /// <returns></returns>
        Task<T2> ExecuteAsync(T1 ruleRequest);
    }
}
