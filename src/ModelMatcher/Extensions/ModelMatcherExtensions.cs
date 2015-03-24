namespace ModelMatcher.Extensions
{
    using System;
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

        public static void ShouldContainAMatch<T>(this IEnumerable<T> list, T expectedItem)
        {
            ShouldContainMatch(list, expectedItem, MatchMode.Strict, 1);
        }

        public static void ShouldContainAMatchOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem)
        {
            ShouldContainMatch(list, expectedItem, MatchMode.IgnoreDefaultProperties, 1);
        }

        public static void ShouldContainMatches<T>(this IEnumerable<T> list, T expectedItem, int numberOfMatches)
        {
            ShouldContainMatch(list, expectedItem, MatchMode.Strict, numberOfMatches);
        }

        public static void ShouldContainMatchesOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem, int numberOfMatches)
        {
            ShouldContainMatch(list, expectedItem, MatchMode.IgnoreDefaultProperties, numberOfMatches);
        }
        
        private static void ShouldContainMatch<T>(IEnumerable<T> list, T expectedItem, MatchMode mode, int requiredMatches)
        {
            var matches = 0;

            foreach (var item in list)
            {
                if (SingleItemMatcher.MatchSingleItem(item, expectedItem, mode).Matches)
                    matches++;
            }

            if (requiredMatches == matches)
                return;

            if (requiredMatches == 1)
                throw new CollectionDoesNotMatch("Could not find a matching item in the collection");

            var message = string.Format("Expected {0} matching items but found {1}", requiredMatches, matches);
            throw new CollectionDoesNotMatch(message);

        }

    }
}
