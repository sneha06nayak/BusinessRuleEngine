using System;
using System.Collections.Generic;
using System.Text;

namespace RulesEngine.Common.Model
{
    public class RuleResult
    {
        public List<PackagingSlip> PackagingSlip {get; set;}

        public string Message { get; set; }
    }
}
