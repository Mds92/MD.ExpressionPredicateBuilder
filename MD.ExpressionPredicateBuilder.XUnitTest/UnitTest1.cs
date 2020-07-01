using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using MD.ExpressionPredicateBuilder.Predicate;
using MD.ExpressionPredicateBuilder.XUnitTest.Models;

namespace MD.ExpressionPredicateBuilder.XUnitTest
{
    public class UnitTestEnumerable
    {
        private readonly List<Entity1> _entity1List = new List<Entity1>();
        private readonly ulong _ulongObjectToCompare;
        private readonly string _stringObjectToCompare;
        private readonly DateTime _dateTimeNow;
        private readonly PersianDateTime.Standard.PersianDateTime _persianDateTime;
        public UnitTestEnumerable()
        {
            _ulongObjectToCompare = 99;
            _stringObjectToCompare = _ulongObjectToCompare.ToString();
            _dateTimeNow = DateTime.Now;
            _persianDateTime = PersianDateTime.Standard.PersianDateTime.Now;
            FillDummyData();
        }

        private void FillDummyData()
        {
            for (var i = 0; i < 100; i++)
            {
                _entity1List.Add(new Entity1
                {
                    Id = (ulong)i,
                    IsActive = i % 10 != 0,
                    LastName = $"{i}LastName",
                    Name = $"{i}Name",
                    RegisterDateTime = _dateTimeNow.AddSeconds(i),
                    RegisterDateTimeNullable = i % 5 == 0 ? (DateTime?)null : _dateTimeNow.AddSeconds(i),
                    RegisterPersianDateTimeNullable = new PersianDateTime.Standard.PersianDateTime(_dateTimeNow.addmi(i))
                });
            }
        }

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
                .And(q => q.RegisterPersianDateTimeNullable, OperatorEnum.Equal, _stringObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.Name == _stringObjectToCompare).ToList();
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

        #region Contain

        [Fact]
        public void Test_Contain_String()
        {
            var criteria = Criteria<Entity1>.True()
                .And(q => q.LastName, OperatorEnum.Contain, _stringObjectToCompare);
            var mdExpressionPredicateBuilderResult = _entity1List.Where(criteria.GetExpression().Compile()).ToList();
            var linqResult = _entity1List.Where(q => q.LastName.Contains(_stringObjectToCompare)).ToList();
            Assert.True(linqResult.Count == mdExpressionPredicateBuilderResult.Count);
        }

        #endregion


    }
}
