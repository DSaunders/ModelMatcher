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

#### Coming soon..

- Support more complex models, currently only supports models one level deep
- Some version ShouldMatchNonDefaultFields that actually allows you to check a default field in the model. For example, I might wish to assert that a bool is ``false`` without having to assert the whole model.


