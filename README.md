# Introducing ModelMatcher

**ModelMatcher** makes asserting on your models easier and cleaner. No more Linq statements to assert that an item is in a collection and no need for for ten lines of asserts just to check that all of the properties on a model are correct.

### Assert that a model has the values you expect

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

### Compare only the non-default properties in the expected model

```csharp
var result = myApi.Call();

// We only care that Property2 matches, ignore everything else
var expectedResult = new MyModel
{
    Property2 = 123,
};

result.ShouldMatchNonDefaultProperties(expectedResult);
```

### Check for a matching items in a collection

All properies must match:

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

result.ShouldContainAMatch(expectedResult);
```

Only non-default properies in the expected model must match:

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

result.ShouldContainAMatchOfNonDefaultProperties(expectedResult);
```

Multiple matches are expected:

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

result.ShouldContainMatches(expectedResult, Matches.Two);
```

Multiple matches of non-default properties only:

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false
};

result.ShouldContainMatchesOfNonDefaultProperties(expectedResult, Matches.Three);
```

### Conditional Model Matching

Match the entire model, with some exceptions:

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Property1 = "Some Value",
    Property2 = 123,
    Property3 = false,
	Property4 = "Another Value,
	...
};

result.ShouldContainMatches(expectedResult, Matches.Two);

model.ShouldMatch(expectedResult, new[]
	{
		Ignore.This(() => expectedResult.Property1),
		Ignore.This(() => expectedResult.Property3)
	});
```


#### Coming soon..

- Support more complex models, currently only supports models one level deep
- Conditional model matching for lists