namespace ModelMatcher.Extensions
{
    using System;
    using System.Reflection;
    using System.Text;
    using Exceptions;

    public static class ModelMatcherExtensions
    {
        private static readonly string ExceptionText = "Expected property {0} to be \"{1}\" but was \"{2}\"" + Environment.NewLine;

        public static void ShouldMatch<T>(this T itemUnderTest, T expected)
        {
            var exceptionList = new StringBuilder();

            var matches = true;

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in properties)
            {
                var itemUnderTestValue = field.GetValue(itemUnderTest);
                var expectedValue = field.GetValue(expected);
                if (!itemUnderTestValue.Equals(expectedValue))
                {
                    matches = false;
                    exceptionList.AppendFormat(ExceptionText, field.Name, expectedValue, itemUnderTestValue);
                }
            }

            if (!matches)
                throw new DidNotMatch(exceptionList.ToString());
        }
    }
}
