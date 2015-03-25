namespace ModelMatcher.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Conditions;
    using Exceptions;
    using Matcher;

    public static class ModelMatcherExtensions
    {
        public static void ShouldMatchNonDefaultProperties<T>(this T itemUnderTest, T expected)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected, MatchCondition.IgnoreIfDefaultInExpectedModel);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }

        public static void ShouldMatchNonDefaultProperties<T>(this T itemUnderTest, T expected, IEnumerable<Condition> conditions)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected, MatchCondition.IgnoreIfDefaultInExpectedModel, conditions);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }

        public static void ShouldMatch<T>(this T itemUnderTest, T expected)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected, MatchCondition.Match);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }

        public static void ShouldMatch<T>(this T itemUnderTest, T expected, IEnumerable<Condition> conditions)
        {
            var matchResult = SingleItemMatcher.MatchSingleItem(itemUnderTest, expected, MatchCondition.Match, conditions);

            if (!matchResult.Matches)
                throw new DidNotMatch(matchResult.Exceptions);
        }

        public static void ShouldContainAMatch<T>(this IEnumerable<T> list, T expectedItem)
        {
            ShouldContainMatch(list, expectedItem, MatchCondition.Match, 1);
        }

        public static void ShouldContainAMatch<T>(this IEnumerable<T> list, T expectedItem, IEnumerable<Condition> conditions)
        {
            ShouldContainMatch(list, expectedItem, MatchCondition.Match, 1, conditions);
        }

        

        public static void ShouldContainMatches<T>(this IEnumerable<T> list, T expectedItem,  int numberOfMatches)
        {
            ShouldContainMatch(list, expectedItem, MatchCondition.Match, numberOfMatches);
        }

        public static void ShouldContainMatches<T>(this IEnumerable<T> list, T expectedItem, IEnumerable<Condition> conditions, int numberOfMatches)
        {
            ShouldContainMatch(list, expectedItem, MatchCondition.Match, numberOfMatches, conditions);
        }


        public static void ShouldContainAMatchOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem)
        {
            ShouldContainMatch(list, expectedItem, MatchCondition.IgnoreIfDefaultInExpectedModel, 1);
        }

        public static void ShouldContainAMatchOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem, IEnumerable<Condition> conditions)
        {
            ShouldContainMatch(list, expectedItem, MatchCondition.IgnoreIfDefaultInExpectedModel, 1, conditions);
        }
        
        public static void ShouldContainMatchesOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem, int numberOfMatches)
        {
            ShouldContainMatch(list, expectedItem, MatchCondition.IgnoreIfDefaultInExpectedModel, numberOfMatches);
        }

        public static void ShouldContainMatchesOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem, IEnumerable<Condition> conditions, int numberOfMatches)
        {
            ShouldContainMatch(list, expectedItem, MatchCondition.IgnoreIfDefaultInExpectedModel, numberOfMatches, conditions);
        }
        
        private static void ShouldContainMatch<T>(IEnumerable<T> list, T expectedItem, MatchCondition matchCondition, int requiredMatches, IEnumerable<Condition> conditions = null)
        {
            var matches = list.Count(item => 
                SingleItemMatcher.MatchSingleItem(item, expectedItem, matchCondition, conditions).Matches);

            if (requiredMatches == matches)
                return;

            if (requiredMatches == 1)
                throw new CollectionDoesNotMatch("Could not find a matching item in the collection");

            var message = string.Format("Expected {0} matching items but found {1}", requiredMatches, matches);
            throw new CollectionDoesNotMatch(message);

        }
    }
}
