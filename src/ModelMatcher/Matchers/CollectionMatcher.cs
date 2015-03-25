namespace ModelMatcher.Matchers
{
    using System.Collections.Generic;
    using System.Linq;
    using Conditions;
    using Exceptions;

    internal static class CollectionMatcher
    {
        internal static void ShouldContainMatch<T>(IEnumerable<T> list, T expectedItem, MatchCondition matchCondition, int requiredMatches, IEnumerable<Condition> conditions = null)
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