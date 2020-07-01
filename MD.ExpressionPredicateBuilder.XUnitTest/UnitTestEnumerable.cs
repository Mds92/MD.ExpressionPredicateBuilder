using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MD.ExpressionPredicateBuilder.Predicate;
using MD.ExpressionPredicateBuilder.XUnitTest.Models;

namespace MD.ExpressionPredicateBuilder.XUnitTest
{
    public class UnitTestEnumerable
    {
        #region Initilize

        private readonly ulong _ulongObjectToCompare;
        private readonly string _stringObjectToCompare;
        private readonly DateTime _dateTimeNow;
        private readonly PersianDateTime.Standard.PersianDateTime _persianDateTime;
        private readonly List<Entity1> _entity1List = new List<Entity1>();
        private readonly List<string> _stringListObjectToCompare = new List<string>();
        private readonly List<long> _numericListObjectToCompare = new List<long>();
        public UnitTestEnumerable()
        {
            _ulongObjectToCompare = 90;
            _stringObjectToCompare = _ulongObjectToCompare.ToString();
            _dateTimeNow = DateTime.Now;
            _persianDateTime = PersianDateTime.Standard.PersianDateTime.Now;

            FillDummyData();
        }

        private void FillDummyData()
        {
            for (var i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    _stringListObjectToCompare.Add(i.ToString());
                    _numericListObjectToCompare.Add(i);
                }
                _entity1List.Add(new Entity1
                {
                    Id = (ulong)i,
                    Name = $"{i}Name",
                    IsActive = i % 10 != 0,
                    LastName = $"{i}LastName",
                    RegisterDateTime = _dateTimeNow.AddMinutes(i),
                    RegisterDateTimeNullable = i % 5 == 0 ? (DateTime?)null : _dateTimeNow.AddMinutes(i),
                    RegisterPersianDateTime = new PersianDateTime.Standard.PersianDateTime(_dateTimeNow.AddMinutes(i))
                });
            }
        }

        #endregion

        #region Like

        [Fact]
        public void Test_Like_Number()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.Like, _ulongObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.Id.ToString().Contains(_stringObjectToCompare)).ToList();
            Assert.True(mdExpressionPredicateBuilderResult.Count == linqResult.Count);
        }

        [Fact]
        public void Test_NotLike_Number()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.NotLike, _ulongObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => !q.Id.ToString().Contains(_stringObjectToCompare)).ToList();
            Assert.True(mdExpressionPredicateBuilderResult.Count == linqResult.Count);
        }

        [Fact]
        public void Test_Like_String()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Name, OperatorEnum.Like, _stringObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(q => q.Name.Contains(_stringObjectToCompare)).ToList();
            var linqResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_NotLike_String()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Name, OperatorEnum.NotLike, _stringObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(q => !q.Name.Contains(_stringObjectToCompare)).ToList();
            var linqResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        #endregion

        #region StartsWith

        [Fact]
        public void Test_StartsWith_Number()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.StartsWith, _ulongObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.Id.ToString().StartsWith(_stringObjectToCompare)).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_StartsWith_String()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Name, OperatorEnum.StartsWith, _stringObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.Name.StartsWith(_stringObjectToCompare)).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_NotStartsWith_String()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Name, OperatorEnum.NotStartsWith, _stringObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => !q.Name.StartsWith(_stringObjectToCompare)).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_NotStartsWith_Number()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.NotStartsWith, _ulongObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => !q.Id.ToString().StartsWith(_stringObjectToCompare)).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        #endregion

        #region Equal

        [Fact]
        public void Test_Equal_Number()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.Equal, _ulongObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.Id.ToString() == _stringObjectToCompare).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_Equal_String()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.Equal, _ulongObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.Id.ToString() == _stringObjectToCompare).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_Equal_DateTimeNullable()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.RegisterDateTimeNullable, OperatorEnum.Equal, null);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.RegisterDateTimeNullable == null).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_Equal_DateTime()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Name, OperatorEnum.Equal, _stringObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.Name == _stringObjectToCompare).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_Equal_PersianDateTime()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.RegisterPersianDateTime, OperatorEnum.Equal, _persianDateTime);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.RegisterPersianDateTime == _persianDateTime).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_NotEqual_DateTime()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.RegisterDateTime, OperatorEnum.NotEqual, _dateTimeNow);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.RegisterDateTime != _dateTimeNow).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_NotEqual_Number()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.NotEqual, _ulongObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.Id.ToString() != _stringObjectToCompare).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        #endregion

        #region Greater

        [Fact]
        public void Test_GreaterThan_DateTime()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.RegisterDateTime, OperatorEnum.GreaterThan, _dateTimeNow);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.RegisterDateTime > _dateTimeNow).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_GreaterThanOrEqual_DateTime()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.RegisterDateTime, OperatorEnum.GreaterThanOrEqual, _dateTimeNow);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.RegisterDateTime >= _dateTimeNow).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_GreaterThanOrEqual_PersianDateTime()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.RegisterPersianDateTime, OperatorEnum.GreaterThanOrEqual, _persianDateTime);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.RegisterPersianDateTime >= _persianDateTime).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_LessThanOrEqual_DateTime()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.RegisterDateTime, OperatorEnum.LessThanOrEqual, _dateTimeNow);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.RegisterDateTime <= _dateTimeNow).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_LessThan_DateTime()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.RegisterDateTime, OperatorEnum.LessThan, _dateTimeNow);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.RegisterDateTime < _dateTimeNow).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        #endregion

        #region Contain

        [Fact]
        public void Test_Contain_StringList()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.Contain, _stringListObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => _stringListObjectToCompare.Contains(q.Id.ToString())).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_Contain_NumericList()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.Contain, _numericListObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => _numericListObjectToCompare.Contains((long)q.Id)).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        [Fact]
        public void Test_NotContain_NumericList()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.NotContain, _numericListObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => !_numericListObjectToCompare.Contains((long)q.Id)).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        #endregion

        #region Nested Conditions

        [Fact]
        public void Test_NestedConditions()
        {
            var criteria1 = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.GreaterThan, 10)
                .And(q => q.RegisterDateTime, OperatorEnum.GreaterThan, DateTime.Now.AddMinutes(1));
            var criteria2 = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.StartsWith, 5)
                .And(q => q.IsActive, OperatorEnum.Equal, true);
            var criteria3 = Criteria<Entity1>.False().Or(criteria1).Or(criteria2);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria3.GetExpression().Compile()).ToList();
            var linqResult = _entity1List
                .Where(q =>
                    (q.Id > 10 && q.RegisterDateTime > DateTime.Now.AddMinutes(1))
                    ||
                    (q.Id.ToString().StartsWith("5") && q.IsActive)
                )
                .ToList();
            Assert.True(mdExpressionPredicateBuilderResult.Count == linqResult.Count);
        }

        #endregion

        #region Performance

        [Fact]
        public void Test_ConcurrentPerformance()
        {
            var criteria1 = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.GreaterThan, 10)
                .And(q => q.RegisterDateTime, OperatorEnum.GreaterThan, DateTime.Now.AddMinutes(1));
            var criteria2 = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.StartsWith, 5)
                .And(q => q.IsActive, OperatorEnum.Equal, true);
            var criteria3 = Criteria<Entity1>.False().Or(criteria1).Or(criteria2);
            var stopwatch = Stopwatch.StartNew();
            var taskList = new List<Task>();
            for (var i = 0; i < 10_000; i++)
            {
                taskList.Add(Task.Run(() => {  criteria3.GetExpression(); }));
            }
            Task.WhenAll(taskList).Wait();
            stopwatch.Stop();
            Assert.True(stopwatch.Elapsed <= TimeSpan.FromMilliseconds(1000));
        }

        [Fact]
        public void Test_Performance()
        {
            var criteria1 = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.GreaterThan, 10)
                .And(q => q.RegisterDateTime, OperatorEnum.GreaterThan, DateTime.Now.AddMinutes(1));
            var criteria2 = Criteria<Entity1>.True()
                .And(q => q.Id, OperatorEnum.StartsWith, 5)
                .And(q => q.IsActive, OperatorEnum.Equal, true);
            var criteria3 = Criteria<Entity1>.False().Or(criteria1).Or(criteria2);
            var stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < 100_000; i++)
                criteria3.GetExpression();
            stopwatch.Stop();
            Assert.True(stopwatch.Elapsed <= TimeSpan.FromMilliseconds(3000));
        }

        #endregion
    }
}
