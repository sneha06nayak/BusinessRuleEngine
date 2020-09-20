using RulesEngine.Common.Enums;
using RulesEngine.Common.Interface;
using RulesEngine.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesEngine.Service.Utilities
{
    public class SlipGenerator : ISlipGenerator<OrderInfo, PackagingSlip>
    {
        /// <inheritdoc/>
        public async Task<PackagingSlip> GenerateSlip(OrderInfo orderInfo, Department department = Department.Shipping)
        {
            return await Task.FromResult(new PackagingSlip
            {
                CustomerName = $"{orderInfo.FirstName} {orderInfo.LastName}",
                CustomerAddress = orderInfo.Address,
                InvoiceDate = DateTime.UtcNow,
                ProductName = string.Join(',', orderInfo.ProductInfo.Select(p => p.Name)),
                InvoicePrice = orderInfo.ProductInfo.Select(p => p.Price).ToList().Sum(),
                Department = department.ToString()
            });
        }
    }
}
