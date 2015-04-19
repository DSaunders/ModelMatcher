namespace ModelMatcher.Assertions.Tests.SingleItems
{
    using System;
    using Shouldly;
    using Xunit;

    public partial class SingleItems
    {
        public class NonDefaultPropertiesMatch
        {
            [Fact]
            public void Should_Not_Throw_If_All_Properties_Match()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolProperty = true
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolProperty = true
                };

                // Then
                Should.NotThrow(() => model.ShouldMatchNonDefaultProperties(expectedResult));
            }

            [Fact]
            public void Should_Not_Throw_If_Property_With_Default_Value_In_Expected_Model_Does_Not_Match()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolProperty = true
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = default(int),
                    StringProperty = default(string),
                    BoolProperty = default(bool)
                };

                // Then
                Should.NotThrow(() => model.ShouldMatchNonDefaultProperties(expectedResult));
            }

            [Fact]
            public void Should_Throw_If_Non_Default_Property_In_Expected_Model_Does_Not_Match()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolProperty = true
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 999,
                    StringProperty = default(string),
                    BoolProperty = default(bool)
                };

                // Then
                Should.Throw<DidNotMatch>(() => model.ShouldMatchNonDefaultProperties(expectedResult));
            }

            [Fact]
            public void Should_Record_Non_Matching_Properties_In_Exception_Message()
            {
                // Given
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse("5319ca84-12f8-4d1c-a773-af2a5bbb0d1f"),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolProperty = true
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse("49934b49-1cc3-443d-a89a-23496708f64b"),
                    IntProperty = 999,
                    StringProperty = default(string),
                    BoolProperty = default(bool)
                };
                var exception = Record.Exception(() => model.ShouldMatchNonDefaultProperties(expectedResult));

                // Then
                exception.Message.ShouldContain("Expected property GuidProperty to be \"49934b49-1cc3-443d-a89a-23496708f64b\" but was \"5319ca84-12f8-4d1c-a773-af2a5bbb0d1f\"" + Environment.NewLine);
                exception.Message.ShouldContain("Expected property IntProperty to be \"999\" but was \"345\"" + Environment.NewLine);
            }
        }
    }
}