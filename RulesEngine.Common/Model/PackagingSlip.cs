using RulesEngine.Common.Enums;
using System;

namespace RulesEngine.Common.Model
{
    public class PackagingSlip
    {
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string ProductName { get; set; }
        public double InvoicePrice { get; set; }
        public string Department { get; set; }
        public string CustomerAddress { get; set; }
    }
}