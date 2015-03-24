# Introducing ModelMatcher

**ModelMatcher** makes asserting on your models easier and cleaner.

### Assert that a model is as expected 

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

### Check for a matching item in a collection

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