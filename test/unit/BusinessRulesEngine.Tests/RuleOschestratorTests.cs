using FakeItEasy;
using Microsoft.Extensions.Logging;
using RulesEngine.Common.Enums;
using RulesEngine.Common.Interface;
using RulesEngine.Common.Model;
using RulesEngine.Service;
using RulesEngine.Service.RuleExecutors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessRulesEngine.Tests
{
    public class RuleOschestratorTests
    {
        private readonly Func<RuleType, IRuleEvaluator<OrderInfo, RuleResult>> _ruleFactory;
        private readonly ILogger<BookRulesHandler> _bookRuleslogger;
        private readonly ILogger<ProductRuleHandler> _productRuleslogger;
        private readonly ILogger<RuleOrchestrator> _logger;
        private readonly ISlipGenerator<OrderInfo, PackagingSlip> _slipGenerator;
        private readonly RuleOrchestrator _ruleOrchestrator;
        public RuleOschestratorTests()
        {
            _ruleFactory = A.Fake<Func<RuleType, IRuleEvaluator<OrderInfo, RuleResult>>>();
            _bookRuleslogger = A.Fake<ILogger<BookRulesHandler>>();
            _productRuleslogger = A.Fake<ILogger<ProductRuleHandler>>();
            _logger = A.Fake<ILogger<RuleOrchestrator>>();
            _slipGenerator = A.Fake<ISlipGenerator<OrderInfo, PackagingSlip>>();
            _ruleOrchestrator = new RuleOrchestrator(_ruleFactory, _logger);
        }

        [Fact]
        public async Task ExecuteAsync_Book_ReturnsThreePackageSlip()
        {
            //Arrange
            A.CallTo(() => _ruleFactory(RuleType.Book)).Returns<IRuleEvaluator<OrderInfo, RuleResult>>(new BookRulesHandler(_bookRuleslogger,_slipGenerator));

            //Act
            var result = await _ruleOrchestrator.ExecuteAsync(new RuleRequest 
            { 
             OrderInfo = new OrderInfo
             {
                 Address = "Bangalore",
                 FirstName="John",
                 LastName ="Carter",
                 Email="john.carter@gmail.com",
                 ProductInfo = new List<Product> { new Product
                 {
                     Id = 1,
                     Name = "Sidney Sheldon",
                     ProductType=RuleType.Book,
                     Price = 79
                 } }
             }
            });

            //Assert
            Assert.Equal(3, result.SelectMany(x => x.PackagingSlip).ToList().Count);
        }

        [Fact]
        public async Task ExecuteAsync_2Books_ReturnsThreePackageSlip()
        {
            //Arrange
            A.CallTo(() => _ruleFactory(RuleType.Book)).Returns<IRuleEvaluator<OrderInfo, RuleResult>>(new BookRulesHandler(_bookRuleslogger, _slipGenerator));

            //Act
            var result = await _ruleOrchestrator.ExecuteAsync(new RuleRequest
            {
                OrderInfo = new OrderInfo
                {
                    Address = "Bangalore",
                    FirstName = "John",
                    LastName = "Carter",
                    Email = "john.carter@gmail.com",
                    ProductInfo = new List<Product> { new Product
                 {
                     Id = 1,
                     Name = "Sidney Sheldon",
                     ProductType=RuleType.Book,
                     Price = 79
                 },
                 new Product{
                     Id = 2,
                     Name = "A Journey called life",
                     ProductType=RuleType.Book,
                     Price = 50
                 }}
                }
            });

            //Assert
            Assert.Equal(3, result.SelectMany(x => x.PackagingSlip).ToList().Count);
        }

        [Fact]
        public async Task ExecuteAsync_BookAndProduct_Returns5PackageSlip()
        {
            //Arrange
            A.CallTo(() => _ruleFactory(RuleType.Book)).Returns<IRuleEvaluator<OrderInfo, RuleResult>>(new BookRulesHandler(_bookRuleslogger, _slipGenerator));
            A.CallTo(() => _ruleFactory(RuleType.Product)).Returns<IRuleEvaluator<OrderInfo, RuleResult>>(new ProductRuleHandler(_productRuleslogger, _slipGenerator));

            //Act
            var result = await _ruleOrchestrator.ExecuteAsync(new RuleRequest
            {
                OrderInfo = new OrderInfo
                {
                    Address = "Bangalore",
                    FirstName = "John",
                    LastName = "Carter",
                    Email = "john.carter@gmail.com",
                    ProductInfo = new List<Product> { new Product
                    {
                        Id = 1,
                        Name = "Sidney Sheldon",
                        ProductType=RuleType.Book,
                        Price = 79
                    }, new Product{
                        Id = 1,
                        Name = "Cycle",
                        ProductType=RuleType.Product,
                        Price = 1000
                    } 
                    }
                }
            });

            //Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.SelectMany(x => x.PackagingSlip).ToList().Count);
        }
        [Fact]
        public async Task ExecuteAsync_NullRuleEvaluator_ReturnsError()
        {
            //Arrange
            A.CallTo(() => _ruleFactory(RuleType.Book)).Returns<IRuleEvaluator<OrderInfo, RuleResult>>(null);
            A.CallTo(() => _ruleFactory(RuleType.Product)).Returns<IRuleEvaluator<OrderInfo, RuleResult>>(new ProductRuleHandler(_productRuleslogger, _slipGenerator));

            //Act
            var result = await _ruleOrchestrator.ExecuteAsync(new RuleRequest
            {
                OrderInfo = new OrderInfo
                {
                    Address = "Bangalore",
                    FirstName = "John",
                    LastName = "Carter",
                    Email = "john.carter@gmail.com",
                    ProductInfo = new List<Product> { new Product
                    {
                        Id = 1,
                        Name = "Sidney Sheldon",
                        ProductType=RuleType.Book,
                        Price = 79
                    }, new Product{
                        Id = 1,
                        Name = "Cycle",
                        ProductType=RuleType.Product,
                        Price = 1000
                    }
                    }
                }
            });

            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.SelectMany(x => x.PackagingSlip).ToList().Count);
        }
    }
}
