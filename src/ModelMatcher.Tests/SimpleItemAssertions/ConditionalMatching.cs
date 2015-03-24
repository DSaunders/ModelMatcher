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
            public void ShouldNotThrowIfAllMatchExceptIgnoreConditions()
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

            [Fact]
            public void ShouldThrowIfDoesNotMatchWithIgnoreConditions()
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
                    StringProperty = "This does not match and there is no condition for it",
                    BoolType = true
                };

                // Then
                Should.Throw<DidNotMatch>(() =>
                    model.ShouldMatch(expectedResult, new[]
                    {
                        Ignore.This(() => expectedResult.GuidProperty)
                    })
                );
            }

            [Fact]
            public void ShouldNotThrowIfAllMatchExceptIgnoreConditionsAndDefaultsWhenMatchingNonDefaultProperties()
            {
                // Given
                const string guid1 = "49934b49-1cc3-443d-a89a-23496708f64b";
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guid1),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = false
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = default(Guid),
                    IntProperty = 999,
                    StringProperty = default(string),
                    BoolType = false
                };

                // Then
                // Guid and string should be ignored, BoolType should be matched
                Should.NotThrow(() =>
                    model.ShouldMatchNonDefaultProperties(expectedResult, new[]
                    {
                        Ignore.This(() => expectedResult.IntProperty),
                    })
                );
            }

            [Fact]
            public void ShouldThrowIfMatchConditionFailsWhenMatchingNonDefaultProperties()
            {
                // Given
                const string guid1 = "49934b49-1cc3-443d-a89a-23496708f64b";
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
                    GuidProperty = default(Guid),
                    IntProperty = 345,
                    StringProperty = default(string),
                    BoolType = false
                };

                // Then
                // Guid and string should be ignored, BoolType should be matched
                Should.Throw<DidNotMatch>(() =>
                    model.ShouldMatchNonDefaultProperties(expectedResult, new[]
                    {
                        Match.This(() => expectedResult.BoolType),
                    })
                );
            }


            [Fact]
            public void ShouldSupportMultipleConditions()
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
                    BoolType = false
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guid2),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = false
                };

                // Then
                // Guid and string should be ignored, BoolType should be matched
                Should.NotThrow(() =>
                    model.ShouldMatchNonDefaultProperties(expectedResult, new[]
                    {
                        Match.This(() => expectedResult.BoolType),
                        Ignore.This(() => expectedResult.GuidProperty),
                    })
                );
            }

            [Fact]
            public void ShouldThrowWithMultipleConditionsPassedAndSomeNotSatisfied()
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
                    StringProperty = "Hello, World",
                    BoolType = false
                };

                // Then
                // Guid and string should be ignored, BoolType should be matched
                Should.Throw<DidNotMatch>(() =>
                    model.ShouldMatchNonDefaultProperties(expectedResult, new[]
                    {
                        Match.This(() => expectedResult.BoolType),
                        Ignore.This(() => expectedResult.GuidProperty),
                    })
                );
            }


            [Fact]
            public void ShouldIgnoreCaseIfConditionApplied()
            {
                // Given
                const string guid1 = "49934b49-1cc3-443d-a89a-23496708f64b";
                var model = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guid1),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = false
                };

                // When
                var expectedResult = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guid1),
                    IntProperty = 345,
                    StringProperty = "Hello, WORLD",
                    BoolType = false
                };

                // Then
                Should.NotThrow(() =>
                    model.ShouldMatch(expectedResult, new[]
                    {
                        Match.IgnoringCase(() => expectedResult.StringProperty)
                    })
                );
            }


            [Fact]
            public void ShouldMatchObjectsIfNotNullIfConditionApplied()
            {
                // Given
                var model = new ComplexModel()
                {
                    Name = "My Model",
                    Child = new SimpleModel()
                };

                // When
                var expectedResult = new ComplexModel()
                {
                    Name = "My Model"
                };

                // Then
                Should.NotThrow(() =>
                    model.ShouldMatch(expectedResult, new[]
                    {
                        Match.IfNotNull(() => expectedResult.Child)   
                    })
                );
            }
        }
    }
}
