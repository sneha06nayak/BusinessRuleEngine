using FakeItEasy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RulesEngine.Common.Enums;
using RulesEngine.Common.Interface;
using RulesEngine.Common.Model;
using RulesEngine.Service.RuleExecutors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessRulesEngine.Tests
{
    public class BookRulesHandlerTests
    {
        private readonly ILogger<BookRulesHandler> _bookRuleslogger;
        private readonly ISlipGenerator<OrderInfo, PackagingSlip> _slipGenerator;
        private readonly IRuleEvaluator<OrderInfo, RuleResult> _bookRulesHandler;

        public BookRulesHandlerTests()
        {
            _bookRuleslogger = A.Fake<ILogger<BookRulesHandler>>();
            _slipGenerator = A.Fake<ISlipGenerator<OrderInfo, PackagingSlip>>();
            _bookRulesHandler = new BookRulesHandler(_bookRuleslogger, _slipGenerator);
        }

        [Fact]
        public async Task ExecuteAsync_ReturnsPackageSlip()
        {
            //Arrange
            var commissionPackagingSlip = new PackagingSlip
            {
                CustomerAddress = "Bangalore",
                CustomerName = "John Carter",
                Department="Commission",
                InvoicePrice=79,
                ProductName="Sydney Shedon"
            };
            var shippingPackagingSlip = new PackagingSlip
            {
                CustomerAddress = "Bangalore",
                CustomerName = "John Carter",
                Department = "Commission",
                InvoicePrice = 79,
                ProductName = "Sydney Shedon"
            };
            var royaltyPackagingSlip = new PackagingSlip
            {
                CustomerAddress = "Bangalore",
                CustomerName = "John Carter",
                Department = "Commission",
                InvoicePrice = 79,
                ProductName = "Sydney Shedon"
            };
            A.CallTo(() => _slipGenerator.GenerateSlip(A<OrderInfo>._, Department.Commission)).Returns<PackagingSlip>(commissionPackagingSlip);
            A.CallTo(() => _slipGenerator.GenerateSlip(A<OrderInfo>._, Department.Shipping)).Returns<PackagingSlip>(shippingPackagingSlip);
            A.CallTo(() => _slipGenerator.GenerateSlip(A<OrderInfo>._, Department.Royalty)).Returns<PackagingSlip>(royaltyPackagingSlip);

            //Act
            var result = await _bookRulesHandler.ExecuteAsync(new OrderInfo
            {
                Address = "Bangalore",
                FirstName = "John",
                LastName = "Carter",
                Email = "john.carter@gmail.com",
                ProductInfo = new List<Product> { new Product
                 {
                     Id = 1,
                     Name = "Sydney Shedon",
                     ProductType=RuleType.Book,
                     Price = 79
                 } }
            });

            //Assert
            var expectedPackageSlips = new List<PackagingSlip> { commissionPackagingSlip, shippingPackagingSlip, royaltyPackagingSlip };
            Assert.Equal(3, result.PackagingSlip.Count);
            Assert.Equal(JsonConvert.SerializeObject(expectedPackageSlips), JsonConvert.SerializeObject(result.PackagingSlip));
        }
    }
}
