namespace ModelMatcher.Assertions
{
    using System.Collections.Generic;

    public static class CollectionMatchExtensions
    {
        public static void ShouldContainAMatch<T>(this IEnumerable<T> list, T expectedItem)
        {
            CollectionMatcher.ShouldContainMatch(list, expectedItem, MatchCondition.Match, 1);
        }

        public static void ShouldContainAMatch<T>(this IEnumerable<T> list, T expectedItem, IEnumerable<Condition> conditions)
        {
            CollectionMatcher.ShouldContainMatch(list, expectedItem, MatchCondition.Match, 1, conditions);
        }

        public static void ShouldContainMatches<T>(this IEnumerable<T> list, T expectedItem, int numberOfMatches)
        {
            CollectionMatcher.ShouldContainMatch(list, expectedItem, MatchCondition.Match, numberOfMatches);
        }

        public static void ShouldContainMatches<T>(this IEnumerable<T> list, T expectedItem, IEnumerable<Condition> conditions, int numberOfMatches)
        {
            CollectionMatcher.ShouldContainMatch(list, expectedItem, MatchCondition.Match, numberOfMatches, conditions);
        }

        public static void ShouldContainAMatchOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem)
        {
            CollectionMatcher.ShouldContainMatch(list, expectedItem, MatchCondition.IgnoreIfDefaultInExpectedModel, 1);
        }

        public static void ShouldContainAMatchOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem, IEnumerable<Condition> conditions)
        {
            CollectionMatcher.ShouldContainMatch(list, expectedItem, MatchCondition.IgnoreIfDefaultInExpectedModel, 1, conditions);
        }

        public static void ShouldContainMatchesOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem, int numberOfMatches)
        {
            CollectionMatcher.ShouldContainMatch(list, expectedItem, MatchCondition.IgnoreIfDefaultInExpectedModel, numberOfMatches);
        }

        public static void ShouldContainMatchesOfNonDefaultProperties<T>(this IEnumerable<T> list, T expectedItem, IEnumerable<Condition> conditions, int numberOfMatches)
        {
            CollectionMatcher.ShouldContainMatch(list, expectedItem, MatchCondition.IgnoreIfDefaultInExpectedModel, numberOfMatches, conditions);
        }
    }
}
