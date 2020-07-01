using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MD.ExpressionPredicateBuilder.Predicate;

namespace MD.ExpressionPredicateBuilder.Helpers
{
    /// <summary>
    /// کلاسی برای تبدیل از
    /// ServicePredicateBuilder
    /// به این کلاس و استفاده از این کلاس در لایه های پایین  تری
    /// </summary>
    public class ExpressionInfo<TEntity> where TEntity : class
    {
        public ExpressionInfo()
        {
            IncludedNavigationProperties = new List<string>();
        }

        public Expression<Func<TEntity, bool>> Expression;
        public SortCondition<TEntity> SortCondition { get; set; }
        public List<string> IncludedNavigationProperties { get; set; }
        public PaginationData PaginationData { get; set; }

        public void AddIncludedNavigationProperty(Expression<Func<TEntity, object>> expression)
        {
            if (expression == null) return;
            IncludedNavigationProperties ??= new List<string>();
            var selectorString = expression.Body.ToString();
            IncludedNavigationProperties.Add(selectorString.Remove(0, selectorString.IndexOf('.') + 1));
        }
        public void AddRangeIncludedNavigationProperty(List<Expression<Func<TEntity, object>>> expressions)
        {
            if (expressions == null || !expressions.Any()) return;
            IncludedNavigationProperties ??= new List<string>();
            foreach (var selectorString in expressions.Select(expression => expression.Body.ToString()))
                IncludedNavigationProperties.Add(selectorString.Remove(0, selectorString.IndexOf('.') + 1));
        }
        public void RemoveIncludedNavigationProperty(Expression<Func<TEntity, object>> expression)
        {
            if (expression == null) return;
            if (IncludedNavigationProperties == null || !IncludedNavigationProperties.Any()) return;
            var selectorString = expression.Body.ToString();
            IncludedNavigationProperties.Remove(selectorString.Remove(0, selectorString.IndexOf('.') + 1));
        }

        public void NullEverythingExceptCriteria()
        {
            IncludedNavigationProperties = null;
            PaginationData = null;
            SortCondition = null;
        }
        public void NullNavigationProperties()
        {
            IncludedNavigationProperties = null;
        }
    }
}