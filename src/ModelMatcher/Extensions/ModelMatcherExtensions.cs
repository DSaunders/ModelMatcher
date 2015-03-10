namespace ModelMatcher.Extensions
{
    using System.Collections.Generic;
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

        public static void ShouldContainAnItemMatching<T>(this IEnumerable<T> list, T expectedItem)
        {
            var matchFound = false;
            
            foreach (var item in list)
            {
                if (SingleItemMatcher.MatchSingleItem(item, expectedItem, MatchMode.Strict).Matches)
                    matchFound = true;
            }

            if (!matchFound)
                throw new NoMatchingItemInCollection();
        }
        
    }
}
