namespace ModelMatcher.Assertions
{
    using System.Collections.Generic;

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