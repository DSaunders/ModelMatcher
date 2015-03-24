namespace ModelMatcher.Extensions
{
    using System.Collections.Generic;
    using Enums;
    using Exceptions;
    using Matcher;

    public static class ModelMatcherExtensions
    {
        public static void ShouldMatchNonDefaultProperties<T>(this T itemUnderTest, T expected)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected, MatchMode.IgnoreDefaultProperties);

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
            ShouldContainMatch(list, expectedItem, MatchMode.Strict);
        }

        public static void ShouldContainAnItemMatchingNonDefaultPropertiesOf<T>(this IEnumerable<T> list, T expectedItem)
        {
            ShouldContainMatch(list, expectedItem, MatchMode.IgnoreDefaultProperties);
        }


        private static void ShouldContainMatch<T>(IEnumerable<T> list, T expectedItem, MatchMode mode)
        {
            var matchFound = false;

            foreach (var item in list)
            {
                if (SingleItemMatcher.MatchSingleItem(item, expectedItem, mode).Matches)
                    matchFound = true;
            }

            if (!matchFound)
                throw new NoMatchingItemInCollection();
        }

    }
}
