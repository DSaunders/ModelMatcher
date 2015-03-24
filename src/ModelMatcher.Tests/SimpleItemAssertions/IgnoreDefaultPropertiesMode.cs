namespace ModelMatcher.Tests.SimpleItemAssertions
{
    using System;
    using Enums;
    using Exceptions;
    using Extensions;
    using Shouldly;
    using TestModels;
    using Xunit;

    public partial class SimpleItemAssertions
    {
        public class IgnoreDefaultPropertiesMode
        {
            [Fact]
            public void ShouldNotThrowIfAllPropertiesMatch()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = true
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = true
                };

                // Then
                Should.NotThrow(() => model.ShouldMatchNonDefaultProperties(expectedResult));
            }

            [Fact]
            public void ShouldNotThrowIfPropertyWithDefaultValueInExpectedModelDoesNotMatch()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = true
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = default(int),
                    StringProperty = default(string),
                    BoolType = default(bool)
                };

                // Then
                Should.NotThrow(() => model.ShouldMatchNonDefaultProperties(expectedResult));
            }

            [Fact]
            public void ShouldThrowIfPropertySetInExpectedModelDoesNotMatch()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = true
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 999,
                    StringProperty = default(string),
                    BoolType = default(bool)
                };

                // Then
                Should.Throw<DidNotMatch>(() => model.ShouldMatchNonDefaultProperties(expectedResult));
            }

            [Fact]
            public void ShouldRecordNonMatchingPropertiesInExceptionMessage()
            {
                // Given
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse("5319ca84-12f8-4d1c-a773-af2a5bbb0d1f"),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = true
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse("49934b49-1cc3-443d-a89a-23496708f64b"),
                    IntProperty = 999,
                    StringProperty = default(string),
                    BoolType = default(bool)
                };
                var exception = Record.Exception(() => model.ShouldMatchNonDefaultProperties(expectedResult));

                // Then
                exception.Message.ShouldContain("Expected property GuidProperty to be \"49934b49-1cc3-443d-a89a-23496708f64b\" but was \"5319ca84-12f8-4d1c-a773-af2a5bbb0d1f\"" + Environment.NewLine);
                exception.Message.ShouldContain("Expected property IntProperty to be \"999\" but was \"345\"" + Environment.NewLine);
            }
        }
    }

   
}