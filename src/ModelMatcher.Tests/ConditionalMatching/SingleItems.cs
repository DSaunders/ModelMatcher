namespace ModelMatcher.Tests.ConditionalMatching
{
    using System;
    using System.Collections.Generic;
    using Conditions;
    using Exceptions;
    using Extensions;
    using Shouldly;
    using TestModels;
    using Xunit;

    public partial class ConditionalMatching
    {
        public class SingleItems
        {
            [Fact]
            public void Should_Not_Throw_If_All_Items_Match()
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
                    GuidProperty = Guid.Parse(guid1),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolType = true
                };

                // Then
                Should.NotThrow(() => model.ShouldMatch(expectedResult, new List<Condition>()));

            }

            [Fact]
            public void Should_Not_Throw_If_All_Match_Except_Ignore_Conditions()
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
            public void Should_Throw_If_Does_Not_Match_With_Ignore_Conditions()
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
            public void Should_Not_Throw_If_All_Match_Except_Ignore_Conditions_And_Defaults()
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
            public void Should_Throw_If_Matching_Non_Default_Properties_And_Match_Condition_Fails()
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
            public void Should_Support_Multiple_Match_Conditions()
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
            public void Should_Throw_With_Multiple_Conditions_Passed_And_Some_Not_Satisfied()
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
            public void Should_Not_Throw_If_String_Case_Does_Not_Match_With_IgnoreCase_Condition()
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
            public void Should_Not_Throw_If_Property_Is_Not_Null_If_Condition_Applied()
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
