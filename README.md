**ModelMatcher** makes vertifying models in your unit tests easier and cleaner. 

No more Linq statements to assert that an item is in a collection, and no need for for ten lines of assert statements just to check that all of the properties on a model are correct.

## Single items ...

```csharp
var result = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

result.ShouldMatch(expectedResult);
```

## Collections

One matching item:

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

resultCollection.ShouldContainAMatch(expectedResult);
```

Or multiple matches:

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

resultCollection.ShouldContainMatches(expectedResult, Matches.Two);
```

## Conditional Matching

```csharp
var result = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false,
    Property4 = "Another Value"
    ...
};

result.ShouldMatch(expectedResult, new[]
	{
		Ignore.This(() => expectedResult.Property1),
		Ignore.This(() => expectedResult.Property3),
	});
```

## Non-default matching

Check only the properties that are *not* set to the default value in the expected model. This saves you from specifying every property in the expected model even if it is not important.

Single items:

```csharp
var result = myApi.Call();

// We only care that Property2 matches, ignore everything else
var expectedResult = new MyModel
{
    Property2 = 123,
};

result.ShouldMatchNonDefaultProperties(expectedResult);
```

Collections:

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value"
};

resultCollection.ShouldContainAMatchOfNonDefaultProperties(expectedResult);
```

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value"
};

resultCollection.ShouldContainMatchesOfNonDefaultProperties(expectedResult, Matches.Three);
```

Sometimes you need to use this mode, but match a property that *is* set to the default value. For example, to assert that a ``bool`` is set to ``false``.

You can do this with conditional matching:

```csharp
var result = myApi.Call();

// We only care that Property2 matches, ignore everything else
var expectedResult = new MyModel
{
    Property2 = 123,
    Property3 = false
};

result.ShouldMatchNonDefaultProperties(expectedResult, new[]
	{
		// Property3 would normally be ignored, as it is set to the default value
		// in the expected model. Force ModelMatcher to check it.
		Match.This(() => expectedResult.Property3)
	});
```


### Coming soon..

- Support more complex models, currently only supports models one level deep
- Conditional model matching for lists
