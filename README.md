# Introducing ModelMatcher

**ModelMatcher** makes asserting on your models easier and cleaner.

### Assert that a model is as expected 

```csharp
var result = myApi.Call();

var expectedResult = new MyModel
{
    Field1 = "Some Value",
    Field2 = 123,
    Field3 = false
};

result.ShouldMatch(expectedResult);
```

### Compare only the non-default fields in the expected model

```csharp
var result = myApi.Call();

// We only care that Field2 matches, ignore everything else
var expectedResult = new MyModel
{
    Field2 = 123,
};

result.ShouldMatchNonDefaultFields(expectedResult);
```

### Check for a matching item in a collection

```csharp
var resultCollection = myApi.Call();

var expectedResult = new MyModel
{
    Field1 = "Some Value",
    Field2 = 123,
    Field3 = false
};

result.ShouldContainAnItemMatching(expectedResult);
```
