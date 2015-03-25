namespace ModelMatcher.Assertions.Conditions
{
    using System;
    using System.Linq.Expressions;

    public static class Ignore
    {
        public static Condition This<U>(Expression<Func<U>> expression)
        {
            var member = expression.Body as MemberExpression;
            return new Condition
            {
                Type = MatchCondition.Ignore,
                PropertyName = member.Member.Name
            };
        }
    }
}

