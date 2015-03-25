namespace ModelMatcher.Matcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Conditions;
    using Models;

    internal static class SingleItemMatcher
    {
        private static readonly string ExceptionText = "Expected property {0} to be \"{1}\" but was \"{2}\"" + Environment.NewLine;

        internal static MatchResult MatchSingleItem<T>(T itemUnderTest, T expected, MatchCondition defaultMatchCondition, IEnumerable<Condition> conditions = null)
        {
            conditions = conditions ?? Enumerable.Empty<Condition>();

            var matchResult = new MatchResult { Matches = true };
            var exceptionList = new StringBuilder();

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var itemUnderTestValue = propertyInfo.GetValue(itemUnderTest);
                var expectedValue = propertyInfo.GetValue(expected);

                var matchingCondition = conditions.FirstOrDefault(c => c.PropertyName == propertyInfo.Name);
                var conditionType = (matchingCondition != null)
                    ? matchingCondition.Type
                    : defaultMatchCondition;

                // Shortcut if the value is present and matches
                if (itemUnderTestValue != null && itemUnderTestValue.Equals(expectedValue))
                    continue;

                switch (conditionType)
                {
                    case MatchCondition.Ignore:
                        continue;

                    case MatchCondition.IgnoreCase:
                        if (itemUnderTestValue == null)
                            break;

                        if (itemUnderTestValue.ToString()
                            .Equals(expectedValue.ToString(), StringComparison.OrdinalIgnoreCase))
                            continue;
                        break;

                    case MatchCondition.IfNotNull:
                        if (itemUnderTestValue != null)
                            continue;
                        break;

                    case MatchCondition.Match:
                        if (itemUnderTestValue != null && itemUnderTestValue.Equals(expectedValue))
                            continue;
                        break;

                    case MatchCondition.IgnoreIfDefaultInExpectedModel:

                        // Create a default version of this property
                        var defaultValue = propertyInfo.PropertyType.IsValueType
                            ? Activator.CreateInstance(propertyInfo.PropertyType)
                            : null;

                        // Ignore this property if it has a default value
                        if (expectedValue == null || expectedValue.Equals(defaultValue))
                            continue;
                        break;
                }

                // If we get here, we should have matched but didn't
                matchResult.Matches = false;
                exceptionList.AppendFormat(ExceptionText, propertyInfo.Name, expectedValue, itemUnderTestValue);
            }

            matchResult.Exceptions = exceptionList.ToString();
            return matchResult;
        }
    }
}
