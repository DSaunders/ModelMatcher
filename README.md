Asserting that a POCO has the values you expect is ugly:

```csharp
var result = myApi.Call();

Assert.Equal(result.Field1, "Some value");
Assert.Equal(result.Field2, 123);
Assert.Equal(result.Field3, false);
etc..
```

It's even worse when you are trying to assert that a collection contains an item with the values you are expecting. Even with a great library like ``Shouldly`` you end up with something like this:

```csharp
var resultCollection = myApi.Call();

resultCollection.ShouldContain(x => x.Field1 == "Some Value" && x.Field2 == 123 && x.Field3 == false etc..); 
```

# Introducing ModelMatcher

**ModelMatcher** makes asserting on your models easy.

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

var expectedResult = new MyModel
{
    Field2 = 123,
};

// Only checks Field2
result.ShouldMatch(expectedResult, MatchMode.IgnoreDefaultPropertiesInExpectedModel);
```

### Check for a matching model in a collection

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
