namespace ModelMatcher.Conditions
{
    using System;
    using System.Linq.Expressions;

    public static class Match
    {
        public static Condition This<U>(Expression<Func<U>> expression)
        {
            var member = expression.Body as MemberExpression;
            return new Condition
            {
                Type = MatchCondition.Match,
                PropertyName = member.Member.Name
            };
        }

        public static Condition IgnoringCase(Expression<Func<string>> expression)
        {
            var member = expression.Body as MemberExpression;
            return new Condition
            {
                Type = MatchCondition.IgnoreCase,
                PropertyName = member.Member.Name
            };
        }

        public static Condition IfNotNull(Expression<Func<object>> expression)
        {
            var member = expression.Body as MemberExpression;
            return new Condition
            {
                Type = MatchCondition.IfNotNull,
                PropertyName = member.Member.Name
            };
        }
    }
}