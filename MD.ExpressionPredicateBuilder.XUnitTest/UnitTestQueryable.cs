using System;
using System.Linq;
using Xunit;
using MD.ExpressionPredicateBuilder.Predicate;
using MD.ExpressionPredicateBuilder.XUnitTest.DbContext;
using MD.ExpressionPredicateBuilder.XUnitTest.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MD.ExpressionPredicateBuilder.XUnitTest
{
    public class UnitTestQueryable
    {
        #region Initilize

        private readonly DbContextTest _dbContextTest;
        public UnitTestQueryable()
        {
            _dbContextTest = new DbContextTest(new DbContextOptionsBuilder<DbContextTest>().UseSqlServer(DbContextTest.ConnectionString).Options);
        }

        #endregion

        [Fact]
        public void Test_OnDateTime()
        {
            var criteria = Criteria<CrmServiceLayerHistory>.True()
                .And(q => q.RequestDateTime, OperatorEnum.GreaterThan, DateTime.Now.Date)
                .And(q => q.ResponseStatusCode, OperatorEnum.Equal, 200);
            var mdExpressionPredicateBuilderResult = _dbContextTest.CrmServiceLayerHistory.Where(criteria.GetExpression()).ToList();
            var linqResult = _dbContextTest.CrmServiceLayerHistory
                .Where(q => q.RequestDateTime > DateTime.Now.Date && q.ResponseStatusCode == 200)
                .ToList();
            Assert.True(mdExpressionPredicateBuilderResult.Count == linqResult.Count);
        }

        [Fact]
        public void Test_NestedConditions()
        {
            var criteria1 = Criteria<CrmServiceLayerHistory>.True()
                .And(q => q.RequestDateTime, OperatorEnum.GreaterThan, DateTime.Now.Date)
                .And(q => q.ResponseStatusCode, OperatorEnum.Equal, 200);
            var criteria2 = Criteria<CrmServiceLayerHistory>.True()
                .And(q => q.CreatedBy, OperatorEnum.StartsWith, "B2B")
                .And(q => q.ClientIP, OperatorEnum.Equal, "10.104.52.47");
            var criteria3 = Criteria<CrmServiceLayerHistory>.False().Or(criteria1).Or(criteria2);
            var mdExpressionPredicateBuilderResult = _dbContextTest.CrmServiceLayerHistory.Where(criteria3.GetExpression()).ToList();
            var linqResult = _dbContextTest.CrmServiceLayerHistory
                .Where(q =>
                    (q.RequestDateTime > DateTime.Now.Date && q.ResponseStatusCode == 200)
                        ||
                    (q.CreatedBy.StartsWith("B2B") && q.ClientIP == "10.104.52.47")
                    )
                .ToList();
            Assert.True(mdExpressionPredicateBuilderResult.Count == linqResult.Count);
        }

        [Fact]
        public void Test_ConvertToTsql()
        {
            var criteria1 = Criteria<CrmServiceLayerHistory>.True()
                .And(q => q.RequestDateTime, OperatorEnum.GreaterThan, DateTime.Now.Date)
                .And(q => q.ResponseStatusCode, OperatorEnum.Equal, 200);
            var criteria2 = Criteria<CrmServiceLayerHistory>.True()
                .And(q => q.CreatedBy, OperatorEnum.StartsWith, "B2B")
                .And(q => q.ClientIP, OperatorEnum.Equal, "10.104.52.47");
            var criteria3 = Criteria<CrmServiceLayerHistory>.False().Or(criteria1).Or(criteria2);
            var mdExpressionPredicateBuilderResult = _dbContextTest.CrmServiceLayerHistory.Where(criteria3.GetExpression()).ToList();
            var linqResult = _dbContextTest.CrmServiceLayerHistory
                .Where(q =>
                    (q.RequestDateTime > DateTime.Now.Date && q.ResponseStatusCode == 200)
                    ||
                    (q.CreatedBy.StartsWith("B2B") && q.ClientIP == "10.104.52.47")
                )
                .ToList();
            Assert.True(mdExpressionPredicateBuilderResult.Count == linqResult.Count);
        }

        [Fact]
        public void Test_Serialize()
        {
            // Client Side
            var criteria1 = Criteria<CrmServiceLayerHistory>.True()
                .And(q => q.RequestDateTime, OperatorEnum.GreaterThan, DateTime.Now.Date)
                .And(q => q.ResponseStatusCode, OperatorEnum.Equal, 200);
            var criteria2 = Criteria<CrmServiceLayerHistory>.True()
                .And(q => q.CreatedBy, OperatorEnum.StartsWith, "B2B")
                .And(q => q.ClientIP, OperatorEnum.Equal, "10.104.52.47");
            var criteria3 = Criteria<CrmServiceLayerHistory>.False().Or(criteria1).Or(criteria2);

            var servicePredicateBuilderClientSide = new ServicePredicateBuilder<CrmServiceLayerHistory>
            {
                Criteria = criteria3,
                SortCondition = SortCondition<CrmServiceLayerHistory>.OrderByDescending(q => q.ResponseDateTime)
            };
            var serializedString = JsonConvert.SerializeObject(servicePredicateBuilderClientSide);

            // Service Side
            var servicePredicateBuilderServiceSide = JsonConvert.DeserializeObject<ServicePredicateBuilder<CrmServiceLayerHistory>>(serializedString);
            var mdExpressionPredicateBuilderResult = _dbContextTest.CrmServiceLayerHistory
                .Where(servicePredicateBuilderServiceSide.Criteria.GetExpression())
                .ToList();

            var linqResult = _dbContextTest.CrmServiceLayerHistory
                .Where(q =>
                    q.RequestDateTime > DateTime.Now.Date && q.ResponseStatusCode == 200
                    ||
                    q.CreatedBy.StartsWith("B2B") && q.ClientIP == "10.104.52.47"
                )
                .ToList();
            Assert.True(mdExpressionPredicateBuilderResult.Count == linqResult.Count);
        }
    }
}
