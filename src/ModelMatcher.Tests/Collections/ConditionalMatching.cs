namespace ModelMatcher.Tests.Collections
{
    using System;
    using System.Collections.Generic;
    using Conditions;
    using Constants;
    using Exceptions;
    using Extensions;
    using Shouldly;
    using TestModels;
    using Xunit;

    public partial class Collections
    {
        public class ConditionalMatching
        {
            [Fact]
            public void Should_Not_Throw_If_Match_Found_And_No_Conditions()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var list = new List<SimpleModel>
                {
                    new SimpleModel
                    {
                        DecimalProperty = 456,
                        GuidProperty = Guid.NewGuid(),
                        IntProperty = 678,
                        StringProperty = "Goodbye, World",
                        BoolProperty = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolProperty = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 123,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "Hello, World",
                    BoolProperty = true
                };

                // Then
                Should.NotThrow(() => list.ShouldContainAMatch(expectedModel, new List<Condition>()));
            }

            [Fact]
            public void Should_Throw_If_Match_Not_Found_And_No_Conditions()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var list = new List<SimpleModel>
                {
                    new SimpleModel
                    {
                        DecimalProperty = 456,
                        GuidProperty = Guid.NewGuid(),
                        IntProperty = 678,
                        StringProperty = "Goodbye, World",
                        BoolProperty = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolProperty = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 123,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "This is a failure!",
                    BoolProperty = true
                };

                // Then
                Should.Throw<CollectionDoesNotMatch>(() => list.ShouldContainAMatch(expectedModel, new List<Condition>()));
            }

            [Fact]
            public void Should_Not_Throw_If_Condition_Overrides_Default_Match_Logic()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var list = new List<SimpleModel>
                {
                    new SimpleModel
                    {
                        DecimalProperty = 456,
                        GuidProperty = Guid.NewGuid(),
                        IntProperty = 678,
                        StringProperty = "Goodbye, World",
                        BoolProperty = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolProperty = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 123,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "This is not a match",
                    BoolProperty = true
                };

                // Then
                Should.NotThrow(() => list.ShouldContainAMatch(expectedModel, new[]
                {
                    Ignore.This(() => expectedModel.StringProperty)
                }));
            }

            [Fact]
            public void Should_Throw_If_Correct_Number_Of_Matches_Not_Found()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var list = new List<SimpleModel>
                {
                    new SimpleModel
                    {
                        DecimalProperty = 456,
                        GuidProperty = Guid.NewGuid(),
                        IntProperty = 678,
                        StringProperty = "Goodbye, World",
                        BoolProperty = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolProperty = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 123,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = "This is a failure!",
                    BoolProperty = true
                };

                // Then
                Should.Throw<CollectionDoesNotMatch>(() => 
                    list.ShouldContainMatches(expectedModel, new[]
                    {
                        Ignore.This(() => expectedModel.StringProperty)
                    }, 
                    Matches.Two));
            }

            [Fact]
            public void Should_Not_Throw_If_Correct_Number_Of_Matches_Found_After_Conditions_Applied()
            {
                // Given
                const string guidString = "49934b49-1cc3-443d-a89a-23496708f64b";
                var list = new List<SimpleModel>
                {
                    new SimpleModel
                    {
                        DecimalProperty = 456,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 678,
                        StringProperty = "Goodbye, World",
                        BoolProperty = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 456,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 678,
                        StringProperty = "Hello, World",
                        BoolProperty = false
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 456,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 678,
                    StringProperty = "This will never match, everything else should",
                    BoolProperty = false
                };

                // Then
                Should.NotThrow(() =>
                    list.ShouldContainMatches(expectedModel, new[]
                    {
                        Ignore.This(() => expectedModel.StringProperty)
                    },
                    Matches.Two));
            }
        }

    }
}
