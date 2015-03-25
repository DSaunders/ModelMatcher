namespace ModelMatcher.Assertions.Extensions
{
    using System.Collections.Generic;
    using Conditions;
    using Exceptions;
    using Matchers;

    public static class SingleItemMatchextensions
    {
        public static void ShouldMatch<T>(this T itemUnderTest, T expected)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected, MatchCondition.Match);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }

        public static void ShouldMatch<T>(this T itemUnderTest, T expected, IEnumerable<Condition> conditions)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected, MatchCondition.Match,
                conditions);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }


        public static void ShouldMatchNonDefaultProperties<T>(this T itemUnderTest, T expected)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected,
                MatchCondition.IgnoreIfDefaultInExpectedModel);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }

        public static void ShouldMatchNonDefaultProperties<T>(this T itemUnderTest, T expected, IEnumerable<Condition> conditions)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected,
                MatchCondition.IgnoreIfDefaultInExpectedModel, conditions);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }
    }
}