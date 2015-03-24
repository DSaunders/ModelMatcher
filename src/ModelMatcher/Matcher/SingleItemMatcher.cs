namespace ModelMatcher.Matcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Conditions;
    using Enums;
    using Models;

    internal static class SingleItemMatcher
    {
        private static readonly string ExceptionText = "Expected property {0} to be \"{1}\" but was \"{2}\"" + Environment.NewLine;

        internal static MatchResult MatchSingleItem<T>(T itemUnderTest, T expected, MatchMode mode, IEnumerable<Condition> conditions = null)
        {
            conditions = conditions ?? Enumerable.Empty<Condition>();

            var matchResult = new MatchResult { Matches = true };

            var exceptionList = new StringBuilder();

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var itemUnderTestValue = propertyInfo.GetValue(itemUnderTest);
                var expectedValue = propertyInfo.GetValue(expected);
                if (!itemUnderTestValue.Equals(expectedValue))
                {
                    // See if we have an conditions, and if so, are we ignoring this property?
                    var matchingCondition = conditions.FirstOrDefault(c => c.PropertyName == propertyInfo.Name);
                    if (matchingCondition != null && matchingCondition.Type == ConditionType.Ignore)
                        continue;
                    
                    if (mode == MatchMode.IgnoreDefaultProperties)
                    {
                        // Create a default version of this property
                        object defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

                        // Ignore this property if it has a default value
                        if (expectedValue == null || expectedValue.Equals(defaultValue))
                            continue;

                        matchResult.Matches = false;
                        exceptionList.AppendFormat(ExceptionText, propertyInfo.Name, expectedValue, itemUnderTestValue);
                    }
                    else
                    {
                        matchResult.Matches = false;
                        exceptionList.AppendFormat(ExceptionText, propertyInfo.Name, expectedValue, itemUnderTestValue);
                    }
                }
            }

            matchResult.Exceptions = exceptionList.ToString();
            return matchResult;
        }
    }
}
