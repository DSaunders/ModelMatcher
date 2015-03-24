namespace ModelMatcher.Tests.CollectionAssertions
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using Exceptions;
    using Extensions;
    using Shouldly;
    using TestModels;
    using Xunit;

    public partial class CollectionAssertions
    {
        public class IgnoreDetailsPropertiesMode
        {
            [Fact]
            public void DoesNotThrowIfCollectionContainsMatchingItem()
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
                        BoolType = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolType = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 123,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = default(string)
                };

                // Then
                Should.NotThrow(() => list.ShouldContainAMatchOfNonDefaultProperties(expectedModel));
            }

            [Fact]
            public void ThrowsIfCollectionDoesNotContainMatchingItem()
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
                        BoolType = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123m,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolType = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 999,
                    GuidProperty = Guid.NewGuid(),
                    IntProperty = 678,
                    StringProperty = default(string)
                };

                // Then
                Should.Throw<CollectionDoesNotMatch>(() => list.ShouldContainAMatchOfNonDefaultProperties(expectedModel));
            }

            [Fact]
            public void ThrowswithCorrectExceptionMessageIfCollectionDoesNotContainMatchingItem()
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
                        BoolType = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123m,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolType = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 999,
                    GuidProperty = Guid.NewGuid(),
                    IntProperty = 678,
                    StringProperty = default(string)
                };
                var exception = Record.Exception(() => list.ShouldContainAMatchOfNonDefaultProperties(expectedModel));

                // Then
                exception.Message.ShouldBe("Could not find a matching item in the collection");
            }

            [Fact]
            public void DoesNotThrowIfCollectionContainsCorrectNumberOfMatchingItems()
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
                        BoolType = false
                    },

                    new SimpleModel
                    {
                        DecimalProperty = 123,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolType = true
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Another, Message",
                        BoolType = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 123,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = default(string),
                    BoolType = true
                };

                // Then
                Should.NotThrow(() => list.ShouldContainMatchesOfNonDefaultProperties(expectedModel, Matches.Two));
            }

            [Fact]
            public void ThrowsIfCollectionContainsIncorrectNumberOfMatchingItems()
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
                        BoolType = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolType = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 123,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = default(string),
                };

                // Then
                Should.Throw<CollectionDoesNotMatch>(() => list.ShouldContainMatchesOfNonDefaultProperties(expectedModel, Matches.Two));
            }

            [Fact]
            public void ThrowsWithCorrectMessageIfCollectionContainsIncorrectNumberOfMatchingItems()
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
                        BoolType = false
                    },
                    new SimpleModel
                    {
                        DecimalProperty = 123,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolType = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 123,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = default(string),
                };

                // Then
                var exception = Record.Exception(() => list.ShouldContainMatchesOfNonDefaultProperties(expectedModel, Matches.Two));

                // Then
                exception.Message.ShouldBe("Expected 2 matching items but found 1");
            }
        }
    }
}
