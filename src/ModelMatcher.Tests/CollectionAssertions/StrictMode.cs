﻿namespace ModelMatcher.Tests.CollectionAssertions
{
    using System;
    using System.Collections.Generic;
    using Exceptions;
    using Extensions;
    using Shouldly;
    using TestModels;
    using Xunit;

    public partial class CollectionAssertions
    {
        public class StrictMode
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
                    StringProperty = "Hello, World",
                    BoolType = true
                };

                // Then
                Should.NotThrow(() => list.ShouldContainAMatch(expectedModel));
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
                    DecimalProperty = 456,
                    GuidProperty = Guid.NewGuid(),
                    IntProperty = 678,
                    StringProperty = "Good Morning, World",
                    BoolType = false
                };

                // Then
                Should.Throw<NoMatchingItemInCollection>(() => list.ShouldContainAMatch(expectedModel));
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
                    DecimalProperty = 456,
                    GuidProperty = Guid.NewGuid(),
                    IntProperty = 678,
                    StringProperty = "Good Morning, World",
                    BoolType = false
                };
                var exception = Record.Exception(() => list.ShouldContainAMatch(expectedModel));

                // Then
                exception.Message.ShouldBe("Could not find a matching item in the collection");
            }

            [Fact]
            public void DoesNotIgnoreDefaultProperties()
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
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = default(string),
                    BoolType = true
                };

                // Then
                Should.Throw<NoMatchingItemInCollection>(() => list.ShouldContainAMatch(expectedModel));
            }

        }
    }
}
