namespace ModelMatcher.Tests.SimpleItemAssertions
{
    using System;
    using Conditions;
    using Exceptions;
    using Extensions;
    using Shouldly;
    using TestModels;
    using Xunit;

    public partial class SimpleItemAssertions
    {
        public class ConditionalMatching
        {
            [Fact]
            public void ShouldNotThrowIfAllMatchExceptIgnoredProperties()
            {
                // Given
                const string guid1 = "49934b49-1cc3-443d-a89a-23496708f64b";
                const string guid2 = "fd239783-3ae5-4df4-82ed-57c98e69d5ac";
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guid1),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = true
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guid2),
                    IntProperty = 345,
                    StringProperty = "This does not match",
                    BoolType = true
                };

                // Then
                Should.NotThrow(() =>
                    model.ShouldMatch(expectedResult, new[]
                    {
                        Ignore.This(() => expectedResult.GuidProperty),
                        Ignore.This(() => expectedResult.StringProperty)
                    })
                );
            }

        }
    }
}
