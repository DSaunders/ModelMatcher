namespace ModelMatcher.Assertions.Tests.Collections
{
    using System;
    using System.Collections.Generic;
    using Assertions.Constants;
    using Assertions.Exceptions;
    using Assertions.Extensions;
    using Shouldly;
    using TestModels;
    using Xunit;

    public partial class Collections
    {
        public class AllPropertiesMatch
        {
            [Fact]
            public void Does_Not_Throw_If_Collection_Contains_Matching_Item()
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
                Should.NotThrow(() => list.ShouldContainAMatch(expectedModel));
            }

            [Fact]
            public void Throws_If_Collection_Does_Not_Contain_Matching_Item()
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
                        DecimalProperty = 123m,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolProperty = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 456,
                    GuidProperty = Guid.NewGuid(),
                    IntProperty = 678,
                    StringProperty = "Good Morning, World",
                    BoolProperty = false
                };

                // Then
                Should.Throw<CollectionDoesNotMatch>(() => list.ShouldContainAMatch(expectedModel));
            }

            [Fact]
            public void Throws_With_Correct_Exception_Message_If_Collection_Does_Not_Contain_Matching_Item()
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
                        DecimalProperty = 123m,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolProperty = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 456,
                    GuidProperty = Guid.NewGuid(),
                    IntProperty = 678,
                    StringProperty = "Good Morning, World",
                    BoolProperty = false
                };
                var exception = Record.Exception(() => list.ShouldContainAMatch(expectedModel));

                // Then
                exception.Message.ShouldBe("Could not find a matching item in the collection");
            }

            [Fact]
            public void Does_Not_Ignore_Default_Properties()
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
                        DecimalProperty = 123m,
                        GuidProperty = Guid.Parse(guidString),
                        IntProperty = 345,
                        StringProperty = "Hello, World",
                        BoolProperty = true
                    }
                };

                // When
                var expectedModel = new SimpleModel
                {
                    DecimalProperty = 123m,
                    GuidProperty = Guid.Parse(guidString),
                    IntProperty = 345,
                    StringProperty = default(string),
                    BoolProperty = true
                };

                // Then
                Should.Throw<CollectionDoesNotMatch>(() => list.ShouldContainAMatch(expectedModel));
            }

            [Fact]
            public void Does_Not_Throw_If_Collection_Contains_Correct_Number_Of_Matching_Items()
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
                Should.NotThrow(() => list.ShouldContainMatches(expectedModel,  Matches.Two));
            }

            [Fact]
            public void Throws_If_Collection_Contains_Incorrect_Number_Of_Matching_Items()
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
                Should.Throw<CollectionDoesNotMatch>(() => list.ShouldContainMatches(expectedModel, Matches.Two));
            }

            [Fact]
            public void Throws_With_Correct_Message_If_Collection_Contains_Incorrect_Number_Of_Matching_Items()
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
                var exception = Record.Exception(() => list.ShouldContainMatches(expectedModel, Matches.Two));

                // Then
                exception.Message.ShouldBe("Expected 2 matching items but found 1");
            }
        }
    }
}
