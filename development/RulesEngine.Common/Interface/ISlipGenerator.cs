using RulesEngine.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Common.Interface
{
    public interface ISlipGenerator<T1,T2> where T1 : class,new()
    {
        /// <summary>
        /// Slip Generatir Interface.
        /// </summary>
        /// <param name="input"> Input.</param>
        /// <param name="department">Department for which skip needs to be generated.</param>
        /// <returns></returns>
        Task<T2> GenerateSlip(T1 input, Department department= Department.Shipping);
    }
}
