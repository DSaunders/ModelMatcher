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
                Type = ConditionType.Match,
                PropertyName = member.Member.Name
            };
        }
    }
}