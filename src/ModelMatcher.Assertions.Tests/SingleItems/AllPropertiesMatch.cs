namespace ModelMatcher.Assertions.Tests.SingleItems
{
    using System;
    using Assertions.Exceptions;
    using Assertions.Extensions;
    using Shouldly;
    using TestModels;
    using Xunit;

    public partial class SingleItems
    {
        public class AllPropertiesMatch
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
                Should.NotThrow(() => model.ShouldMatch(expectedResult));
            }

            [Fact]
            public void Should_Throw_If_One_Property_Does_Not_Match()
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
            public void Should_Throw_If_Multiple_Properties_Do_Not_Match()
            {
                // Given
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.NewGuid(),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolProperty = false
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 999m,
                    GuidProperty = Guid.NewGuid(),
                    IntProperty = 999,
                    StringProperty = "Goodbye, World",
                    BoolProperty = true
                };

                // Then
                Should.Throw<DidNotMatch>(() => model.ShouldMatch(expectedResult));
            }

            [Fact]
            public void Should_Record_Single_Non_Matching_Property_Value_In_Exception_Message()
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
                exception.Message.ShouldContain(
                    "Expected property StringProperty to be \"Goodbye, World\" but was \"Hello, World\"" +
                    Environment.NewLine);
            }

            [Fact]
            public void Should_Record_Multiple_Non_Matching_Property_Values_In_Exception_Message()
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
                    DecimalProperty = 999m,
                    GuidProperty = Guid.Parse("9e09fed2-2070-4339-a9d1-2b824fd38bf7"),
                    IntProperty = 999,
                    StringProperty = "Goodbye, World",
                    BoolProperty = false
                };
                var exception = Record.Exception(() => model.ShouldMatch(expectedResult));

                // Then// Then
                exception.Message.ShouldContain("Expected property DecimalProperty to be \"999\" but was \"123\"" + Environment.NewLine);
                exception.Message.ShouldContain("Expected property GuidProperty to be \"9e09fed2-2070-4339-a9d1-2b824fd38bf7\" but was \"5319ca84-12f8-4d1c-a773-af2a5bbb0d1f\"" + Environment.NewLine);
                exception.Message.ShouldContain("Expected property IntProperty to be \"999\" but was \"345\"" + Environment.NewLine);
                exception.Message.ShouldContain("Expected property StringProperty to be \"Goodbye, World\" but was \"Hello, World\"" + Environment.NewLine);
                exception.Message.ShouldContain("Expected property BoolProperty to be \"false\" but was \"true\"" + Environment.NewLine);
            }

            [Fact]
            public void Should_Not_Ignore_Default_Properties()
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
                    StringProperty = default(string)
                };

                // Then
                Should.Throw<DidNotMatch>(() => model.ShouldMatch(expectedResult));
            }
        }
    }
}
