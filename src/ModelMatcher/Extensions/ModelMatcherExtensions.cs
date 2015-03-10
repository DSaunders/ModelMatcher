namespace ModelMatcher.Extensions
{
    using System;
    using System.Reflection;
    using System.Text;
    using Enums;
    using Exceptions;

    public static class ModelMatcherExtensions
    {
        private static readonly string ExceptionText = "Expected property {0} to be \"{1}\" but was \"{2}\"" + Environment.NewLine;

        public static void ShouldMatchNonDefaultFields<T>(this T itemUnderTest, T expected)
        {
            itemUnderTest.ShouldMatch(expected, MatchMode.IgnoreDefaultPropertiesInExpectedModel);
        }

        public static void ShouldMatch<T>(this T itemUnderTest, T expected)
        {
            itemUnderTest.ShouldMatch(expected, MatchMode.Strict);
        }

        private static void ShouldMatch<T>(this T itemUnderTest, T expected, MatchMode mode)
        {
            var exceptionList = new StringBuilder();

            var matches = true;

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var propertyInfo in properties)
            {
                var itemUnderTestValue = propertyInfo.GetValue(itemUnderTest);
                var expectedValue = propertyInfo.GetValue(expected);
                if (!itemUnderTestValue.Equals(expectedValue))
                {
                    if (mode == MatchMode.IgnoreDefaultPropertiesInExpectedModel)
                    {
                        // Create a default version of this property
                        object defaultValue = propertyInfo.PropertyType.IsValueType ? Activator.CreateInstance(propertyInfo.PropertyType) : null;

                        // Ignore this property if it has a default value
                        if (expectedValue == null || expectedValue.Equals(defaultValue))
                            continue;

                        matches = false;
                        exceptionList.AppendFormat(ExceptionText, propertyInfo.Name, expectedValue, itemUnderTestValue);
                    }
                    else
                    {
                        matches = false;
                        exceptionList.AppendFormat(ExceptionText, propertyInfo.Name, expectedValue, itemUnderTestValue);
                    }
                }
            }

            if (!matches)
                throw new DidNotMatch(exceptionList.ToString());
        }
    }
}
