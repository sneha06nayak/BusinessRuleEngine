using FakeItEasy;
using Newtonsoft.Json;
using RulesEngine.Common.Interface;
using RulesEngine.Common.Model;
using RulesEngine.Service;
using RulesEngine.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessRulesEngine.Tests
{
    public class SlipGeneratorTests
    {
        private readonly SlipGenerator _slipGenerator;
        public SlipGeneratorTests()
        {
            _slipGenerator = new SlipGenerator();
        }

        [Fact]
        public async Task GenerateSlip_NoDepartment_ReturnsShippingPackageSlip()
        {
            var shippingPackagingSlip = new PackagingSlip
            {
                CustomerAddress = "Bangalore",
                CustomerName = "John Carter",
                Department = "Shipping",
                InvoicePrice = 79,
                ProductName = "Sydney Shedon"
            };

            //Act
            var result = await _slipGenerator.GenerateSlip(new OrderInfo
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
            shippingPackagingSlip.InvoiceDate = result.InvoiceDate;
            Assert.Equal(JsonConvert.SerializeObject(shippingPackagingSlip), JsonConvert.SerializeObject(result));
        }
        [Fact]
        public async Task GenerateSlip_CommissionDepartment_ReturnsCommissionPackageSlip()
        {
            var shippingPackagingSlip = new PackagingSlip
            {
                CustomerAddress = "Bangalore",
                CustomerName = "John Carter",
                Department = "Commission",
                InvoicePrice = 79,
                ProductName = "Sydney Shedon"
            };

            //Act
            var result = await _slipGenerator.GenerateSlip(new OrderInfo
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
            }, RulesEngine.Common.Enums.Department.Commission);

            //Assert
            shippingPackagingSlip.InvoiceDate = result.InvoiceDate;
            Assert.Equal(JsonConvert.SerializeObject(shippingPackagingSlip), JsonConvert.SerializeObject(result));
        }
        [Fact]
        public async Task GenerateSlip_EmptyOrderInfo_ReturnsCommissionPackageSlip()
        {
            var shippingPackagingSlip = new PackagingSlip
            {
                CustomerAddress = "Bangalore",
                CustomerName = "John Carter",
                Department = "Commission",
                InvoicePrice = 79,
                ProductName = "Sydney Shedon"
            };

            //Act
            var result = await _slipGenerator.GenerateSlip(new OrderInfo
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
            }, RulesEngine.Common.Enums.Department.Commission);

            //Assert
            shippingPackagingSlip.InvoiceDate = result.InvoiceDate;
            Assert.Equal(JsonConvert.SerializeObject(shippingPackagingSlip), JsonConvert.SerializeObject(result));
        }
    }
}
