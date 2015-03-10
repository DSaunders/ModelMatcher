namespace ModelMatcher.Extensions
{
    using Enums;
    using Exceptions;
    using Matcher;

    public static class ModelMatcherExtensions
    {
        public static void ShouldMatchNonDefaultFields<T>(this T itemUnderTest, T expected)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected, MatchMode.IgnoreDefaultPropertiesInExpectedModel);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }

        public static void ShouldMatch<T>(this T itemUnderTest, T expected)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected, MatchMode.Strict);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }

        
    }
}
