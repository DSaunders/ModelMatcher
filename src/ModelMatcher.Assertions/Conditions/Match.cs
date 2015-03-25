namespace ModelMatcher.Assertions.Conditions
{
    using System;
    using System.Linq.Expressions;
    using Exceptions;

    public static class Match
    {
        public static Condition This<T>(Expression<Func<T>> expression)
        {
            return GetConditionForExpressionAndType(expression, MatchCondition.Match);
        }

        public static Condition IgnoringCase(Expression<Func<string>> expression)
        {
            return GetConditionForExpressionAndType(expression, MatchCondition.IgnoreCase);
        }

        public static Condition IfNotNull(Expression<Func<object>> expression)
        {
            return GetConditionForExpressionAndType(expression, MatchCondition.IfNotNull);
        }

        private static Condition GetConditionForExpressionAndType<T>(Expression<Func<T>> expression, MatchCondition conditionType)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new InvalidMatchExpression();

            return new Condition
            {
                Type = conditionType,
                PropertyName = member.Member.Name
            };
        }
    }
}