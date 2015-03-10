namespace ModelMatcher.Tests
{
    using System;
    using Exceptions;
    using Extensions;
    using Shouldly;
    using TestModels;
    using Xunit;

    public class SimpleItemAssertions
    {
        [Fact]
        public void ShouldNotThrowIfAllPublicPropertiesMatch()
        {
            // Given
            const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
            var model = new SimpleModel
            {
                DecimalProperty = 123m,
                GuidProperty = Guid.Parse(guidString),
                IntProperty = 345,
                StringProperty = "Hello, World"
            };

            // When
            var expectedResult = new SimpleModel
            {
                DecimalProperty = 123m,
                GuidProperty = Guid.Parse(guidString),
                IntProperty = 345,
                StringProperty = "Hello, World"
            };

            // Then
            Should.NotThrow(() => model.ShouldMatch(expectedResult));
        }

        [Fact]
        public void ShouldThrowIfOnePropertyDoesNotMatch()
        {
            // Given
            const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
            var model = new SimpleModel
            {
                DecimalProperty = 123m,
                GuidProperty = Guid.Parse(guidString),
                IntProperty = 345,
                StringProperty = "Hello, World"
            };

            // When
            var expectedResult = new SimpleModel
            {
                DecimalProperty = 123m,
                GuidProperty = Guid.Parse(guidString),
                IntProperty = 345,
                StringProperty = "Goodbye, World"
            };

            // Then
            Should.Throw<DidNotMatch>(() => model.ShouldMatch(expectedResult));
        }
        
        [Fact]
        public void ShouldThrowIfMultiplePropertiesDoesNotMatch()
        {
            // Given
            var model = new SimpleModel
            {
                DecimalProperty = 123m,
                GuidProperty = Guid.NewGuid(),
                IntProperty = 345,
                StringProperty = "Hello, World"
            };
            
            // When
            var expectedResult = new SimpleModel
            {
                DecimalProperty = 999m,
                GuidProperty = Guid.NewGuid(),
                IntProperty = 999,
                StringProperty = "Goodbye, World"
            };

            // Then
            Should.Throw<DidNotMatch>(() => model.ShouldMatch(expectedResult));
        }

        [Fact]
        public void ShouldRecordSingleNonMatchingPropertyValueInExceptionMessage()
        {
            // Given
            const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
            var model = new SimpleModel
            {
                DecimalProperty = 123m,
                GuidProperty = Guid.Parse(guidString),
                IntProperty = 345,
                StringProperty = "Hello, World"
            };

            // When
            var expectedResult = new SimpleModel
            {
                DecimalProperty = 123m,
                GuidProperty = Guid.Parse(guidString),
                IntProperty = 345,
                StringProperty = "Goodbye, World"
            };
            var exception = Record.Exception(() => model.ShouldMatch(expectedResult));

            // Then
            exception.Message.ShouldContain("Expected property StringProperty to be \"Goodbye, World\" but was \"Hello, World\"" + Environment.NewLine);
        }

        [Fact]
        public void ShouldRecordMultipleNonMatchingPropertyValuesInExceptionMessage()
        {
            // Given
            var model = new SimpleModel
            {
                DecimalProperty = 123m,
                GuidProperty = Guid.Parse("5319ca84-12f8-4d1c-a773-af2a5bbb0d1f"),
                IntProperty = 345,
                StringProperty = "Hello, World"
            };

            // When
            var expectedResult = new SimpleModel
            {
                DecimalProperty = 999m,
                GuidProperty = Guid.Parse("9e09fed2-2070-4339-a9d1-2b824fd38bf7"),
                IntProperty = 999,
                StringProperty = "Goodbye, World"
            };
            var exception = Record.Exception(() => model.ShouldMatch(expectedResult));

            // Then// Then
            exception.Message.ShouldContain("Expected property DecimalProperty to be \"999\" but was \"123\"" + Environment.NewLine);
            exception.Message.ShouldContain("Expected property GuidProperty to be \"9e09fed2-2070-4339-a9d1-2b824fd38bf7\" but was \"5319ca84-12f8-4d1c-a773-af2a5bbb0d1f\"" + Environment.NewLine);
            exception.Message.ShouldContain("Expected property IntProperty to be \"999\" but was \"345\"" + Environment.NewLine);
            exception.Message.ShouldContain("Expected property StringProperty to be \"Goodbye, World\" but was \"Hello, World\"" + Environment.NewLine);
        }
    }
}
